using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Profiles;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Resources.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.Loading
{
    public class CredentialingContractManager : ICredentialingContractManager
    {
        private IUnitOfWork uow = null;
        IProfileRepository profileRepository = null;
        IDocumentsManager documentManager = null;
        ProfileDocumentManager profileDocumentManager = null;

        public CredentialingContractManager(IUnitOfWork uow, IDocumentsManager documentManager )
        {
            this.uow = uow;
            this.profileRepository = uow.GetProfileRepository();
            this.documentManager = documentManager;
            this.profileDocumentManager = new ProfileDocumentManager(profileRepository, documentManager);
        }

        /// <summary>
        /// Method to add Credentialing Contract Request
        /// </summary>
        /// <param name="credentialingInfoID"></param>
        /// <param name="credentialingContractRequest"></param>
        /// <param name="userAuthID"></param>
        /// <returns></returns>
        public async Task<CredentialingContractRequest> AddCredentialingContractRequest(int credentialingInfoID, CredentialingContractRequest credentialingContractRequest, string userAuthID)
        {
            try
            {
                int userID = GetUserId(userAuthID);

                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                CredentialingInfo updateCredentialingInfo = credentialingInfoRepo.Find(c => c.CredentialingInfoID == credentialingInfoID, "CredentialingContractRequests, CredentialingContractRequests.ProfileReports, CredentialingContractRequests.BusinessEntity, CredentialingContractRequests.ContractLOBs.LOB, CredentialingContractRequests.ContractPracticeLocations.ProfilePracticeLocation.Facility, CredentialingContractRequests.ContractSpecialties.ProfileSpecialty.Specialty, CredentialingLogs.CredentialingActivityLogs ");

                CredentialingLog latestCredentialingLog = updateCredentialingInfo.CredentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();
                List<CredentialingActivityLog> credentialingActivityLogs = latestCredentialingLog.CredentialingActivityLogs.ToList();
                int flag = 0;
                foreach (var item in credentialingActivityLogs)
                {
                    if (item.ActivityType == AHC.CD.Entities.MasterData.Enums.ActivityType.Loading) {

                        flag = 1;
                        break;
                    }
                }

                if (flag == 0) {
                    CredentialingActivityLog credentialingActivityLog = new CredentialingActivityLog();
                    credentialingActivityLog.ActivityByID = userID;
                    credentialingActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                    credentialingActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Loading;
                    latestCredentialingLog.CredentialingActivityLogs.Add(credentialingActivityLog);
                }

                if (updateCredentialingInfo.CredentialingContractRequests == null)
                {
                    updateCredentialingInfo.CredentialingContractRequests = new List<CredentialingContractRequest>();
                }
                credentialingContractRequest.CredentialingContractLoadedByID = userID;
                updateCredentialingInfo.CredentialingContractRequests.Add(credentialingContractRequest);

                #region Fragmentation of request

                credentialingContractRequest.ContractGrid = new List<ContractGrid>();

                if (credentialingContractRequest.BusinessEntityID != null && credentialingContractRequest.ContractLOBs != null && credentialingContractRequest.ContractPracticeLocations != null && credentialingContractRequest.ContractSpecialties != null)
                {
                    foreach (var contractLOB in credentialingContractRequest.ContractLOBs)
                    {
                        foreach (var contractPracticeLocation in credentialingContractRequest.ContractPracticeLocations)
                        {
                            foreach (var contractSpecialty in credentialingContractRequest.ContractSpecialties)
                            {
                                ContractGrid addContractGrid = new ContractGrid();
                                addContractGrid.BusinessEntityID = credentialingContractRequest.BusinessEntityID;
                                addContractGrid.CredentialingInfoID = credentialingInfoID;
                                addContractGrid.LOBID = contractLOB.LOBID;
                                addContractGrid.ProfilePracticeLocationID = contractPracticeLocation.ProfilePracticeLocationID;
                                addContractGrid.ProfileSpecialtyID = contractSpecialty.ProfileSpecialtyID;
                                addContractGrid.InitialCredentialingDate = credentialingContractRequest.InitialCredentialingDate;
                                addContractGrid.StatusType = Entities.MasterData.Enums.StatusType.Active;
                                addContractGrid.Report = new CredentialingContractInfoFromPlan();
                                //addContractGrid.Report.TerminationDate = addContractGrid.InitialCredentialingDate.Value.AddYears(3);
                                //addContractGrid.Report.ReCredentialingDate = addContractGrid.InitialCredentialingDate.Value.AddYears(3);
                                credentialingContractRequest.ContractGrid.Add(addContractGrid);
                            }
                        }
                    }
                }

                #endregion

                credentialingInfoRepo.Update(updateCredentialingInfo);
                await credentialingInfoRepo.SaveAsync();
                
                var credentialingContractRequestRepo = uow.GetGenericRepository<CredentialingContractRequest>();
                CredentialingContractRequest returnCredentialingContractRequest = credentialingContractRequestRepo.Find(c => c.CredentialingContractRequestID == credentialingContractRequest.CredentialingContractRequestID, "ProfileReports, BusinessEntity, ContractLOBs.LOB, ContractPracticeLocations.ProfilePracticeLocation.Facility, ContractSpecialties.ProfileSpecialty.SpecialtyBoardCertifiedDetail.SpecialtyBoard, ContractSpecialties.ProfileSpecialty.Specialty, ContractGrid, ContractGrid.LOB, ContractGrid.ProfilePracticeLocation.Facility, ContractGrid.ProfileSpecialty.Specialty, ContractGrid.BusinessEntity, ContractGrid.CredentialingInfo,ContractGrid.Report");

                return returnCredentialingContractRequest;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to fetch Contract Grid for a Credentialing Info
        /// </summary>
        /// <param name="credentialingInfoID"></param>
        /// <returns></returns>
        public async Task<List<ContractGrid>> GetContractGridForCredentialingInfo(int credentialingInfoID)
        {
            try
            {
                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                CredentialingInfo credentialingInfo = await credentialingInfoRepo.FindAsync(c => c.CredentialingInfoID == credentialingInfoID, "CredentialingContractRequests.ContractGrid, CredentialingContractRequests.ContractGrid.Report, CredentialingContractRequests.ContractGrid.ProfileSpecialty.Specialty, CredentialingContractRequests.ContractGrid.ProfileSpecialty.SpecialtyBoardCertifiedDetail, CredentialingContractRequests.ContractGrid.ProfileSpecialty.SpecialtyBoardCertifiedDetail.SpecialtyBoard, CredentialingContractRequests.ContractGrid.ProfilePracticeLocation.Facility,CredentialingContractRequests.ContractGrid.ProfilePracticeLocation.BillingContactPerson, CredentialingContractRequests.ContractGrid.CredentialingInfo.Plan, CredentialingContractRequests.ContractGrid.CredentialingInfo.Profile, CredentialingContractRequests.ContractGrid.BusinessEntity, CredentialingContractRequests.ContractGrid.LOB");
                List<ContractGrid> contractGrid = null;
                List<ContractGrid> requiredContractGrid = new List<ContractGrid>();
                if (credentialingInfo.CredentialingContractRequests != null)
                {
                    List<CredentialingContractRequest> credentialingContractRequest = credentialingInfo.CredentialingContractRequests.Where(r => r.ContractGrid != null).ToList();
                    contractGrid = credentialingContractRequest.SelectMany(r => r.ContractGrid).ToList();
                }
                foreach (var item in contractGrid)
                {
                    item.CredentialingInfo.CredentialingContractRequests = null;
                    if (item.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString())
                    {
                        if (item.Report == null)
                        {
                            item.Report = new CredentialingContractInfoFromPlan();
                        }                        
                        requiredContractGrid.Add(item);
                    }
                }
                return requiredContractGrid;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// Method to save Plan Report
        /// </summary>
        /// <param name="contractGrid"></param>
        /// <param name="contractDocument"></param>
        /// <param name="welcomeLetter"></param>
        /// <returns></returns>
        public async Task<ContractGrid> AddContractInfoFromPlan(ContractGrid contractGrid, DocumentDTO welcomeLetter, string authId)
        {
            try
            {
                int userId = GetUserId(authId);
                var contractGridRepo = uow.GetGenericRepository<ContractGrid>();
                ContractGrid updateContractGrid = await contractGridRepo.FindAsync(c => c.ContractGridID == contractGrid.ContractGridID, "ProfileSpecialty.Specialty, ProfilePracticeLocation.Facility, LOB, CredentialingInfo, CredentialingInfo.Plan, CredentialingInfo.Profile, BusinessEntity, Report, CredentialingInfo.CredentialingLogs.CredentialingActivityLogs");
                List<CredentialingLog> credentialingLogs = updateContractGrid.CredentialingInfo.CredentialingLogs.ToList();
                CredentialingLog credentialingLog = credentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();
                List<CredentialingActivityLog> credentialingActivityLogs = credentialingLog.CredentialingActivityLogs.ToList();
                int flag = 0;
                foreach (var item in credentialingActivityLogs)
                {
                    if (item.ActivityType == AHC.CD.Entities.MasterData.Enums.ActivityType.Report)
                    {
                        flag = 1;
                        break;
                    }
                }
                if (flag == 0)
                {
                    CredentialingActivityLog credentialingActivityLog = new CredentialingActivityLog();
                    credentialingActivityLog.ActivityByID = userId;
                    credentialingActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Report;
                    credentialingActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                    credentialingActivityLogs.Add(credentialingActivityLog);
                    credentialingLog.CredentialingActivityLogs = credentialingActivityLogs;
                }
                updateContractGrid.InitialCredentialingDate = contractGrid.InitialCredentialingDate;
                updateContractGrid.Report = AutoMapper.Mapper.Map<CredentialingContractInfoFromPlan, CredentialingContractInfoFromPlan>(contractGrid.Report, updateContractGrid.Report);
                if (welcomeLetter != null)
                {
                    updateContractGrid.Report.WelcomeLetterPath = AddDocument(DocumentRootPath.LOADING_DOCUMENT_PATH, DocumentTitle.LOADING_DOCUMENT, null, welcomeLetter);
                }                
                contractGridRepo.Update(updateContractGrid);
                contractGridRepo.Save();
                return updateContractGrid;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }

        /// <summary>
        /// Method for Quick Save
        /// </summary>
        /// <param name="contractGrid"></param>
        /// <returns></returns>
        public async Task<ContractGrid> QuickSaveContractInfoFromPlan(ContractGrid contractGrid,string authId)
        {
            try
            {
                int userId = GetUserId(authId);
                var contractGridRepo = uow.GetGenericRepository<ContractGrid>();
                ContractGrid updateContractGrid = await contractGridRepo.FindAsync(c => c.ContractGridID == contractGrid.ContractGridID, "ProfileSpecialty.Specialty, ProfilePracticeLocation.Facility, LOB, CredentialingInfo, CredentialingInfo.Plan, CredentialingInfo.Profile, BusinessEntity, Report, CredentialingInfo.CredentialingLogs.CredentialingActivityLogs");
                //updateContractGrid = AutoMapper.Mapper.Map<ContractGrid, ContractGrid>(contractGrid, updateContractGrid);
                List<CredentialingLog> credentialingLogs = updateContractGrid.CredentialingInfo.CredentialingLogs.ToList();
                CredentialingLog credentialingLog = credentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();
                List<CredentialingActivityLog> credentialingActivityLogs = credentialingLog.CredentialingActivityLogs.ToList();
                int flag = 0;
                foreach (var item in credentialingActivityLogs)
                {
                    if (item.ActivityType == AHC.CD.Entities.MasterData.Enums.ActivityType.Report) {
                        flag = 1;
                        break;
                    }   
                }
                if (flag == 0) {
                    CredentialingActivityLog credentialingActivityLog = new CredentialingActivityLog();
                    credentialingActivityLog.ActivityByID = userId;
                    credentialingActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.Report;
                    credentialingActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;
                    credentialingActivityLogs.Add(credentialingActivityLog);
                    credentialingLog.CredentialingActivityLogs = credentialingActivityLogs;
                }

                if (updateContractGrid.Report == null)
                {
                    updateContractGrid.Report = new CredentialingContractInfoFromPlan();
                }
                updateContractGrid.InitialCredentialingDate = contractGrid.InitialCredentialingDate;
                updateContractGrid.Report.ProviderID = contractGrid.Report.ProviderID;
                updateContractGrid.Report.CredentialingContractInfoFromPlanID = contractGrid.Report.CredentialingContractInfoFromPlanID;
                //updateContractGrid.Report.ContractDocumentPath = AddDocument(DocumentRootPath.LOADING_DOCUMENT_PATH, DocumentTitle.LOADING_DOCUMENT, null, contractDocument);
                //updateContractGrid.Report.WelcomeLetterPath = AddDocument(DocumentRootPath.LOADING_DOCUMENT_PATH, DocumentTitle.LOADING_DOCUMENT, null, welcomeLetter);
                contractGridRepo.Update(updateContractGrid);
                contractGridRepo.Save();
                return updateContractGrid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        /// <summary>
        /// Method to inactivate Credentialing Request and respective Contract Grid
        /// </summary>
        /// <param name="credentialingContractRequestID"></param>
        /// <returns></returns>
        public async Task<int> RemoveRequestAndGrid(int credentialingContractRequestID)
        {
            var credentialingRequestRepo = uow.GetGenericRepository<CredentialingContractRequest>();
            CredentialingContractRequest inactivateCredentialingContractRequest = await credentialingRequestRepo.FindAsync(c => c.CredentialingContractRequestID == credentialingContractRequestID, "ContractSpecialties, ContractPracticeLocations, ContractLOBs, ContractGrid, ContractGrid.Report");
            inactivateCredentialingContractRequest.StatusType = Entities.MasterData.Enums.StatusType.Inactive;
            foreach (ContractGrid grid in inactivateCredentialingContractRequest.ContractGrid)
            {
                grid.StatusType = Entities.MasterData.Enums.StatusType.Inactive;
            }
            credentialingRequestRepo.Update(inactivateCredentialingContractRequest);
            credentialingRequestRepo.Save();
            return inactivateCredentialingContractRequest.CredentialingContractRequestID;
        }

        /// <summary>
        /// Method to remove Contract Grid
        /// </summary>
        /// <param name="contractGridID"></param>
        /// <returns></returns>
        public async Task<int> RemoveGrid(int contractGridID)
        {
            try
            {
                var contractGridRepo = uow.GetGenericRepository<ContractGrid>();
                ContractGrid inactivateContractGrid = await contractGridRepo.FindAsync(c => c.ContractGridID == contractGridID, "Report");
                inactivateContractGrid.StatusType = Entities.MasterData.Enums.StatusType.Inactive;
                contractGridRepo.Update(inactivateContractGrid);
                contractGridRepo.Save();
                return inactivateContractGrid.ContractGridID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Private Methods

        /// <summary>
        /// Private method to get CDUserID
        /// </summary>
        /// <param name="inputUserId"></param>
        /// <returns></returns>
        private int GetUserId(string authUserId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == authUserId);
                return user.CDUserID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private ProfileDocument CreateProfileDocumentObject(string title, string docPath, DateTime? expiryDate)
        {
            return new ProfileDocument()
            {
                DocPath = docPath,
                Title = title,
                ExpiryDate = expiryDate
            };
        }

        /// <summary>
        /// Private method to add a document
        /// </summary>
        /// <param name="docRootPath"></param>
        /// <param name="docTitle"></param>
        /// <param name="expiryDate"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        private string AddDocument(string docRootPath, string docTitle, DateTime? expiryDate, DocumentDTO document)
        {
            //Create a profile document object
            ProfileDocument profileDocument = CreateProfileDocumentObject(docTitle, null, expiryDate);

            //Assign the Doc root path
            document.DocRootPath = docRootPath;

            //Add the document if uploaded
            return profileDocumentManager.AddCCMSignatureDocument(document, profileDocument);
        }

        #endregion
    }
}
