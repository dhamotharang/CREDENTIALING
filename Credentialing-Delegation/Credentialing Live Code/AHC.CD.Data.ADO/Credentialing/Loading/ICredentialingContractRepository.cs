using AHC.CD.Data.ADO.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Credentialing.Loading
{
    public interface ICredentialingContractRepository
    {
        Task<IEnumerable<AHC.CD.Data.ADO.DTO.ContractGridDTO>> GetAllContractGrid();
       List<ContractGridForProfileDTO> GetAllActiveContractGridByID(int ProfileID);
        Task<IEnumerable<AHC.CD.Data.ADO.DTO.ContractGridDTO>> GetAllInactiveContractGrid();
        //Task<AHC.CD.Data.ADO.DTO.ContractGridDTO> GetAllInactiveContractGridByID(int ProfileID);  
        List<ContractGridForProfileDTO> GetAllInactiveContractGridByID(int ProfileID);

    }
}
