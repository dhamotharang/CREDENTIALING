using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.Credentialing.Loading;
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
        Task<ContractGrid> AddContractInfoFromPlan(ContractGrid contractGrid, DocumentDTO welcomeLetter, string authId, string ReasonForPanelChange);
        Task<ContractGrid> QuickSaveContractInfoFromPlan(ContractGrid contractGrid, string authId);
        Task<int> RemoveRequestAndGrid(int credentialingContractRequestID);
        Task<int> RemoveGrid(int contractGridID);
        Task<IEnumerable<CredentialingInfo>> GetAllContractGrid();
        Task<IEnumerable<AHC.CD.Data.ADO.DTO.ContractGridDTO>> GetAllContractGridDTO();
        Task<ContractGrid> GetContractGridById(int ContractGridID);
        Task<CredentialingContractInfoFromPlan> ViewContractInfoFromPlan(int ReportID);

        Task<IEnumerable<CredentialingInfo>> GetAllContractGridByID(int ProfileID);
        Task<IEnumerable<ParticipatingStatusType>> GetAllParticipatingStatus();
    }
}
