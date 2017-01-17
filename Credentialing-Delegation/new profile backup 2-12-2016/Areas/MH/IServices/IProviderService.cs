using PortalTemplate.Areas.MH.Models.ViewModels.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.IServices
{
    public interface IProviderService
    {
        string GetAllProviderAccounts(string searchTerm);

        string GetAllProviders(string searchTerm);
    }
}