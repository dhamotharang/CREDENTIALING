using AHC.CD.Business.BusinessModels.ProfileUpdates;
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
        Task SetApproval(ApprovalSubmission tracker, string userAuthId);

        int AddProfileUpdateForProvider<TObject, T>(TObject t,T t1, ProfileUpdateTrackerBusinessModel tracker) where TObject : class where T : class;
        List<ProfileUpdatedData> GetDataById(int trackerId);
        
    }
}
