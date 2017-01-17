using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.PriorAuthorization
{
    public class FacilityViewModel
    {
        public int FacilityId { get; set; }

        public bool FacilityNetwork { get; set; }

        [Display(Name = "FACILITY NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FullName { get; set; }

        public string Name { get; set; }

        [Display(Name="CONTACT NAME")]
        public string ContactName { get; set; }

        [Display(Name = "PHONE NUMBER")]
        public string PhoneNumber { get; set; }

        [Display(Name = "FAX NUMBER")]
        public string FaxNumber { get; set; }

        [Display(Name = "TAX ID")]
        public string TaxID { get; set; }

        [Display(Name = "NPI")]
        public string FacilityNPI { get; set; }

        [Display(Name = "FACILITY TYPE")]
        public string FacilityType { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }
    }
}
