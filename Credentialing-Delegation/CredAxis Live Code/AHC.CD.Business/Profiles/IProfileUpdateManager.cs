using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.Credentialing.CredentialingRequestTracker;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface IProfileUpdateManager
    {
        //int AddProfileUpdateForProvider(int profileId, StateLicenseInformation stateLicense, string subSectionName, string userId);
        List<ProfileUpdatesTracker> GetAllUpdates();
        List<ProfileUpdatesTracker> GetUpdatesById(int profileId);
        List<ProfileUpdatesTracker> GetUpdatesForProfileById(int profileId);
        void SetApproval(List<ApprovalSubmission> trackers, string userAuthId);

        int AddProfileUpdateForProvider<TObject, T>(TObject t,T t1, ProfileUpdateTrackerBusinessModel tracker) where TObject : class where T : class;
        List<ProfileUpdatedData> GetDataById(int trackerId);
        string SaveDocumentTemporarily(DocumentDTO document, string documentSubPath, string documentTitle, int profileId);
        List<HospitalContactInfo> GetHospitalContactInfoByIds(int[] HospitalContactInfoIds);

        List<ProfileUpdatesTracker> GetAllUpdatesHistory();
        List<ProfileUpdatesTracker> GetUpdatesHistoryById(int profileId);

        List<ProfileUpdatesTracker> GetUpdatesByIdForAllStatus(int profileId);
        List<ProfileUpdatesTracker> GetUpdatesByTrackerId(int trackerId, string status, string modificationType);
        void CredentialingRequestTrackerSetApprovalAsync(CredentialingRequestTracker credentialingRequestTracker);

        int GetAllRequestCounts();
        int GetAllProviderApprovalCounts(int profileId);

        Task<T> GetProfileDataByID<T>(T t, int ID) where T : class;
        Task<string> GetUserName(int trackerID);
    }
}
