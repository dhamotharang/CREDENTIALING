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
        Task<CredentialingInfo> GetProfileVerificationData(int credentialingInfoId);
        int? AddVerifiedData(int credInfoId, int profileId, ProfileVerificationDetail verificationData, DocumentDTO verificationDocument, string userAuthId);
        int InitiateNewPSV(int credInfoId, string userAuthId);
        List<ProfileVerificationDetail> GetPSVReport(int credentialingInfoId);
        void SetAllVerified(int credinfoId, int credVerificationId, string userAuthId);        
        CredentialingVerificationInfo GetPendingPSV(int credentialingInfoId);
        int? UpdateVerifiedData(int credVerificationInfoId,int profileId, ProfileVerificationDetail verificationData, DocumentDTO verificationDocument, string userAuthId);
    }
}
