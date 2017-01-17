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
    }
}
