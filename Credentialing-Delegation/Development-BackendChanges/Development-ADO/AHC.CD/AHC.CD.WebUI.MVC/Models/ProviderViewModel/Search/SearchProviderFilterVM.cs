using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Search
{
    public class SearchProviderFilterVM
    {
        public int CategoryID { set; get; }

        public ProviderStatus ProviderStatus { set; get; }
    }
}