using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DemographisViewModels
{
    public class PersonalDetailsViewModel
    {
        [Key]
        public int PersonalDetailID { get; set; }


        [Display(Name = "SALUTATION")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Salutations { get; set; }

        [Display(Name = "FIRST NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "MIDDLE NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MiddleName { get; set; }

        [Display(Name = "LAST NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        [Display(Name = "GENDER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Gender { get; set; }

        [Display(Name = "SUFFIX")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Suffix { get; set; }

        [Display(Name = "MARITAL STATUS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MaritalStatus { get; set; }

        [Display(Name = "SPOUSE NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SpouseName { get; set; }

        [Display(Name = "PROVIDER LEVEL")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ProviderLevel { get; set; }

        [Display(Name = "PROVIDER TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ProviderType { get; set; }

        [Display(Name = "HAVE YOU USED OTHER LEGAL NAMES ?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IsLegalName { get; set; }

        [Display(Name = "OTHER LEGAL NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string OtherLegalName { get; set; }
    }
}