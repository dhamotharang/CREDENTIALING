using PortalTemplate.Areas.MH.Common;
using PortalTemplate.Areas.MH.IServices;
using PortalTemplate.Areas.MH.Models.ViewModels.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.MH.Services
{
    public class ProviderService : IProviderService
    {
        CommonMethods commonMethods = new CommonMethods();

        public string GetAllProviderAccounts(string searchTerm)
        {
            try
            {
                if (searchTerm == null) { searchTerm = ""; }
                var data = commonMethods.GetMasterDataFromJson<ProviderAccountViewModel>(searchTerm, "~/Areas/MH/Resources/ServiceData/Provider/AllIPAs.JSON", "AccountName");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string GetAllProviders(string searchTerm)
        {
            try
            {
                if (searchTerm == null) { searchTerm = ""; }
                var data = commonMethods.GetMasterDataFromJson<ProviderSelectViewModel>(searchTerm, "~/Areas/MH/Resources/ServiceData/Provider/ProviderServiceData.JSON", "NPI");
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}