using AHC.CD.Entities.Credentialing.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.CnD
{
    public interface IApplicationManager
    {
        Task<CredentialingInfo> GetCredentialingInfoByIdAsync(int credInfo);
        Task<object> GetCredentialingInfoByProfileId(int profileID);
        Task<object> GetCredentialingInfoById(int credInfoID);
        Task<CredentialingInfo> GetCredentialingFilterInfoByIdAsync(int credInfo);

        Task<string> SetCredentialingInfoStatusById(int credInfoID,string authId);
    }
}
