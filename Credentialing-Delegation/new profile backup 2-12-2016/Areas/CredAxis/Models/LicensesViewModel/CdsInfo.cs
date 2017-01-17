using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.LicensesViewModel
{
    public class CdsInfo
    {
        [Key]
        public int? CdsInfoID { get; set; }


        [Display(Name = "CDS #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string CDS { get; set; }

        [Display(Name = "STATE OF REGISTRATION")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StateOfRegistration { get; set; }

        [Display(Name = "ISSUE DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IssueDate { get; set; }

        [Display(Name = "EXPIRY DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ExpirationDate { get; set; }

        [Display(Name = "SUPPORTING DOCUMENT")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string SupportingDocumentPath { get; set; }
    }
}