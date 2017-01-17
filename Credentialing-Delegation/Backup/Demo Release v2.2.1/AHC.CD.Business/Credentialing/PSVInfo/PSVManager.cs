using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Profiles;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.Credentialing.PSVInformation;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Exceptions.Credentialing.PSV;
using AHC.CD.Resources.Document;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.PSVInfo
{
    internal class PSVManager : IPSVManager
    {
        IUnitOfWork uow = null;
        IProfileRepository profileRepository = null;
        IDocumentsManager documentManager = null;
        ProfileDocumentManager profileDocumentManager = null;

        public PSVManager(IUnitOfWork uow, IDocumentsManager documentManager)
        {
            this.uow = uow;
            this.profileRepository = uow.GetProfileRepository();
            this.documentManager = documentManager;
            this.profileDocumentManager = new ProfileDocumentManager(profileRepository, documentManager);
        }

        public List<Entities.Credentialing.Loading.CredentialingInfo> GetAllPSVList()
        {
            try
            {
                var psvRepo = uow.GetGenericRepository<CredentialingInfo>();

                var psvData = psvRepo.Get(p => p.IsDelegated == true, "Plan, Profile.PersonalDetail.ProviderTitles.ProviderType").ToList();

                foreach (var item in psvData)
                {
                    item.Profile.PersonalDetail.ProviderTitles = item.Profile.PersonalDetail.ProviderTitles.
                        Where(s => (s.Status != StatusType.Inactive.ToString())).ToList();
                }
                
                return psvData;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PSVManagerException(ExceptionMessage.ALL_PSV_GET_EXCEPTION, ex); 
            }
            
        }

        public async Task<Entities.Credentialing.Loading.CredentialingInfo> GetProfileVerificationData(int credentialingInfoId)
        {

            var includeProperties = new string[]
                {
                    "Plan",
                    "Profile.PersonalDetail.ProviderTitles.ProviderType",
                    "Profile.FederalDEAInformations",
                    "Profile.CDSCInformations",
                    
                    "Profile.SpecialtyDetails.Specialty",
                    "Profile.SpecialtyDetails.SpecialtyBoardCertifiedDetail.SpecialtyBoard",

                    //State License
                    "Profile.StateLicenses.ProviderType",
                    "Profile.StateLicenses.StateLicenseStatus",
                    
                };

            try
            {
                var psvRepo = uow.GetGenericRepository<CredentialingInfo>();

                var psvData = await psvRepo.FindAsync(p => p.CredentialingInfoID == credentialingInfoId, includeProperties);

                psvData.Profile.StateLicenses = psvData.Profile.StateLicenses.Where(s => (s.Status != StatusType.Inactive.ToString())).ToList();
                psvData.Profile.CDSCInformations = psvData.Profile.CDSCInformations.Where(c => (c.Status != StatusType.Inactive.ToString())).ToList();
                psvData.Profile.SpecialtyDetails = psvData.Profile.SpecialtyDetails.Where(s => (s.Status != StatusType.Inactive.ToString())).ToList();
                psvData.Profile.FederalDEAInformations = psvData.Profile.FederalDEAInformations.Where(c => !c.Status.Equals(StatusType.Inactive.ToString())).ToList();

                return psvData;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PSVManagerException(ExceptionMessage.PROFILE_VERIFICATIONDATA_GET_EXCEPTION, ex);
            }
            
        }

        public int? AddVerifiedData(int credVerificationInfoId, int profileId, Entities.Credentialing.PSVInformation.ProfileVerificationDetail verificationData, DocumentDTO verificationDocument, string userAuthId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                verificationData.VerificationResult.VerificationDocumentPath = AddDocument(DocumentRootPath.PSV_DOCUMENT_PATH, DocumentTitle.VERIFIED_DOCUMENT, null, verificationDocument, profileId);

                var psvRepo = uow.GetGenericRepository<CredentialingVerificationInfo>();

                var credverificationData = psvRepo.Find(c => c.CredentialingVerificationInfoId == credVerificationInfoId, "ProfileVerificationInfo.ProfileVerificationDetails");

                if (credverificationData.ProfileVerificationInfoId == null)
                {
                    ProfileVerificationInfo profileVerificationInfo = new ProfileVerificationInfo();
                    profileVerificationInfo.ProfileVerificationStatus = AHC.CD.Entities.MasterData.Enums.ProfileVerificationStatusType.InProgress.ToString();
                    profileVerificationInfo.ProfileID = profileId;

                    if (profileVerificationInfo.ProfileVerificationDetails == null)
                    {
                        profileVerificationInfo.ProfileVerificationDetails = new List<ProfileVerificationDetail>();

                        verificationData.VerificationDate = DateTime.Now;
                        verificationData.VerifiedById = user.CDUserID;
                        profileVerificationInfo.ProfileVerificationDetails.Add(verificationData);
                    }
                    else
                    {
                        verificationData.VerificationDate = DateTime.Now;
                        verificationData.VerifiedById = user.CDUserID;
                        profileVerificationInfo.ProfileVerificationDetails.Add(verificationData);
                    }
                    
                    //var profileVerificationRepo = uow.GetGenericRepository<ProfileVerificationInfo>();
                    //profileVerificationRepo.Create(profileVerificationInfo);
                    //profileVerificationRepo.Save();
                    credverificationData.ProfileVerificationInfo = profileVerificationInfo;
                    
                }
                else
                {
                    verificationData.VerificationDate = DateTime.Now;
                    verificationData.VerifiedById = user.CDUserID;
                    credverificationData.ProfileVerificationInfo.ProfileVerificationDetails.Add(verificationData);
                    
                }

                psvRepo.Update(credverificationData);
                psvRepo.Save();

                
                return credverificationData.ProfileVerificationInfoId;
                
                
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PSVManagerException(ExceptionMessage.SAVE_PROFILE_VERIFIED_DATA_EXCEPTION, ex);
            }
            
        }
        
        public int InitiateNewPSV(int credInfoId, string userAuthId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credentialingInfo = credentialingInfoRepo.Find(c => c.CredentialingInfoID == credInfoId);

                
                var psvRepo = uow.GetGenericRepository<CredentialingVerificationInfo>();

                CredentialingVerificationInfo verification = new CredentialingVerificationInfo();
                verification.CredentialingVerificationStartDate = DateTime.Now;
                verification.VerifiedById = user.CDUserID;

                //var psvData = psvRepo.Create(verification);
                //psvRepo.Save();



                if (credentialingInfo.CredentialingVerificationInfos == null)
                {
                    credentialingInfo.CredentialingVerificationInfos = new List<CredentialingVerificationInfo>();
                    credentialingInfo.CredentialingVerificationInfos.Add(verification);
                }
                else
                {
                    credentialingInfo.CredentialingVerificationInfos.Add(verification);
                }

                

                credentialingInfoRepo.Update(credentialingInfo);
                credentialingInfoRepo.Save();

                //AssignInitiatePSVStatus(credInfoId, verification.CredentialingVerificationInfoId, user.CDUserID);

                return verification.CredentialingVerificationInfoId;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PSVManagerException(ExceptionMessage.INITIATE_PSV_EXCEPTION, ex); 
            }            
        }
        
        public List<Entities.Credentialing.PSVInformation.ProfileVerificationDetail> GetPSVReport(int credentialingInfoId)
        {
            try
            {
                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credentialingInfo = credentialingInfoRepo.Find(x => x.CredentialingInfoID == credentialingInfoId, "CredentialingVerificationInfos,CredentialingVerificationInfos.ProfileVerificationInfo,CredentialingVerificationInfos.ProfileVerificationInfo.ProfileVerificationDetails,CredentialingVerificationInfos.ProfileVerificationInfo.ProfileVerificationDetails.VerificationResult,CredentialingVerificationInfos.ProfileVerificationInfo.ProfileVerificationDetails.ProfileVerificationParameter");
                ProfileVerificationInfo latest = null;

                if (credentialingInfo.CredentialingVerificationInfos.Count == 0)
                {
                    throw new PSVManagerException("No PSV is available");
                }
                if (credentialingInfo.CredentialingVerificationInfos.Count > 1)
                {
                    var completedPSV = credentialingInfo.CredentialingVerificationInfos.Where(c => c.CredentialingVerificationEndDate != null).ToList();

                    if (completedPSV.Count == 0)
                    {
                        throw new PSVManagerException("No PSV is available");
                    }
                    else if (completedPSV.Count == 1)
                    {
                        latest = completedPSV.First().ProfileVerificationInfo;
                    }
                    else if (completedPSV.Count > 1)
                    {
                        latest = completedPSV.OrderByDescending(x => x.LastModifiedDate).First().ProfileVerificationInfo;
                    }
                    
                }
                else if (credentialingInfo.CredentialingVerificationInfos.Count == 1)
                {
                    latest = credentialingInfo.CredentialingVerificationInfos.First().ProfileVerificationInfo;
                }
                
                if (latest.ProfileVerificationStatus.Equals(ProfileVerificationStatusType.Completed.ToString()))
                {

                    return latest.ProfileVerificationDetails.ToList();
                }

                else
                    throw new PSVManagerException(ExceptionMessage.PSV_INPROGRESS_EXCEPTION);
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new PSVManagerException(ExceptionMessage.GET_VERIFIED_EXCEPTION, ex);
            }
           
        }

        public void SetAllVerified(int credinfoId, int credVerificationId, string userAuthId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                var credVerificationRepo = uow.GetGenericRepository<CredentialingVerificationInfo>();
                var credVerifiedData = credVerificationRepo.Find(c => c.CredentialingVerificationInfoId == credVerificationId, "ProfileVerificationInfo");
                credVerifiedData.CredentialingVerificationEndDate = DateTime.Now;
                credVerifiedData.VerifiedById = user.CDUserID;
                if (credVerifiedData.ProfileVerificationInfo != null)
                {
                    credVerifiedData.ProfileVerificationInfo.ProfileVerificationStatus = AHC.CD.Entities.MasterData.Enums.ProfileVerificationStatusType.Completed.ToString();
                }
                else
                {
                    throw new PSVManagerException(ExceptionMessage.SET_ALLVERIFIED_BEFORE_COMPLETE_EXCEPTION);
                }
                
                credVerificationRepo.Update(credVerifiedData);

                credVerificationRepo.Save();

                //AssignCompletedPSVStatus(credinfoId, credVerificationId, user.CDUserID);

            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PSVManagerException(ExceptionMessage.SET_ALLVERIFIED_EXCEPTION, ex);
            }
            
        }

        public CredentialingVerificationInfo GetPendingPSV(int credentialingInfoId)
        {
            try
            {
                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credentialingInfo = credentialingInfoRepo.Find(x => x.CredentialingInfoID == credentialingInfoId, "CredentialingVerificationInfos,CredentialingVerificationInfos.ProfileVerificationInfo,CredentialingVerificationInfos.ProfileVerificationInfo.ProfileVerificationDetails,CredentialingVerificationInfos.ProfileVerificationInfo.ProfileVerificationDetails.VerificationResult,CredentialingVerificationInfos.ProfileVerificationInfo.ProfileVerificationDetails.ProfileVerificationParameter");
                CredentialingVerificationInfo latest = null;

                if (credentialingInfo.CredentialingVerificationInfos.Count == 0)
                {
                    throw new PSVManagerException("No pending PSV is available");
                }
                if (credentialingInfo.CredentialingVerificationInfos.Count > 1)
                {
                    latest = credentialingInfo.CredentialingVerificationInfos.OrderByDescending(x => x.LastModifiedDate).First();
                }
                else if (credentialingInfo.CredentialingVerificationInfos.Count == 1)
                {
                    latest = credentialingInfo.CredentialingVerificationInfos.First();
                }

                if (latest.ProfileVerificationInfo.ProfileVerificationStatus == AHC.CD.Entities.MasterData.Enums.ProfileVerificationStatusType.InProgress.ToString())
                {
                    return latest;
                }
                else
                {
                    throw new PSVManagerException("No pending PSV is available");
                }
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PSVManagerException(ExceptionMessage.GET_PENDING_PSV_EXCEPTION, ex);
            }
            
        }

        public int? UpdateVerifiedData(int credVerificationInfoId,int profileId, ProfileVerificationDetail verificationData, DocumentDTO verificationDocument, string userAuthId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                
                verificationData.VerificationDate = DateTime.Now;
                verificationData.VerifiedById = user.CDUserID;
                var verificationRepo = uow.GetGenericRepository<ProfileVerificationDetail>();
                var psvData = verificationRepo.Find(p => p.ProfileVerificationDetailId == verificationData.ProfileVerificationDetailId, "VerificationResult");
                
                if (psvData == null)
                {
                    verificationData.VerificationResult.VerificationDocumentPath = AddDocument(DocumentRootPath.PSV_DOCUMENT_PATH, DocumentTitle.VERIFIED_DOCUMENT, null, verificationDocument, profileId);

                    var psvRepo = uow.GetGenericRepository<CredentialingVerificationInfo>();

                    var credverificationData = psvRepo.Find(c => c.CredentialingVerificationInfoId == credVerificationInfoId, "ProfileVerificationInfo.ProfileVerificationDetails");
                    credverificationData.ProfileVerificationInfo.ProfileVerificationDetails.Add(verificationData);
                    psvRepo.Update(credverificationData);
                    psvRepo.Save();
                }
                else
                {
                    string oldFilePath = verificationData.VerificationResult.VerificationDocumentPath;
                    verificationData.VerificationResult.VerificationDocumentPath = AddUpdateDocument(DocumentRootPath.PSV_DOCUMENT_PATH, oldFilePath, DocumentTitle.VERIFIED_DOCUMENT, null, verificationDocument, profileId);

                    psvData = AutoMapper.Mapper.Map<ProfileVerificationDetail, ProfileVerificationDetail>(verificationData, psvData);

                    verificationRepo.Update(psvData);
                    verificationRepo.Save();
                }



                return verificationData.ProfileVerificationDetailId;
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new PSVManagerException(ExceptionMessage.UPDATE_PROFILE_VERIFIED_DATA_EXCEPTION, ex); 
            }
            
            
        }

        #region Private Methods

        private void AssignInitiatePSVStatus(int credInfoId,int credVerificationId, int userId)
        {
            var psvRepo = uow.GetGenericRepository<CredentialingInfo>();

            var credentialingData = psvRepo.Find(c => c.CredentialingInfoID == credInfoId, "CredentialingLogs, CredentialingLogs.CredentialingActivityLogs");
            CredentialingLog credentialingLog = null;
            if (credentialingData.CredentialingLogs.Count > 1)
            {
                credentialingLog = credentialingData.CredentialingLogs.OrderBy(l => l.LastModifiedDate).First();
            }
            else
            {
                credentialingLog = credentialingData.CredentialingLogs.First();
            }

            CredentialingActivityLog activity = new CredentialingActivityLog();
            activity.ActivityByID = userId;
            activity.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.PSV;
            activity.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.InProgress;

            var verificationRepo = uow.GetGenericRepository<CredentialingVerificationInfo>();
            var verificationData = verificationRepo.Find(v => v.CredentialingVerificationInfoId == credVerificationId);            

            credentialingLog.CredentialingVerificationInfo = AutoMapper.Mapper.Map<CredentialingVerificationInfo, CredentialingVerificationInfo>(verificationData, credentialingLog.CredentialingVerificationInfo);
            credentialingLog.CredentialingActivityLogs.Add(activity);

            psvRepo.Update(credentialingData);
            psvRepo.Save();
        }

        private void AssignCompletedPSVStatus(int credInfoId, int credVerificationId, int userId)
        {
            var psvRepo = uow.GetGenericRepository<CredentialingInfo>();

            var credentialingData = psvRepo.Find(c => c.CredentialingInfoID == credInfoId, "CredentialingLogs, CredentialingLogs.CredentialingActivityLogs");
            CredentialingLog credentialingLog = null;
            if (credentialingData.CredentialingLogs.Count > 1)
            {
                credentialingLog = credentialingData.CredentialingLogs.OrderBy(l => l.LastModifiedDate).First();
            }
            else
            {
                credentialingLog = credentialingData.CredentialingLogs.First();
            }
            CredentialingActivityLog activity = new CredentialingActivityLog();
            activity.ActivityByID = userId;
            activity.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.PSV;
            activity.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;

            var verificationRepo = uow.GetGenericRepository<CredentialingVerificationInfo>();
            var verificationData = verificationRepo.Find(v => v.CredentialingVerificationInfoId == credVerificationId);
            
            credentialingLog.CredentialingVerificationInfo = AutoMapper.Mapper.Map<CredentialingVerificationInfo, CredentialingVerificationInfo>(verificationData, credentialingLog.CredentialingVerificationInfo);
            psvRepo.Update(credentialingData);
            credentialingLog.CredentialingActivityLogs.Add(activity);

            psvRepo.Update(credentialingData);
            psvRepo.Save();
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

        private string AddDocument(string docRootPath, string docTitle, DateTime? expiryDate, DocumentDTO document, int profileId)
        {
            //Create a profile document object
            ProfileDocument profileDocument = CreateProfileDocumentObject(docTitle, null, expiryDate);

            //Assign the Doc root path
            document.DocRootPath = docRootPath;

            //Add the document if uploaded
            return profileDocumentManager.AddPSVDocument(profileId, document, profileDocument);
        }

        private string AddUpdateDocument(string docRootPath, string oldFilePath, string docTitle, DateTime? expiryDate, DocumentDTO document, int profileId)
        {
            document.DocRootPath = docRootPath;
            document.OldFilePath = oldFilePath;

            //Create the profile document object
            ProfileDocument profileDocument = CreateProfileDocumentObject(docTitle, oldFilePath, expiryDate);
            return profileDocumentManager.AddUpdatePSVDocument(profileId, document, profileDocument);
        }

        private string AddUpdateDocumentInformation(string docRootPath, string oldFilePath, string docTitle, DateTime? expiryDate, DocumentDTO document, int profileId)
        {
            document.DocRootPath = docRootPath;
            document.OldFilePath = oldFilePath;

            //Create the profile document object
            ProfileDocument profileDocument = CreateProfileDocumentObject(docTitle, oldFilePath, expiryDate);
            return profileDocumentManager.AddUpdateDocumentInformation(profileId, document, profileDocument);
        }

        #endregion



        
    }
}
