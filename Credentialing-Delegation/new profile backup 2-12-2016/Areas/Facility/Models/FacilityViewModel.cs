using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Facility.Models
{
    public class FacilityViewModel
    {
        public FacilityViewModel()
        {
            new FacilityViewModel();
            new ContactInformation();
            new AddressInformation();
        }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FacilityID { get; set; }

        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FacilityName { get; set; }

        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LegalEntity { get; set; }

        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FacilityType { get; set; }

        public ContactInformation Contact { get; set; }
        public AddressInformation Address { get; set; }


    }


    public class ContactInformation
    {
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactInformationID { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactType { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ContactName { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Relationship { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string TelephoneNumber { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Extension { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FaxNumber { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Email { get; set; }

        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FamilyID { get; set; }

    }
    public class AddressInformation
    {
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AddressID { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AddressType { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AddressLine1 { get; set; }

        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AddressLine2 { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }
        [Display(Name = "FACILITY ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CountryCode { get; set; }

    }
}