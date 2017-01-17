using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class StateLicenseViewModel
    {
        public int StateLicenseID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        [Display(Name = "License Number *")]

        public string LicenseNumber { get; set; }
        [Display(Name = "State License Type")]

        public string StateLicenseType { get; set; }
        [Display(Name = "State License Status")]

        public string StateLicenseStatus { get; set; }

        [Required]
        [Display(Name = "Issue State *")]

        public string IssuingState { get; set; }

        [Required]
        [Display(Name = "Current Practice State *")]

        public string PracticeState { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Issue Date *")]

        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Expiry Date *")]

        public DateTime ExpiryDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Current Issue Date *")]

        public DateTime CurrentIssueDate { get; set; }

        [Required]

        [Display(Name = "License in good standing? *")]

        public bool LicenseInGoodStanding { get; set; }

        //public bool IsRelinquished { get; set; }

        //[Column(TypeName = "datetime2")]
        //public DateTime RellinquishedDate { get; set; }

        public string StateLicenseDocumentPath { get; set; }

        public string Status { get; set; }

    }
}
