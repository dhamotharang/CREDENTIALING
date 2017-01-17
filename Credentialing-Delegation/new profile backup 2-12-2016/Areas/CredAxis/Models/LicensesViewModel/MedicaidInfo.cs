using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.LicensesViewModel
{
    public class MedicaidInfo
    {
        [Key]
        public int? MedicaidInfoID { get; set; }
        
        [Display(Name = "MEDICAID #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MedicaidID { get; set; }


        [Display(Name = "ISSUE DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IssueDate { get; set; }

        [Display(Name = "ISSUE STATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IssueState { get; set; }

        [Display(Name = "EXPIRY DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ExpirationDate { get; set; }

        [Display(Name = "SUPPORTING DOCUMENT")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string SupportingDocumentPath { get; set; }
    }
}