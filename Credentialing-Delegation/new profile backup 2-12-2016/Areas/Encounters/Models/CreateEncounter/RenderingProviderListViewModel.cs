using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.CreateEncounter
{
    public class RenderingProviderListViewModel
    {
        public string ProviderNPI { get; set; }

        public string ProviderFirstName { get; set; }

        public string ProviderMiddleInitial { get; set; }

        public string ProviderLastName { get; set; }

        public string Speciality { get; set; }

        public string TaxanomyCode { get; set; }
    }
}