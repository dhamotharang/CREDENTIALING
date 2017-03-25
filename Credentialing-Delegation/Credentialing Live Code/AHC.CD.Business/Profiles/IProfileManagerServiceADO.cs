using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.Credentialing.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface IProfileManagerServiceADO
    {
        IEnumerable<ProviderDTO> GetAllProviders();
        IEnumerable<ProfileAndPlanDTO> GetAllCredentialingPlansForProviders();		
    }
}
