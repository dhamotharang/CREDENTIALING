using AHC.CD.Data.ADO.Credentialing.Loading;
using AHC.CD.Data.ADO.DTO;
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
   public  class ContractManager : IContractManager
    {
        private IContractGridRepository contractrepo = null;
        public ICredentialingContractRepository contractgridrepo=null;
        //IUnitOfWork uow = null;

        public ContractManager(IUnitOfWork uow,ICredentialingContractRepository credentialingContractRepository)
        {
            this.contractrepo = uow.GetContractGridRepository();
            this.contractgridrepo = credentialingContractRepository;
        }
        public List<AHC.CD.Data.ADO.DTO.ContractGridForProfileDTO> GetAllActiveContractGridInfoByID(int profileid)
        {
            var result =  contractgridrepo.GetAllActiveContractGridByID(profileid);
            return result;
        }

        public List<ContractGridForProfileDTO> GetAllInactiveContractGridByID(int ProfileID)
        {
            var result = contractgridrepo.GetAllInactiveContractGridByID(ProfileID);
            return result;
        }

    }
}
