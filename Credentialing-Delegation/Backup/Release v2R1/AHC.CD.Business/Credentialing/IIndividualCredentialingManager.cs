using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing
{
    public interface IIndividualCredentialingManager
    {
        
        #region Initiate Credentialing

            Task<int> InitiateCredentialingAsync(CredentialingDetailsDTO credentialingDetailsDTO);
        
        #endregion
        
    }
}
