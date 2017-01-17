using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;

namespace PortalTemplate.Areas.Portal.IManager.Provider
{
    public interface IProviderServiceManager
    {
        List<ProviderViewModal> GetAllProviders();
    }
}
