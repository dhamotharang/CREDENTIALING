using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.Credentialing.LoadingInformation;

namespace AHC.CD.Business.Credentialing
{
    public interface IIndividualCredentialingManager
    {

        #region Initiate Credentialing

        Task InitiateCredentialingAsync(CredentialingInfo credentialingInitiationInfo, string userAuthId);
        Task InitiateReCredentialingAsync(int credentialingInfoID, CredentialingLog credentialingLog);
        Task InitiateDeCredentialingAsync(int credentialingInfoID, CredentialingLog credentialingLog);
        Task AddLoadedContractHistory(LoadedContractHistory loadedContractHistory);
        Task<IEnumerable<CredentialingInfo>> GetAllCredentialingsAsync();
        Task<IEnumerable<CredentialingInfo>> GetAllDeCredentialingAsync();

        #endregion

    }
}
