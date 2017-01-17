using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.IdentificationAndLicenses
{
    public class StateLicenseInformation
    {
        public StateLicenseInformation()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int StateLicenseInformationID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string LicenseNumber { get; set; }

        #region ProviderTypes

        public ICollection<ProviderType> ProviderTypes { get; set; }

        #endregion

        #region StateLicenseStatus

        [Required]
        public int StateLicenseStatusID { get; set; }
        [ForeignKey("StateLicenseStatusID")]
        public StateLicenseStatus StateLicenseStatus { get; set; }

        #endregion

        [Required]
        public string IssuingState { get; set; }

        [Required]
        public string PracticeState { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpiryDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime CurrentIssueDate { get; set; }

        #region LicenseInGoodStanding

        [Required]
        public string LicenseInGoodStanding { get; private set; }

        [NotMapped]
        public YesNoOption GoodStandingYesNoOption
        {
            get
            {
                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.LicenseInGoodStanding);
            }
            set
            {
                this.LicenseInGoodStanding = value.ToString();
            }
        }

        #endregion

        //public bool IsRelinquished { get; set; }

        //[Column(TypeName = "datetime2")]
        //public DateTime RellinquishedDate { get; set; }

        public string StateLicenseDocumentPath { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType StatusType
        {
            get
            {
                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        public virtual ICollection<StateLicenseInfoHistory> StateLicenseInfoHistories { get; set; }
    }
}
