using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository.ContractGrid
{
    public interface IContractGridRepository
    {
        List<Entities.Credentialing.DTO.ContractGridDTO> GetAllContractGridInfoes(int profileid);
    }
}
