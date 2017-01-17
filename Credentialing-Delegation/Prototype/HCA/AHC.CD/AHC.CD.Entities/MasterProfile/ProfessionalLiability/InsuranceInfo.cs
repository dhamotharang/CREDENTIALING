using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.ProfessionalLiability
{
    public class InsuranceInfo
    {
        public InsuranceInfo()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int InsuranceInfoID { get; set; }

        #region InsuranceCarrier

        [Required]
        public int InsuranceCarrierID { get; set; }
        [ForeignKey("InsuranceCarrierID")]
        public InsuranceCarrier InsuranceCarrier { get; set; }
        
        #endregion        

        [Required]
        public string PolicyNumber { get; set; }

        #region SelfInsured

        [Required]
        public string SelfInsured { get; private set; }

        [NotMapped]
        public YesNoOption SelfInsuredYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.SelfInsured);
            }
            set
            {
                this.SelfInsured = value.ToString();
            }
        }
        
        #endregion

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime OriginalEffectiveDate  { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime EffectiveDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpirationDate { get; set; }

        #region CoverageType

        [Required]
        public string CoverageType { get; private set; }

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

        #endregion

        #region UnlimitedCoverageWithInsuranceCarrier

        public string UnlimitedCoverageWithInsuranceCarrier { get; private set; }

        [NotMapped]
        public YesNoOption UnlimitedCoverageWithInsuranceCarrierOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.UnlimitedCoverageWithInsuranceCarrier);
            }
            set
            {
                this.UnlimitedCoverageWithInsuranceCarrier = value.ToString();
            }
        }

        #endregion

        public double AmountOfCoveragePerOccurance { get; set; }

        public double AmountOfCoverageAggregate { get; set; }

        #region PolicyIncludesTailCoverage

        public string PolicyIncludesTailCoverage { get; private set; }

        [NotMapped]
        public YesNoOption PolicyIncludesTailCoverageOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.PolicyIncludesTailCoverage);
            }
            set
            {
                this.PolicyIncludesTailCoverage = value.ToString();
            }
        }

        #endregion

        #region DeniedProffessionalLiabilityInsurance

        public string DeniedProffessionalLiabilityInsurance { get; private set; }

        [NotMapped]
        public YesNoOption DeniedProffessionalLiabilityInsuranceOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.DeniedProffessionalLiabilityInsurance);
            }
            set
            {
                this.DeniedProffessionalLiabilityInsurance = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime DenialDate { get; set; }

        public string DenialReason { get; set; }

        public string InsuranceCertificatePath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        //public string InsuranceDocPath { get; set; }
        //public int LossLimit { get; set; }
        //public int AggregateLimit { get; set; }
    }
}
