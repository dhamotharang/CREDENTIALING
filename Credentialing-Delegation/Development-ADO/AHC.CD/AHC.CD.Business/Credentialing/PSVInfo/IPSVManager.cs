using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.PSVInformation;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.PSVInfo
{
    public interface IPSVManager
    {
        List<CredentialingInfo> GetAllPSVList();
        Task<CredentialingInfo> GetProfileData(int credInfoId);
        Task<Profile> GetProfileDataByID(int credInfoId);
        int? AddVerifiedData(int verificationInfoId, int profileId, Entities.Credentialing.PSVInformation.ProfileVerificationDetail verificationData, DocumentDTO verificationDocument, string userAuthId);
        ProfileVerificationInfo InitiateNewPSV(int profileId, string userAuthId);
        List<CredentialingProfileVerificationDetail> GetPSVReport(int credentialingInfoId);
        void SetAllVerified(int credinfoId, int credVerificationId, string userAuthId, List<int> verificationIDs); 
        int? UpdateVerifiedData(int credVerificationInfoId,int profileId, ProfileVerificationDetail verificationData, DocumentDTO verificationDocument, string userAuthId);
        List<ProfileVerificationDetail> GetPSVHistory(int profileId);
    }
}
