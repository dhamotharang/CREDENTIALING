using AHC.CD.Business.BusinessModels.WelcomeLetter;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Profiles;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.AppointmentInformation;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Exceptions.Credentialing;
using AHC.CD.Resources.Document;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.AppointmentInfo
{
    internal class AppointmentManager : IAppointmentManager
    {
        private IUnitOfWork uow = null;
        IProfileRepository profileRepository = null;
        IDocumentsManager documentManager = null;
        ProfileDocumentManager profileDocumentManager = null;
        private IGenericRepository<CredentialingAppointmentResult> CCMRepo;

        public AppointmentManager(IUnitOfWork uow, IDocumentsManager documentManager)
        {
            this.uow = uow;
            this.profileRepository = uow.GetProfileRepository();
            this.documentManager = documentManager;
            this.profileDocumentManager = new ProfileDocumentManager(profileRepository, documentManager);
            CCMRepo = uow.GetGenericRepository<CredentialingAppointmentResult>();
        }

        /// <summary>
        /// Method to save/update the CCM appointment form for an individual provider
        /// </summary>
        /// <param name="credentialingInfoID"></param>
        /// <param name="credentialingAppointmentDetail"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<CredentialingAppointmentDetail> UpdateAppointmentDetails(int credentialingInfoID, DocumentDTO AttachDocument, CredentialingAppointmentDetail credentialingAppointmentDetail, string authUserId)
        {
            try
            {
                int userID = GetUserId(authUserId);

                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var includeProperties = new string[]
                {
                    "CredentialingLogs",
                    "CredentialingLogs.CredentialingAppointmentDetail",
                    "CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians",
                    "CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists",
                    "CredentialingLogs.CredentialingActivityLogs"                    
                };
                CredentialingInfo updateCredentialingInfo = await credentialingInfoRepo.FindAsync(c => c.CredentialingInfoID == credentialingInfoID, includeProperties);

                if (updateCredentialingInfo.IsDelegated.Equals(Entities.MasterData.Enums.YesNoOption.NO.ToString()))
                {
                    throw new CredentialingException(ExceptionMessage.NOT_DELEGATED_EXCEPTION);
                }

                CredentialingLog latestCredentialingLog = updateCredentialingInfo.CredentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();

                if (latestCredentialingLog != null)
                {
                    if (latestCredentialingLog.CredentialingActivityLogs.Any(a => a.ActivityType == CD.Entities.MasterData.Enums.ActivityType.PSV && a.ActivityStatusType == AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed))
                    {

                        if (latestCredentialingLog.CredentialingAppointmentDetail == null)
                        {
                            latestCredentialingLog.CredentialingAppointmentDetail = new CredentialingAppointmentDetail();
                        }
                        credentialingAppointmentDetail.CredentialingAppointmentDetailID = latestCredentialingLog.CredentialingAppointmentDetail.CredentialingAppointmentDetailID;
                        latestCredentialingLog.CredentialingAppointmentDetail.CredentialingCoveringPhysicians = null;
                        latestCredentialingLog.CredentialingAppointmentDetail.CredentialingSpecialityLists = null;
                        latestCredentialingLog.CredentialingAppointmentDetail = AutoMapper.Mapper.Map<CredentialingAppointmentDetail, CredentialingAppointmentDetail>(credentialingAppointmentDetail, latestCredentialingLog.CredentialingAppointmentDetail);
                        latestCredentialingLog.CredentialingAppointmentDetail.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;

                        if (AttachDocument.FileName != null)
                        {
                            latestCredentialingLog.CredentialingAppointmentDetail.FileUploadPath = AddDocument(DocumentRootPath.CCM_ATTACH_DOCUMENT_PATH, DocumentTitle.CCM_DOCUMENT, null, AttachDocument);
                        }
                        else
                        {
                            latestCredentialingLog.CredentialingAppointmentDetail.FileUploadPath = credentialingAppointmentDetail.FileUploadPath;
                        }
                        CredentialingActivityLog addCredentialingActivityLog = new CredentialingActivityLog();
                        addCredentialingActivityLog.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.CCMAppointment;
                        addCredentialingActivityLog.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.InProgress;
                        addCredentialingActivityLog.ActivityByID = userID;
                        latestCredentialingLog.CredentialingActivityLogs.Add(addCredentialingActivityLog);
                    }
                    else
                    {
                        throw new CredentialingException(ExceptionMessage.PSV_INCOMPLETE_EXCEPTION);
                    }
                }

                credentialingInfoRepo.Update(updateCredentialingInfo);
                credentialingInfoRepo.Save();

                return latestCredentialingLog.CredentialingAppointmentDetail;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to schedule CCM appointment for multiple providers at one go
        /// </summary>
        /// <param name="credentialingAppointmentDetails"></param>
        /// <param name="appointmentDate"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<List<int>> ScheduleAppointmentForMany(List<int> credentialingAppointmentDetails, DateTime appointmentDate, string authUserId)
        {
            try
            {
                int userID = GetUserId(authUserId);

                var credentialingAppointmentDetailRepo = uow.GetGenericRepository<CredentialingAppointmentDetail>();
                IEnumerable<CredentialingAppointmentDetail> allCredentialingAppointmentDetails = await credentialingAppointmentDetailRepo.GetAllAsync("CredentialingCoveringPhysicians, CredentialingCoveringPhysicians.PracticeProviderTypes, CredentialingCoveringPhysicians.PracticeProviderSpecialties, CredentialingAppointmentSchedule, CredentialingAppointmentResult");
                //List<CredentialingAppointmentDetail> updateCredentialingAppointmentDetail = new List<CredentialingAppointmentDetail>();
                foreach (var item in allCredentialingAppointmentDetails)
                {
                    foreach (int id in credentialingAppointmentDetails)
                    {
                        if (item.CredentialingAppointmentDetailID.Equals(id))
                        {
                            if (item.CredentialingAppointmentSchedule == null)
                            {
                                item.CredentialingAppointmentSchedule = new CredentialingAppointmentSchedule();
                            }
                            item.CredentialingAppointmentSchedule.AppointmentDate = appointmentDate;
                            item.CredentialingAppointmentSchedule.AppointmentSetByID = userID;
                            //updateCredentialingAppointmentDetail.Add(item);
                            credentialingAppointmentDetailRepo.Update(item);
                        }
                    }
                }
                credentialingAppointmentDetailRepo.Save();
                return credentialingAppointmentDetails;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to remove the scheduled CCM appointment for an individual provider for Delegated Credentialing
        /// </summary>
        /// <param name="credentialingAppointmentDetailID"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<int> RemoveScheduledAppointmentForIndividual(int credentialingAppointmentDetailID, string authUserId)
        {
            try
            {
                int userID = GetUserId(authUserId);

                var credentialingAppointmentDetailRepo = uow.GetGenericRepository<CredentialingAppointmentDetail>();
                CredentialingAppointmentDetail updateCredentialingAppointmentDetail = await credentialingAppointmentDetailRepo.FindAsync(c => c.CredentialingAppointmentDetailID == credentialingAppointmentDetailID, "CredentialingAppointmentSchedule");
                updateCredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate = null;
                credentialingAppointmentDetailRepo.Update(updateCredentialingAppointmentDetail);
                credentialingAppointmentDetailRepo.Save();
                return credentialingAppointmentDetailID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Method to save the result of CCM Meeting
        /// </summary>
        /// <param name="credentialingAppointmentDetailID"></param>
        /// <param name="credentialingAppointmentResult"></param>
        /// <param name="userID"></param>
        /// <returns></returns>
        public async Task<int> SaveResultForScheduledAppointment(int credentialingAppointmentDetailID, DocumentDTO CCMDocument, CredentialingAppointmentResult credentialingAppointmentResult, string authUserId)
        {
            try
            {
                int userID = GetUserId(authUserId);

                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                List<CredentialingInfo> credentialingInfoes = credentialingInfoRepo.GetAll("CredentialingLogs.CredentialingAppointmentDetail,CredentialingContractRequests,CredentialingContractRequests.ContractGrid").ToList();
                CredentialingInfo selectedCredentialingInfo = null;
                foreach (var credInfo in credentialingInfoes)
                {
                    if (credInfo.CredentialingLogs.Count > 0 && credInfo.CredentialingLogs.Last().CredentialingAppointmentDetail != null && credInfo.CredentialingLogs.Last().CredentialingAppointmentDetail.CredentialingAppointmentDetailID == credentialingAppointmentDetailID)
                    {
                        selectedCredentialingInfo = credInfo;
                        if (selectedCredentialingInfo.IsDelegated == Entities.MasterData.Enums.YesNoOption.YES.ToString() && selectedCredentialingInfo.CredentialingLogs.Last().Credentialing == Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString()) {
                            foreach (var item in selectedCredentialingInfo.CredentialingContractRequests)
                            {
                                item.InitialCredentialingDate = credentialingAppointmentResult.SignedDate;
                                foreach (var nextitem in item.ContractGrid)
                                {
                                    nextitem.InitialCredentialingDate = credentialingAppointmentResult.SignedDate;
                                }
                            }
                        }
                    }
                }
                credentialingInfoRepo.Update(selectedCredentialingInfo);
                credentialingInfoRepo.Save();
                var credentialingAppointmentDetailRepo = uow.GetGenericRepository<CredentialingAppointmentDetail>();
                CredentialingAppointmentDetail updateCredentialingAppointmentDetail = await credentialingAppointmentDetailRepo.FindAsync(c => c.CredentialingAppointmentDetailID == credentialingAppointmentDetailID, "CredentialingAppointmentResult");
                updateCredentialingAppointmentDetail.CredentialingAppointmentResult = new CredentialingAppointmentResult();
                updateCredentialingAppointmentDetail.CredentialingAppointmentResult = AutoMapper.Mapper.Map<CredentialingAppointmentResult, CredentialingAppointmentResult>(credentialingAppointmentResult, updateCredentialingAppointmentDetail.CredentialingAppointmentResult);
                updateCredentialingAppointmentDetail.CredentialingAppointmentResult.SignedByID = userID;
                updateCredentialingAppointmentDetail.CredentialingAppointmentResult.SignaturePath = AddDocument(DocumentRootPath.CCM_SIGNATURE_DOCUMENT_PATH, DocumentTitle.CCM_SIGN_DOCUMENT, null, CCMDocument);
                credentialingAppointmentDetailRepo.Update(updateCredentialingAppointmentDetail);
                credentialingAppointmentDetailRepo.Save();

                var credentialingLogRepo = uow.GetGenericRepository<CredentialingLog>();
                CredentialingLog updateCredentialingLog = await credentialingLogRepo.FindAsync(c => c.CredentialingAppointmentDetail.CredentialingAppointmentDetailID == credentialingAppointmentDetailID, "CredentialingActivityLogs");

                CredentialingActivityLog addCredentialingActivityLog = new CredentialingActivityLog();
                addCredentialingActivityLog.ActivityByID = userID;
                addCredentialingActivityLog.ActivityType = Entities.MasterData.Enums.ActivityType.CCMAppointment;
                addCredentialingActivityLog.ActivityStatusType = Entities.MasterData.Enums.ActivityStatusType.Completed;
                updateCredentialingLog.CredentialingActivityLogs.Add(addCredentialingActivityLog);
                credentialingLogRepo.Update(updateCredentialingLog);
                credentialingLogRepo.Save();

                if (selectedCredentialingInfo.IsDelegated == Entities.MasterData.Enums.YesNoOption.YES.ToString() && selectedCredentialingInfo.CredentialingLogs.Last().Credentialing == Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString())
                {
                    CredentialingLog updateLog = await credentialingLogRepo.FindAsync(c => c.CredentialingAppointmentDetail.CredentialingAppointmentDetailID == credentialingAppointmentDetailID, "CredentialingActivityLogs");

                    CredentialingActivityLog addActivityLog = new CredentialingActivityLog();
                    addActivityLog.ActivityByID = userID;
                    addActivityLog.ActivityType = Entities.MasterData.Enums.ActivityType.Loading;
                    addActivityLog.ActivityStatusType = Entities.MasterData.Enums.ActivityStatusType.Completed;
                    updateLog.CredentialingActivityLogs.Add(addActivityLog);
                    credentialingLogRepo.Update(updateLog);
                    credentialingLogRepo.Save();
                }

                return credentialingAppointmentDetailID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> SaveResultForScheduledAppointmentwithdigitalsignature(int credentialingAppointmentDetailID, CredentialingAppointmentResult credentialingAppointmentResult, string authUserId)
        {
            try
            {
                int userID = GetUserId(authUserId);

                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                List<CredentialingInfo> credentialingInfoes = credentialingInfoRepo.GetAll("CredentialingLogs.CredentialingAppointmentDetail,CredentialingContractRequests,CredentialingContractRequests.ContractGrid").ToList();
                CredentialingInfo selectedCredentialingInfo = null;
                foreach (var credInfo in credentialingInfoes)
                {
                    if (credInfo.CredentialingLogs.Count > 0 && credInfo.CredentialingLogs.Last().CredentialingAppointmentDetail != null && credInfo.CredentialingLogs.Last().CredentialingAppointmentDetail.CredentialingAppointmentDetailID == credentialingAppointmentDetailID)
                    {
                        selectedCredentialingInfo = credInfo;
                        if (selectedCredentialingInfo.IsDelegated == Entities.MasterData.Enums.YesNoOption.YES.ToString() && selectedCredentialingInfo.CredentialingLogs.Last().Credentialing == Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString())
                        {
                            foreach (var item in selectedCredentialingInfo.CredentialingContractRequests)
                            {
                                item.InitialCredentialingDate = credentialingAppointmentResult.SignedDate;
                                foreach (var nextitem in item.ContractGrid)
                                {
                                    nextitem.InitialCredentialingDate = credentialingAppointmentResult.SignedDate;
                                }
                            }
                        }
                    }
                }
                credentialingInfoRepo.Update(selectedCredentialingInfo);
                credentialingInfoRepo.Save();
                var credentialingAppointmentDetailRepo = uow.GetGenericRepository<CredentialingAppointmentDetail>();
                CredentialingAppointmentDetail updateCredentialingAppointmentDetail = await credentialingAppointmentDetailRepo.FindAsync(c => c.CredentialingAppointmentDetailID == credentialingAppointmentDetailID, "CredentialingAppointmentResult");
                updateCredentialingAppointmentDetail.CredentialingAppointmentResult = new CredentialingAppointmentResult();
                updateCredentialingAppointmentDetail.CredentialingAppointmentResult = AutoMapper.Mapper.Map<CredentialingAppointmentResult, CredentialingAppointmentResult>(credentialingAppointmentResult, updateCredentialingAppointmentDetail.CredentialingAppointmentResult);
                updateCredentialingAppointmentDetail.CredentialingAppointmentResult.SignedByID = userID;
                if (credentialingAppointmentResult.SignaturePath.Contains("\\ApplicationDocuments\\"))
                {
                    updateCredentialingAppointmentDetail.CredentialingAppointmentResult.SignaturePath = credentialingAppointmentResult.SignaturePath;
                }
                if (!credentialingAppointmentResult.SignaturePath.Contains("\\ApplicationDocuments\\"))
                {
                    updateCredentialingAppointmentDetail.CredentialingAppointmentResult.SignaturePath = DocumentRootPath.CCM_SIGNATURE_DOCUMENT_PATH + @"\" + credentialingAppointmentResult.SignaturePath;
                }
                credentialingAppointmentDetailRepo.Update(updateCredentialingAppointmentDetail);
                credentialingAppointmentDetailRepo.Save();

                var credentialingLogRepo = uow.GetGenericRepository<CredentialingLog>();
                CredentialingLog updateCredentialingLog = await credentialingLogRepo.FindAsync(c => c.CredentialingAppointmentDetail.CredentialingAppointmentDetailID == credentialingAppointmentDetailID, "CredentialingActivityLogs");

                CredentialingActivityLog addCredentialingActivityLog = new CredentialingActivityLog();
                addCredentialingActivityLog.ActivityByID = userID;
                addCredentialingActivityLog.ActivityType = Entities.MasterData.Enums.ActivityType.CCMAppointment;
                addCredentialingActivityLog.ActivityStatusType = Entities.MasterData.Enums.ActivityStatusType.Completed;
                updateCredentialingLog.CredentialingActivityLogs.Add(addCredentialingActivityLog);
                credentialingLogRepo.Update(updateCredentialingLog);
                credentialingLogRepo.Save();

                if (selectedCredentialingInfo.IsDelegated == Entities.MasterData.Enums.YesNoOption.YES.ToString() && selectedCredentialingInfo.CredentialingLogs.Last().Credentialing == Entities.MasterData.Enums.CredentialingType.ReCredentialing.ToString())
                {
                    CredentialingLog updateLog = await credentialingLogRepo.FindAsync(c => c.CredentialingAppointmentDetail.CredentialingAppointmentDetailID == credentialingAppointmentDetailID, "CredentialingActivityLogs");

                    CredentialingActivityLog addActivityLog = new CredentialingActivityLog();
                    addActivityLog.ActivityByID = userID;
                    addActivityLog.ActivityType = Entities.MasterData.Enums.ActivityType.Loading;
                    addActivityLog.ActivityStatusType = Entities.MasterData.Enums.ActivityStatusType.Completed;
                    updateLog.CredentialingActivityLogs.Add(addActivityLog);
                    credentialingLogRepo.Update(updateLog);
                    credentialingLogRepo.Save();
                }

                return credentialingAppointmentDetailID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<CredentialingInfo>> GetAllCredentialInfoList()
        {
            try
            {
                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var includeProperties = "CredentialingLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,CredentialingLogs.CredentialingActivityLogs,Profile.PersonalDetail,Profile.PersonalDetail.ProviderTitles.ProviderType,Profile.SpecialtyDetails.Specialty,Plan,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists";
                var ListOfCredentialingInfo = await credentialingInfoRepo.GetAllAsync(includeProperties);

                List<CredentialingInfo> credentialList = ListOfCredentialingInfo.ToList();
                var updatedCredentialList = new List<CredentialingInfo>();
                foreach (CredentialingInfo CredData in credentialList)
                {

                    CredentialingLog credentialingLog = CredData.CredentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();
                    if (credentialingLog != null)
                    {
                        CredData.CredentialingLogs = new List<CredentialingLog>();
                        CredData.CredentialingLogs.Add(credentialingLog);
                        if (credentialingLog.CredentialingActivityLogs != null)
                        {
                            if (!credentialingLog.CredentialingActivityLogs.Any(a => a.ActivityType == CD.Entities.MasterData.Enums.ActivityType.CCMAppointment && a.ActivityStatusType == AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed))
                            {
                                if (credentialingLog.CredentialingActivityLogs.Any(a => a.ActivityType == CD.Entities.MasterData.Enums.ActivityType.CCMAppointment && a.ActivityStatusType == AHC.CD.Entities.MasterData.Enums.ActivityStatusType.InProgress))
                                {
                                    if (credentialingLog.CredentialingAppointmentDetail.CredentialingAppointmentSchedule != null)
                                    {
                                        if (credentialingLog.CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate == null)
                                        {
                                            updatedCredentialList.Add(CredData);
                                        }
                                    }
                                    else
                                    {
                                        updatedCredentialList.Add(CredData);
                                    }
                                }
                            }
                        }
                    }

                }
                return updatedCredentialList;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<IEnumerable<CredentialingInfo>> GetAllFilteredCredentialInfoList()
        {
            try
            {
                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var includeProperties = "CredentialingLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,CredentialingLogs.CredentialingActivityLogs,Profile.PersonalDetail,Profile.PersonalDetail.ProviderTitles.ProviderType,Profile.SpecialtyDetails.Specialty,Plan,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists";
                var ListOfCredentialingInfo = await credentialingInfoRepo.GetAllAsync(includeProperties);

                List<CredentialingInfo> credentialList = ListOfCredentialingInfo.ToList();
                var updatedCredentialList = new List<CredentialingInfo>();
                foreach (CredentialingInfo CredData in credentialList)
                {
                    if (CredData.Profile.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString())
                    {
                        CredentialingLog credentialingLog = CredData.CredentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();
                        if (credentialingLog != null)
                        {
                            CredData.CredentialingLogs = new List<CredentialingLog>();
                            CredData.CredentialingLogs.Add(credentialingLog);
                            if (credentialingLog.CredentialingActivityLogs != null)
                            {
                                if (!credentialingLog.CredentialingActivityLogs.Any(a => a.ActivityType == CD.Entities.MasterData.Enums.ActivityType.CCMAppointment && a.ActivityStatusType == AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed))
                                {
                                    if (credentialingLog.CredentialingActivityLogs.Any(a => a.ActivityType == CD.Entities.MasterData.Enums.ActivityType.CCMAppointment && a.ActivityStatusType == AHC.CD.Entities.MasterData.Enums.ActivityStatusType.InProgress))
                                    {
                                        if (credentialingLog.CredentialingAppointmentDetail != null && credentialingLog.CredentialingAppointmentDetail.CredentialingAppointmentSchedule != null && credentialingLog.CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate != null)
                                        {
                                            updatedCredentialList.Add(CredData);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return updatedCredentialList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string GetCCMSignature(string userid)
        {
            try
            {
                int cduserId = GetUserId(userid);
                List<CredentialingAppointmentResult> credentialingappointmentresult = CCMRepo.GetAll().Where(x => x.SignedByID == cduserId).ToList();
                
                if(credentialingappointmentresult.Count !=0)
                {
                    var signature = (credentialingappointmentresult.OrderByDescending(z => z.LastModifiedDate).FirstOrDefault()).SignaturePath;
                    string signaturepath = signature;
                    return signaturepath;
                }
                else
                {
                    return null;
                }
                
            }
            catch (Exception ex)
            {
                throw;
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
        /// <param name="profileId"></param>
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


        public async Task<CredentialingInfo> GetCredentialinfoByID(int id)
        {
            try
            {
                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var includeProperties = new string[]
                {
                    "CredentialingLogs",
                    "CredentialingLogs.CredentialingAppointmentDetail",
                    "CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule",
                    "CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians",
                    "CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists",
                    "CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists.Specialty",
                    "Profile.PersonalDetail",
                    "Profile.PersonalDetail.ProviderTitles.ProviderType",
                    "Profile.SpecialtyDetails.Specialty",
                    "Plan",
                };
                CredentialingInfo CredentialingInfo = await credentialingInfoRepo.FindAsync(c => c.CredentialingInfoID == id, includeProperties);
                CredentialingLog credentialingLog = CredentialingInfo.CredentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();
                if (credentialingLog != null)
                {
                    CredentialingInfo.CredentialingLogs = new List<CredentialingLog>();
                    CredentialingInfo.CredentialingLogs.Add(credentialingLog);
                }
                return CredentialingInfo;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<Object> GetAllCredentialInfoListHistory()
        {
            try
            {
                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var includeProperties = "CredentialingLogs,CredentialingLogs.CredentialingAppointmentDetail,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentResult,CredentialingLogs.CredentialingAppointmentDetail.CredentialingCoveringPhysicians,CredentialingLogs.CredentialingActivityLogs,Profile.PersonalDetail,Profile.PersonalDetail.ProviderTitles.ProviderType,Profile.SpecialtyDetails.Specialty,Plan,CredentialingLogs.CredentialingAppointmentDetail.CredentialingAppointmentSchedule,CredentialingLogs.CredentialingAppointmentDetail.CredentialingSpecialityLists";
                var ListOfCredentialingInfo = await credentialingInfoRepo.GetAsync(c=> c.Profile.Status== AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString(),includeProperties);
                var AppointmentScheuled = new List<Object>();
                var NoAppointmentScheuled = new List<Object>();
                List<CredentialingInfo> credentialList = ListOfCredentialingInfo.ToList();
                var updatedCredentialList = new Object();
                foreach (CredentialingInfo CredData in credentialList)
                {

                    CredentialingLog credentialingLog = CredData.CredentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();
                    if (credentialingLog != null)
                    {
                        CredData.CredentialingLogs = new List<CredentialingLog>();
                        CredData.CredentialingLogs.Add(credentialingLog);
                        if (credentialingLog.CredentialingActivityLogs != null)
                        {
                            if (!credentialingLog.CredentialingActivityLogs.Any(a => a.ActivityType == CD.Entities.MasterData.Enums.ActivityType.CCMAppointment && a.ActivityStatusType == AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed))
                            {
                                if (credentialingLog.CredentialingActivityLogs.Any(a => a.ActivityType == CD.Entities.MasterData.Enums.ActivityType.CCMAppointment && a.ActivityStatusType == AHC.CD.Entities.MasterData.Enums.ActivityStatusType.InProgress))
                                {
                                    if (credentialingLog.CredentialingAppointmentDetail.CredentialingAppointmentSchedule != null)
                                    {
                                        if (credentialingLog.CredentialingAppointmentDetail.CredentialingAppointmentSchedule.AppointmentDate != null && credentialingLog.CredentialingAppointmentDetail.CredentialingAppointmentResult == null)
                                        {
                                            AppointmentScheuled.Add(CredData);
                                        }
                                        else
                                        {
                                            NoAppointmentScheuled.Add(CredData);
                                        }
                                    }
                                    else
                                    {
                                        NoAppointmentScheuled.Add(CredData);
                                    }
                                }
                            }
                        }
                    }
                }
                updatedCredentialList = new
                {
                    AppointmentScheule = AppointmentScheuled,
                    NoAppointmentScheule = NoAppointmentScheuled
                };
                return updatedCredentialList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GenerateWelcomeLetter(int profileid, string welcomeletterinitialdate, string servicecommencingdate)
        {
            string status = "";
            try
            {
                var profiledetails = await profileRepository.FindAsync(y => y.ProfileID == profileid);
                WelcomeLetterBusinessModel welcomeLetter = new WelcomeLetterBusinessModel();
                welcomeLetter.ProviderName = profiledetails.PersonalDetail.Salutation + " " + profiledetails.PersonalDetail.FirstName + " " + profiledetails.PersonalDetail.MiddleName + " " + profiledetails.PersonalDetail.LastName;
                welcomeLetter.WelcomeLetterPreparedDate = welcomeletterinitialdate;
                welcomeLetter.ServiceCommencingDate = servicecommencingdate;

                status = "true";
            }
            catch(Exception ex)
            {
                throw (ex);
            }
            return status;
        }
    }
}
