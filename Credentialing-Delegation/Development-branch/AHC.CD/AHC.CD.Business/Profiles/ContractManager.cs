using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.ContractGrid;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    class ContractManager : IContractManager
    {
        private IContractGridRepository contractrepo = null;
        IUnitOfWork uow = null;

        public ContractManager(IUnitOfWork uow)
        {
            this.contractrepo = uow.GetContractGridRepository();  
        }
        public List<ContractGridDTO> GetAllContractGridInfoes(int profileid)
        {
            var result = contractrepo.GetAllContractGridInfoes(profileid);
            return result;
        }
    }
}
