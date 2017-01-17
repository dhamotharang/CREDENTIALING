using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class FederalDEAInformationViewModel
    {
        public int FederalDEAInformationID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        [Display(Name = "DEA Number *")]
        public string DEANumber { get; set; }

        [Required]
        [Display(Name = "State of Registration *")]

        public string StateOfReg { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Issue Date *")]


        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Expiry Date *")]

        public DateTime ExpiryDate { get; set; }

        public ICollection<DEAScheduleInfoViewModel> DEAScheduleInfoes { get; set; }

        [Required]
        [Display(Name = "License in good standing? *")]

        public bool IsInGoodStanding { get; set; }


        //[Required]
        //[Display(Name = "DO you have any state controlled substance registration Certificate? *")]

        //public bool HasStateControlledSubstanceRegCertificate { get; set; }

       // public CDSCInformationViewModel CDSInformation { get; set; }
      [Display(Name = "Certificate Preview *")]

        public string DEALicenceCertPath { get; set; }

       

        public string Status { get; set; }

    }

    //public class DEAScheduleViewModel
    //{
    //    public int DEAScheduleID { get; set; }
    //    public string ScheduleType { get; set; }
    //    public bool IsNarcotic  { get; set; }
    //    public bool IsNonNarcotic { get; set; }
    //}
}
