using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.ProviderProfileViewModel.PofessionalReferenceViewModel
{
    public class ProfessionalReferenceViewModel
    {

        public string ProfessionalReferenceID { get; set; }


        [Display(Name = "PROVIDER TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ProviderType { get; set; }

        [Display(Name = "FIRST NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "MIDDLE NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MiddleName { get; set; }

        [Display(Name = "LAST NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        [Display(Name = "SUITE/BUILDING")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Suite { get; set; }

        [Display(Name = "STREET/P. O. BOX NO")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StreetNo { get; set; }

        [Display(Name = "CITY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }

        [Display(Name = "STATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        [Display(Name = "COUNTY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }

        [Display(Name = "COUNTRY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Country { get; set; }

        [Display(Name = "ZIP CODE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ZipCode { get; set; }

        [Display(Name = "EMAIL")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Email { get; set; }

        [Display(Name = "TELEPHONE #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Telephone { get; set; }

        [Display(Name = "FAX #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Fax { get; set; }

        [Display(Name = "DEGREE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Degree { get; set; }

        [Display(Name = "SPECIALTY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Specialty { get; set; }

        [Display(Name = "RELATIONSHIP")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Relationship { get; set; }

        [Display(Name = "BOARD CERTIFIED ?")]
        [DisplayFormat(NullDisplayText = "-")]
        public string BoardCertified { get; set; }

       

    }
}