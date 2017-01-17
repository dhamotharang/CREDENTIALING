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
        Task<AHC.CD.Data.ADO.DTO.ContractGridDTO> GetAllContractGridByID(int ProfileID); 
    }
}
