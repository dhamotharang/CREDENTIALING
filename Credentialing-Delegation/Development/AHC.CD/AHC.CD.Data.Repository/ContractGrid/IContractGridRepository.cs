using AHC.CD.Entities.Credentialing.DTO;
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
       // List<ContractForServiceDto> GetContractsForAprovider(int profileID);
        int GetCountOfActiveContractsForaProvider(int ProfiileID);
        object GetContractsForAproviderGropedbyZipCode(int profileID);
    }
}
