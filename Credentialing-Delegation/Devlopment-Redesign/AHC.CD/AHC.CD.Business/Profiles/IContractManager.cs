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

        List<ContractGridDTO> GetAllContractGridInfoes(int profileid);
    }
}
