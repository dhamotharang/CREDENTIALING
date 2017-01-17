using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class CDSCInformationViewModel
    {
        //public int CDSCInformationID { get; set; }

        //[Required]
        //[Display(Name = "Certificate Number *")]
        //public string CertNumber { get; set; }

        //[Required]
        //[Display(Name = "State Of Registration *")]
        //public string State { get; set; }

        //[Column(TypeName = "datetime2")]
        //[Required]
        //[Display(Name = "Issue Date *")]
        //public DateTime IssueDate { get; set; }

        //[Column(TypeName = "datetime2")]
        //[Required]
        //[Display(Name = "Expiration Date *")]
        //public DateTime ExpiryDate { get; set; }
        //public string CDSCCertPath { get; set; }
        public int CDSCInformationID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
      [Display(Name = "CDSC Number *")]
        public string CertNumber { get; set; }

        [Required]
        [Display(Name = "Issue State *")]

        public string State { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Issue Date *")]

        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Expiry Date *")]
      
        public DateTime ExpiryDate { get; set; }

      [Display(Name = "Certificate Preview *")]

        public string CDSCCertPath { get; set; }

        public string Status { get; set; }

    }
}
