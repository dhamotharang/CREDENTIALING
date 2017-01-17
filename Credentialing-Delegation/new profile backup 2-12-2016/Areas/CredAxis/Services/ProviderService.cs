using Newtonsoft.Json;
using PortalTemplate.Areas.CredAxis.Models.ProviderviewModel;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace PortalTemplate.Areas.CredAxis.Services
{
    public class ProviderService : IProviderService
    {
        public List<ProviderSearchResultViewModel> GetAllProviders(ProviderSearch searchParams)
        {
            List<ProviderSearchResultViewModel> SearchResultData;
            {
                string file = HostingEnvironment.MapPath("~/Areas/CredAxis/Resources/CredAxisJson/ProviderData/Providerdata.json");
                string json = System.IO.File.ReadAllText(file);

                SearchResultData = JsonConvert.DeserializeObject<List<ProviderSearchResultViewModel>>(json);

            }

            return SearchResultData;
        }




    }
}