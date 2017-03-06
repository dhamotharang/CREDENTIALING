using AHC.CD.Data.ADO.DTO;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface IContractManager
    {
        //Add Contract to a profile
        //Update a contract

        //Add Contract Group to a contract
        //Update Contract Group

        List<AHC.CD.Data.ADO.DTO.ContractGridForProfileDTO> GetAllActiveContractGridInfoByID(int profileid);
        List<ContractGridForProfileDTO> GetAllInactiveContractGridByID(int ProfileID);
    }
}
