using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class WorkersCompensationInformationViewModel
    {              
        public int WorkersCompensationInformationID { get; set; }

        [Display(Name = "Workers Compensation Number")]
        public string WorkersCompensationNumber { get; set; }      

        [Display(Name = "Certification Status")]
        public StatusType CertificationStatus { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        [Display(Name="Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        [Display(Name = "Issue Date")]
        public DateTime IssueDate { get; set; }

    }
}