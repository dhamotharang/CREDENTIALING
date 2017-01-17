using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Search
{
    public class SearchProviderViewModel
    {
        public int ProviderID { get; set; }
        public string Title { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string City { get; set; }

        public string County { get; set; }

        public string State { get; set; }

        public string ProviderType { get; set; }

        public string ProviderRelation { get; set; }

        public string Group { get; set; }
    }
}