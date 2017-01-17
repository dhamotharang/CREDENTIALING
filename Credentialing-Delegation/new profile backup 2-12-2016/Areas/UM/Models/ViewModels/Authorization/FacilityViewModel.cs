using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class FacilityViewModel
    {
        public string FacilityId { get; set; }

        public bool FacilityNetwork { get; set; }

        [Display(Name = "FACILITY NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        [JsonProperty("FacilityName")]
        public string FullName { get; set; }

        public string Name { get; set; }

        [Display(Name="Contact Name")]
        public string ContactName { get; set; }

        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        [Display(Name = "TAX ID")]
        public string TaxID { get; set; }

        [Display(Name = "NPI")]
        public string FacilityNPI { get; set; }

        public string FacilityType { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }
    }
}
