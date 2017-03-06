using AHC.CD.Data.ADO.ProviderService;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.Credentialing.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    internal class ProfileManagerServiceADO : IProfileManagerServiceADO
    {
        public IProviderRepository iProviderRepository = null;
        public ProfileManagerServiceADO(IProviderRepository iProviderRepository)
        {
            this.iProviderRepository = iProviderRepository;
        }

        public IEnumerable<ProviderDTO> GetAllProviders()
        {
            try
            {
                return iProviderRepository.getAllProviderData();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<ProfileAndPlanDTO> GetAllCredentialingPlansForProviders()
        {
            try
            {
                return iProviderRepository.getAllProviderAndPalns();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
