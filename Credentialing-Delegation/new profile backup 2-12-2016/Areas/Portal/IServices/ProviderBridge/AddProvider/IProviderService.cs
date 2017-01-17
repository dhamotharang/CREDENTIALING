using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using PortalTemplate.Areas.Portal.Models.ProviderBridge.AddProvider;

namespace PortalTemplate.Areas.Portal.IServices.ProviderBridge.AddProvider
{
    public interface IProviderService
    {
        List<ProviderViewModal> GetAllProviders();
        List<ProviderViewModal> SearchProvider(string name);

        #region New Abstraction

        AddProviderViewModel AddProvider(AddProviderViewModel Provider);
        AddProviderViewModel EditProvider(AddProviderViewModel Provider);
        
        #endregion
    }
}
