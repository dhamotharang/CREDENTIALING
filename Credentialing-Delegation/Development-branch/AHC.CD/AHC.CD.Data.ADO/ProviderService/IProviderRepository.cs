using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.Credentialing.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.ProviderService
{
    public interface IProviderRepository
    {
        IEnumerable<ProviderDTO> getAllProviderData();
        IEnumerable<ProfileAndPlanDTO> getAllProviderAndPalns();
    }
}
