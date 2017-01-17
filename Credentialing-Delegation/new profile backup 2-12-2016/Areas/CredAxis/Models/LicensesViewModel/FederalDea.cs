using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.LicensesViewModel
{
    public class FederalDea
    {
        public FederalDea()
        {
            //Schedule = new FederalDeaScheduleViewModel();
        }
        [Key]
        public int? FederalDeaID { get; set; }

        [Display(Name = "DEA #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string DEA { get; set; }


        [Display(Name = "STATE OF REGISTRATION")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StateOfRegistration { get; set; }

        [Display(Name = "LICENSE IN GOOD STANDING?")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool IsLicenseGood { get; set; }

        [Display(Name = "ISSUE DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IssueDate { get; set; }

        [Display(Name = "EXPIRY DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ExpirationDate { get; set; }

        [Display(Name = "SCHEDULE")]
        [DisplayFormat(NullDisplayText = "-")]
        public FederalDeaScheduleViewModel SCHEDULE { get; set; }

        [Display(Name = "SUPPORTING DOCUMENT")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string SupportingDocumentPath { get; set; }
    }
}