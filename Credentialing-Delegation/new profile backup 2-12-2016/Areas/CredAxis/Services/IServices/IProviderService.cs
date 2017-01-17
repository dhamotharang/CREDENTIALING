using PortalTemplate.Areas.CredAxis.Models.ProviderviewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.CredAxis.Services.IServices
{
   public interface IProviderService
    {
       List<ProviderSearchResultViewModel> GetAllProviders(ProviderSearch searchParams);
    }
}
