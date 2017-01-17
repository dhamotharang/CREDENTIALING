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
    public class InsuranceInfoViewModel
    {
        

        public int InsuranceInfoID { get; set; }

        [Display(Name = "Insurance Carrier")]
        public InsuranceCarrier InsuranceCarrier { get; set; }

        [Required]
        [Display(Name = "Policy Number")]
        public string PolicyNumber { get; set; }

        [Required]
        [Display(Name= "Self Insured?")]
        public bool SelfInsured { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Original Effective Date")]
        public DateTime OriginalEffectiveDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Effective Date")]
        public DateTime EffectiveDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Expiration Date")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Display(Name = "Type Of Coverage *?")]
        public string CoverageType { get; set; }

        [NotMapped]
        public InsuranceCoverageType InsuranceCoverageType
        {
            get
            {
                return (InsuranceCoverageType)Enum.Parse(typeof(InsuranceCoverageType), this.CoverageType);
            }
            set
            {
                this.CoverageType = value.ToString();
            }
        }

         [Display(Name = "Do You Have Unlimited Coverage With this Insurance Carrier?")]
        public bool UnlimitedCoverageWithInsuranceCarrier { get; set; }

        [Display(Name = "Amount Of Coverage Per Occurance")]
        public double AmountOfCoveragePerOccurance { get; set; }

        [Display(Name = "Amount Of Coverage Aggregate")]
        public double AmountOfCoverageAggregate { get; set; }

        [Display(Name = "Policy Includes Tail Coverage?")]
        public bool PolicyIncludesTailCoverage { get; set; }

        [Display(Name = "Have You Ever Been Denied Proffessional Liability Insurance?")]
        public bool DeniedProffessionalLiabilityInsurance { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Denial Date")]
        public DateTime DenialDate { get; set; }

        [Display(Name = "Denial Reason")]
        public string DenialReason { get; set; }

        public string InsuranceCertificatePath { get; set; }
    }
}