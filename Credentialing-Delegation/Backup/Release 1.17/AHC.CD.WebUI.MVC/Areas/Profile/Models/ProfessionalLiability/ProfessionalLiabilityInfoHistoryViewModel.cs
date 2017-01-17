using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability
{
    public class ProfessionalLiabilityInfoHistoryViewModel
    {

      
        public int ProfessionalLiabilityInfoHistoryID { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpiryDate { get; set; }

        public string InsuranceCertificatePath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
        
    }
}