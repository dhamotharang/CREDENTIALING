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
        int InitiateReCredentialingAsync(int credentialingInfoID, CredentialingInfo credentialingInitiationInfo, string userAuthId, int[] CredentialingContractRequestsArray);
        int InitiateDeCredentialingAsync(int credentialingInfoID, int contractRequestId, int gridId, string userAuthId);
        void CompleteDeCredentialingAsync(int[] credentialingInfoIDs, string userAuthId);
        Task AddLoadedContractHistory(LoadedContractHistory loadedContractHistory);
        Task<IEnumerable<CredentialingInfo>> GetAllCredentialingsAsync();
        Task<IEnumerable<CredentialingInfo>> GetAllReCredentialingAsync();
        Task<IEnumerable<CredentialingInfo>> GetAllDeCredentialingAsync();
        Task<IEnumerable<Object>> getAllCredentialinginfoByContractRequest(int ProviderID, int PlanID);
        Task<IEnumerable<Object>> getAllCredentialinginfoByContractGrid(int ProviderID, int PlanID);
        Task<IEnumerable<Plan>>  getAllPlanListforCredentialinginfoByContractRequest(int ProviderID);
        Task<IEnumerable<CredentialingInfo>> GetCredInfoAsync(int credentialingInfoID);

        Task<IEnumerable<Object>> getCredentialingContractRequestForAllPlan(int ProviderID, int[] PlanIDs);

        #endregion

        Task<List<CredentialingInfo>> InitiateDrop(int CDUserId, int[] InfoidArray);
    }
}
