using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Add
{
    public class ProviderCategoryViewModel
    {

        public int categoryId { get; set; }

        public string categoryName { get; set; }

        public List<ProviderTypeViewModel> providerTypes { get; set; }

    }
}