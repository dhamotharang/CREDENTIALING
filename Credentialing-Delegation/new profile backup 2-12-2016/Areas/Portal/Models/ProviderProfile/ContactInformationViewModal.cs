using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.ProviderProfile
{
    public class ContactInformationViewModal
    {
        [Display(Name = "CONTACT TYPE")]
        public string Contacttype { get; set; }
        [Display(Name = "CONTACT NAME")]
        public string ContactName { get; set; }
        [Display(Name = "PHONE NUMBER")]
        public string PhoneNumber { get; set; }
        [Display(Name = "FAX NUMBER")]
        public string FaxNumber { get; set; }
        [Display(Name = "EMAIL ADDRESS")]
        public string EmailAddress { get; set; }
        [Display(Name = "ADDRESS1")]
        public string Address1 { get; set; }
        [Display(Name = "ADDRESS2")]
        public string Address2 { get; set; }
        [Display(Name = "CITY")]
        public string City { get; set; }
        [Display(Name = "STATE")]
        public string STATE { get; set; }
        [Display(Name = "COUNTRY")]
        public string Country { get; set; }
        [Display(Name = "ZIP CODE")]
        public int Zip { get; set; }

    }
}