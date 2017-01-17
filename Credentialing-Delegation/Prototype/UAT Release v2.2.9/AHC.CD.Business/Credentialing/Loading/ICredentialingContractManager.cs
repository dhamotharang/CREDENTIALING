using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.Loading
{
    public interface ICredentialingContractManager
    {
        Task<CredentialingContractRequest> AddCredentialingContractRequest(int credentialingInfoID, CredentialingContractRequest credentialingContractRequest, string userAuthID);
        Task<List<ContractGrid>> GetContractGridForCredentialingInfo(int credentialingInfoID);
        Task<ContractGrid> AddContractInfoFromPlan(ContractGrid contractGrid, DocumentDTO welcomeLetter);
        Task<ContractGrid> QuickSaveContractInfoFromPlan(ContractGrid contractGrid);
        Task<int> RemoveRequestAndGrid(int credentialingContractRequestID);
        Task<int> RemoveGrid(int contractGridID);
    }
}
