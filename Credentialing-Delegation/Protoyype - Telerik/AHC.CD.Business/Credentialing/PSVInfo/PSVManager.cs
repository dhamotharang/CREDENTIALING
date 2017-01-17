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

                var psvData = psvRepo.Get(p => p.IsDelegated.Equals(Entities.MasterData.Enums.YesNoOption.YES.ToString()) && p.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString(), "Plan, Profile.PersonalDetail.ProviderTitles.ProviderType,CredentialingLogs,CredentialingLogs.CredentialingActivityLogs").ToList();
                
                foreach (var item in psvData)
                {
                    CredentialingLog credentialingLog = item.CredentialingLogs.OrderByDescending(c => c.LastModifiedDate).FirstOrDefault();
                    if (credentialingLog != null)
                    {
                        item.CredentialingLogs = new List<CredentialingLog>();
                        item.CredentialingLogs.Add(credentialingLog);
                    }
                    foreach (var data in item.CredentialingLogs)
                    {
                        int count = 0;
                        CredentialingActivityLog credentialingActivityLog=new CredentialingActivityLog();
                        foreach (var data1 in data.CredentialingActivityLogs) 
                        {
                            if(data1.ActivityStatus==AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed.ToString() && data1.Activity==AHC.CD.Entities.MasterData.Enums.ActivityType.CCMAppointment.ToString())
                            {
                                count++;
                                credentialingActivityLog=data1;
                            }
                        }
                        if (count == 0)
                        {
                            data.CredentialingActivityLogs = null;
                        }
                        else
                        {
                            data.CredentialingActivityLogs = new List<CredentialingActivityLog>();
                            data.CredentialingActivityLogs.Add(credentialingActivityLog);
                        }
                    }
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

        public async Task<CredentialingInfo> GetProfileData(int credInfoId)
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

                var psvData = await psvRepo.FindAsync(p => p.CredentialingInfoID == credInfoId, includeProperties);

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

        public async Task<Profile> GetProfileDataByID(int profileId)
        {

            var includeProperties = new string[]
                {
                    
                    "PersonalDetail.ProviderTitles.ProviderType",
                    "FederalDEAInformations",
                    "CDSCInformations",
                    
                    "SpecialtyDetails.Specialty",
                    "SpecialtyDetails.SpecialtyBoardCertifiedDetail.SpecialtyBoard",

                    //State License
                    "StateLicenses.ProviderType",
                    "StateLicenses.StateLicenseStatus",
                    
                };

            try
            {
                //var psvRepo = uow.GetGenericRepository<CredentialingInfo>();

                var profileData = await profileRepository.FindAsync(p => p.ProfileID == profileId, includeProperties);

                profileData.StateLicenses = profileData.StateLicenses.Where(s => (s.Status != StatusType.Inactive.ToString())).ToList();
                profileData.CDSCInformations = profileData.CDSCInformations.Where(c => (c.Status != StatusType.Inactive.ToString())).ToList();
                profileData.SpecialtyDetails = profileData.SpecialtyDetails.Where(s => (s.Status != StatusType.Inactive.ToString())).ToList();
                profileData.FederalDEAInformations = profileData.FederalDEAInformations.Where(c => !c.Status.Equals(StatusType.Inactive.ToString())).ToList();

                return profileData;
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

        public int? AddVerifiedData(int verificationInfoId, int profileId, Entities.Credentialing.PSVInformation.ProfileVerificationDetail verificationData, DocumentDTO verificationDocument, string userAuthId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                verificationData.VerificationResult.VerificationDocumentPath = AddDocument(DocumentRootPath.PSV_DOCUMENT_PATH, DocumentTitle.VERIFIED_DOCUMENT, null, verificationDocument, profileId);

                var psvRepo = uow.GetGenericRepository<ProfileVerificationInfo>();

                var profileVerificationData = psvRepo.Find(c => c.ProfileVerificationInfoId == verificationInfoId, "ProfileVerificationDetails, ProfileVerificationDetails.VerificationResult");

                if (verificationData.VerificationDate==null)
                verificationData.VerificationDate = DateTime.Now;

                if (profileVerificationData.ProfileVerificationDetails == null)
                {
                    profileVerificationData.ProfileVerificationDetails = new List<ProfileVerificationDetail>();

                    if (verificationData.VerificationDate == null)
                    verificationData.VerificationDate = DateTime.Now;
                    verificationData.VerifiedById = user.CDUserID;
                    verificationData.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    profileVerificationData.ProfileVerificationDetails.Add(verificationData);
                }
                else
                {
                    if (verificationData.VerificationDate == null)
                    verificationData.VerificationDate = DateTime.Now;
                    verificationData.VerifiedById = user.CDUserID;
                    verificationData.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    profileVerificationData.ProfileVerificationDetails.Add(verificationData);
                }
                
                psvRepo.Update(profileVerificationData);               
                
                psvRepo.Save();

                return profileVerificationData.ProfileVerificationInfoId;               
                
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

        public ProfileVerificationInfo InitiateNewPSV(int profileId, string userAuthId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                var psvRepo = uow.GetGenericRepository<ProfileVerificationInfo>();
                var verificationInfo = psvRepo.Find(p => p.ProfileID == profileId, "ProfileVerificationDetails, ProfileVerificationDetails.VerificationResult,ProfileVerificationDetails.ProfileVerificationParameter");

                if (verificationInfo == null)
                {
                    ProfileVerificationInfo profileVerificationInfo = new ProfileVerificationInfo();
                    profileVerificationInfo.ProfileID = profileId;
                    profileVerificationInfo.VerifiedById = user.CDUserID;
                    profileVerificationInfo.CredentialingVerificationStartDate = DateTime.Now;
                    profileVerificationInfo.ProfileVerificationDetails = new List<ProfileVerificationDetail>();
                    psvRepo.Create(profileVerificationInfo);
                    psvRepo.Save();

                    return profileVerificationInfo;
                }
                else
                {
                     verificationInfo.ProfileVerificationDetails = verificationInfo.ProfileVerificationDetails.Where(p => p.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();
                     return verificationInfo;
                }
               
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
        
        public List<Entities.Credentialing.PSVInformation.CredentialingProfileVerificationDetail> GetPSVReport(int credentialingInfoId)
        {
            try
            {
                var credentialingInfoRepo = uow.GetGenericRepository<CredentialingInfo>();
                var credentialingInfo = credentialingInfoRepo.Find(x => x.CredentialingInfoID == credentialingInfoId, "CredentialingVerificationInfos, CredentialingVerificationInfos.ProfileVerificationDetails, CredentialingVerificationInfos.ProfileVerificationDetails.VerificationResult, CredentialingVerificationInfos.ProfileVerificationDetails.ProfileVerificationParameter");
                CredentialingVerificationInfo latest = null;

                if (credentialingInfo.CredentialingVerificationInfos.Count == 0)
                {
                    throw new PSVManagerException("No PSV is available");
                }
                if (credentialingInfo.CredentialingVerificationInfos.Count > 1)
                {
                    latest = credentialingInfo.CredentialingVerificationInfos.OrderByDescending(d => d.CredentialingVerificationEndDate).First();                    
                }
                else if (credentialingInfo.CredentialingVerificationInfos.Count == 1)
                {
                    latest = credentialingInfo.CredentialingVerificationInfos.First();
                }

                return latest.ProfileVerificationDetails.ToList();
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

        public void SetAllVerified(int credinfoId, int credVerificationId, string userAuthId, List<int> verificationIDs)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                var VerificationRepo = uow.GetGenericRepository<ProfileVerificationInfo>();
                var VerifiedData = VerificationRepo.Find(c => c.ProfileVerificationInfoId == credVerificationId, "ProfileVerificationDetails, ProfileVerificationDetails.VerificationResult");
                VerifiedData.CredentialingVerificationEndDate = DateTime.Now;
                VerifiedData.VerifiedById = user.CDUserID;

                foreach (var data in VerifiedData.ProfileVerificationDetails)
                {
                    data.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                }
                
                foreach (var id in verificationIDs)
                {
                    foreach (var data in VerifiedData.ProfileVerificationDetails)
                    {
                        if (id == data.ProfileVerificationDetailId)
                            data.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                    }                    
                }
               

                VerificationRepo.Update(VerifiedData);
                VerificationRepo.Save();

                VerifiedData.ProfileVerificationDetails = VerifiedData.ProfileVerificationDetails.Where(p => p.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).ToList();
                
                var credInfoRepo = uow.GetGenericRepository<CredentialingInfo>();

                var credData = credInfoRepo.Find(c => c.CredentialingInfoID == credinfoId, "CredentialingVerificationInfos, CredentialingVerificationInfos.ProfileVerificationDetails, CredentialingVerificationInfos.ProfileVerificationDetails.VerificationResult");
                

                CredentialingVerificationInfo credVerificationInfoData = null;               

                credVerificationInfoData = AutoMapper.Mapper.Map<ProfileVerificationInfo, CredentialingVerificationInfo>(VerifiedData);
                credData.CredentialingVerificationInfos.Add(credVerificationInfoData);
                
                credInfoRepo.Update(credData);
                credInfoRepo.Save();

                AssignCompletedPSVStatus(credinfoId, user.CDUserID);

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

        public int? UpdateVerifiedData(int VerificationInfoId, int profileId, ProfileVerificationDetail verificationData, DocumentDTO verificationDocument, string userAuthId)
        {
            try
            {
                var userRepo = uow.GetGenericRepository<CDUser>();
                var user = userRepo.Find(u => u.AuthenicateUserId == userAuthId);

                if (verificationData.VerificationDate == null)
                verificationData.VerificationDate = DateTime.Now;
                verificationData.VerifiedById = user.CDUserID;
                verificationData.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;

                if (verificationDocument.FileName != null)
                {
                    verificationData.VerificationResult.VerificationDocumentPath = AddDocument(DocumentRootPath.PSV_DOCUMENT_PATH, DocumentTitle.VERIFIED_DOCUMENT, null, verificationDocument, profileId);
                }
                
                
                var verificationRepo = uow.GetGenericRepository<ProfileVerificationDetail>();
                var psvData = verificationRepo.Find(p => p.ProfileVerificationDetailId == verificationData.ProfileVerificationDetailId, "VerificationResult");

                if (psvData != null)
                {
                    psvData.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                    verificationRepo.Update(psvData);
                    verificationRepo.Save();
                }  

                var psvRepo = uow.GetGenericRepository<ProfileVerificationInfo>();

                var profileVerificationData = psvRepo.Find(c => c.ProfileVerificationInfoId == VerificationInfoId, "ProfileVerificationDetails");
                profileVerificationData.ProfileVerificationDetails.Add(verificationData);
                
                psvRepo.Update(profileVerificationData);
                psvRepo.Save();

                              

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

        public List<ProfileVerificationDetail> GetPSVHistory(int profileId)
        {
            try
            {
                var psvRepo = uow.GetGenericRepository<ProfileVerificationInfo>();
                var verificationInfo = psvRepo.Find(p => p.ProfileID == profileId, "ProfileVerificationDetails, ProfileVerificationDetails.VerificationResult,ProfileVerificationDetails.ProfileVerificationParameter");

                verificationInfo.ProfileVerificationDetails = verificationInfo.ProfileVerificationDetails.Where(p => p.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()).ToList();

                return verificationInfo.ProfileVerificationDetails.ToList();
            }
            catch (Exception)
            {
                throw new PSVManagerException("Unable to get psv history for the given profile id");
            }
            
            
        }

        public ProfileVerificationInfo GetPSVDetailsForAProvider(int profileId)
        {
            try
            {
                var psvInfo = uow.GetGenericRepository<ProfileVerificationInfo>();
              
                var verificationInfo = psvInfo.Find(p => p.ProfileID == profileId, "ProfileVerificationDetails, ProfileVerificationDetails.VerificationResult,ProfileVerificationDetails.ProfileVerificationParameter");

                return verificationInfo;
            }
            catch (Exception)
            {
                throw new PSVManagerException("Unable to get psv history for the given profile id");
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

        private void AssignCompletedPSVStatus(int credInfoId, int userId)
        {
            var psvRepo = uow.GetGenericRepository<CredentialingInfo>();

            var credentialingData = psvRepo.Find(c => c.CredentialingInfoID == credInfoId, "CredentialingLogs, CredentialingLogs.CredentialingActivityLogs, CredentialingVerificationInfos, CredentialingVerificationInfos.ProfileVerificationDetails, CredentialingVerificationInfos.ProfileVerificationDetails.VerificationResult");
            CredentialingLog credentialingLog = null;
            
            if (credentialingData.CredentialingLogs.Count > 1)
            {
                credentialingLog = credentialingData.CredentialingLogs.OrderByDescending(l => l.LastModifiedDate).First();
            }
            else if (credentialingData.CredentialingLogs.Count == 1)
            {
                credentialingLog = credentialingData.CredentialingLogs.First();
            }
            CredentialingActivityLog activity = new CredentialingActivityLog();
            activity.ActivityByID = userId;
            activity.ActivityType = AHC.CD.Entities.MasterData.Enums.ActivityType.PSV;
            activity.ActivityStatusType = AHC.CD.Entities.MasterData.Enums.ActivityStatusType.Completed;

            //var verificationRepo = uow.GetGenericRepository<CredentialingVerificationInfo>();

            if (credentialingData.CredentialingVerificationInfos.Count == 1)
            {
                credentialingLog.CredentialingVerificationInfo = credentialingData.CredentialingVerificationInfos.First();
            }
            else if (credentialingData.CredentialingVerificationInfos.Count > 1)
            {
                credentialingLog.CredentialingVerificationInfo = credentialingData.CredentialingVerificationInfos.OrderByDescending(l => l.LastModifiedDate).First();
            }

            //var verificationData = verificationRepo.Find(v => v.CredentialingVerificationInfoId == credVerificationId);
            
            //credentialingLog.CredentialingVerificationInfo = AutoMapper.Mapper.Map<CredentialingVerificationInfo, CredentialingVerificationInfo>(verificationData, credentialingLog.CredentialingVerificationInfo);
            
            
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
