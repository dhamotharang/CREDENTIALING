using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.UtilityService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.ProfessionalLiability
{
    public class ProfessionalLiabilityInfoHistory
    {
        public ProfessionalLiabilityInfoHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProfessionalLiabilityInfoHistoryID { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? ExpiryDate { get; set; }

        #region InsuranceCarrier

        //[Required]
        public int? InsuranceCarrierID { get; set; }
        [ForeignKey("InsuranceCarrierID")]
        public InsuranceCarrier InsuranceCarrier { get; set; }

        #endregion

        #region Insurance Carrier Address

        //[Required]
        public int? InsuranceCarrierAddressID { get; set; }
        [ForeignKey("InsuranceCarrierAddressID")]
        public InsuranceCarrierAddress InsuranceCarrierAddress { get; set; }

        #endregion

        #region Policy Number

        //[Required]
        public string PolicyNumberStored { get; private set; }

        [NotMapped]
        public string PolicyNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.PolicyNumberStored))
                    return null;

                if (this.PolicyNumberStored.Equals("Not Available"))
                    return null;

                return EncryptorDecryptor.Decrypt(this.PolicyNumberStored);
            }
            set { this.PolicyNumberStored = EncryptorDecryptor.Encrypt(value); }
        }

        #endregion

        #region SelfInsured

        //[Required]
        public string SelfInsured { get; set; }

        [NotMapped]
        public YesNoOption? SelfInsuredYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.SelfInsured))
                    return null;

                if (this.SelfInsured.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.SelfInsured);
            }
            set
            {
                this.SelfInsured = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime? OriginalEffectiveDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? EffectiveDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? ExpirationDate { get; set; }

        #region CoverageType

        //[Required]
        public string CoverageType { get; set; }

        [NotMapped]
        public InsuranceCoverageType? InsuranceCoverageType
        {
            get
            {
                if (String.IsNullOrEmpty(this.CoverageType))
                    return null;

                if (this.CoverageType.Equals("Not Available"))
                    return null;

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
        public YesNoOption? UnlimitedCoverageWithInsuranceCarrierOption
        {

            get
            {
                if (String.IsNullOrEmpty(this.UnlimitedCoverageWithInsuranceCarrier))
                    return null;

                if (this.UnlimitedCoverageWithInsuranceCarrier.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.UnlimitedCoverageWithInsuranceCarrier);
            }
            set
            {
                this.UnlimitedCoverageWithInsuranceCarrier = value.ToString();
            }
        }

        #endregion

        public double? AmountOfCoveragePerOccurance { get; set; }

        public double? AmountOfCoverageAggregate { get; set; }

        #region Phone Number

        //[Required]
        public string PhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.PhoneCountryCode))
                    return this.Phone;
                else if (!String.IsNullOrEmpty(this.Phone))
                    return this.PhoneCountryCode + "-" + this.Phone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Phone = numbers[0];
                    else
                    {
                        this.PhoneCountryCode = numbers[0];
                        this.Phone = numbers[1];
                    }

                }
            }
        }

        [NotMapped]
        //[Required]
        public string Phone { get; set; }

        [NotMapped]
        public string PhoneCountryCode { get; set; }

        #endregion

        #region PolicyIncludesTailCoverage

        public string PolicyIncludesTailCoverage { get; set; }

        [NotMapped]
        public YesNoOption? PolicyIncludesTailCoverageOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.PolicyIncludesTailCoverage))
                    return null;

                if (this.PolicyIncludesTailCoverage.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.PolicyIncludesTailCoverage);
            }
            set
            {
                this.PolicyIncludesTailCoverage = value.ToString();
            }
        }

        #endregion

        public string InsuranceCertificatePath { get; set; }

        #region History Status

        public string HistoryStatus { get; private set; }

        [NotMapped]
        public HistoryStatusType? HistoryStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.HistoryStatus))
                    return null;

                if (this.HistoryStatus.Equals("Not Available"))
                    return null;

                return (HistoryStatusType)Enum.Parse(typeof(HistoryStatusType), this.HistoryStatus);
            }
            set
            {
                this.HistoryStatus = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
