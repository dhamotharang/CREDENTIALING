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

        void InitiateCredentialingAsync(CredentialingInfo credentialingInitiationInfo, string userAuthId);
        int InitiateReCredentialingAsync(int credentialingInfoID, CredentialingInfo credentialingInitiationInfo, string userAuthId, int[] CredentialingContractRequestsArray, int[] NonCredentialingContractRequestsArray);
        int InitiateDeCredentialingAsync(int count,int credentialingInfoID, int contractRequestId, int gridId, string userAuthId, CredentialingLog credentialingLog);
        void CompleteDeCredentialingAsync(int[] credentialingInfoIDs, string userAuthId);
        Task AddLoadedContractHistory(LoadedContractHistory loadedContractHistory);
        Task<IEnumerable<CredentialingInfo>> GetAllCredentialingsAsync();
        Task<IEnumerable<CredentialingInfo>> GetAllReCredentialingAsync();
        Task<IEnumerable<CredentialingInfo>> GetAllDeCredentialingAsync();
        Task<IEnumerable<CredentialingInfo>> getAllCredentialinginfoByContractRequest(int ProviderID, int PlanID);
        Task<IEnumerable<Plan>>  getAllPlanListforCredentialinginfoByContractRequest(int ProviderID);
        Task<IEnumerable<CredentialingInfo>> GetCredInfoAsync(int credentialingInfoID);

        #endregion

    }
}
