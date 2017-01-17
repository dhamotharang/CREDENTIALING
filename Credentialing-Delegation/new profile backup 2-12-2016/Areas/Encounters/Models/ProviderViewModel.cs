using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models
{
    public class ProviderViewModel
    {
        public int ProviderUniqueId { get; set; }
          
        [DisplayName("Provider NPI")]
        public string ProviderNPI { get; set; }

        [DisplayName("Provider First Name")]
        public string ProviderFirstName { get; set; }

        [DisplayName("Provider Last Name")]
        public string ProviderLastName { get; set; }

        public string ProviderMiddleName { get; set; }

        [DisplayName("Specialty")]
        public string Specialty { get; set; }

        public string FacilityName { get; set; }

        [DisplayName("Taxonomy Code")]
        public string Taxonomy { get; set; }

        public bool IsProviderChecked { get; set; }  
    }
}