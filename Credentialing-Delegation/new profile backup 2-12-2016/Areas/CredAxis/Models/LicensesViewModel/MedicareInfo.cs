using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.LicensesViewModel
{
    public class MedicareInfo
    {
        [Key]
        public int? MedicareInfoID { get; set; }

        [Display(Name = "MEDICARE #")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MedicareID { get; set; }


        [Display(Name = "ISSUE DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string IssueDate { get; set; }

        [Display(Name = "EXPIRY DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ExpirationDate { get; set; }

        [Display(Name = "GROUP MEDICARE NUMBER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string GroupMedicareNumber { get; set; }
       
        [Display(Name = "GROUP NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string GroupName { get; set; }
      
        [Display(Name = "GROUP TAX ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string GroupTaxID { get; set; }
    
        [Display(Name = "SUPPORTING DOCUMENT")]
        [DisplayFormat(NullDisplayText = "N/A")]
        public string SupportingDocumentPath { get; set; }
    }
}