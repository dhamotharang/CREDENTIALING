using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.LicensesViewModel
{
    public class StateLicense
    {
        [Key]
        public int? StateLicenseID { get; set; }

        [Display(Name = "LICENSE #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LicenseID { get; set; }


        [Display(Name = "LICENSE TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LicenseType { get; set; }

        [Display(Name = "LICENSE STATUS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LicenseStatus { get; set; }

        [Display(Name = "ISSUE STATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IssueState { get; set; }

        [Display(Name = "ISSUE DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IssueDate { get; set; }

        [Display(Name = "CURRENT ISSUE DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CurrentIssueDate { get; set; }

        [Display(Name = "EXPIRY DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ExpirationDate { get; set; }

        [Display(Name = "ARE YOU CURRENTLY PRACTICING IN THIS STATE ?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IsThisState { get; set; }

        [Display(Name = "LICENSE IN GOOD STANDING ?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IsLicenseGood { get; set; }

        [Display(Name = "SUPPORTING DOCUMENT")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string SupportingDocumentPath { get; set; }

        //public List<GroupInformationViewModel> groupInformation { get; set; }
        
    }
}