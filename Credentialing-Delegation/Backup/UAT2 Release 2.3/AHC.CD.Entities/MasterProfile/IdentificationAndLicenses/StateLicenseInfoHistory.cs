using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.IdentificationAndLicenses
{
    public class StateLicenseInfoHistory
    {
        public StateLicenseInfoHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int StateLicenseInfoHistoryID { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? CurrentIssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? ExpiryDate { get; set; }

        //[Required]
        [MaxLength(100)]
        //[Index(IsUnique = true)]
        public string LicenseNumber { get; set; }

        #region State License Type

        //[Required]
        public int? ProviderTypeID { get; set; }
        [ForeignKey("ProviderTypeID")]
        public ProviderType ProviderType { get; set; }

        #endregion

        #region StateLicenseStatus

        //[Required]
        public int? StateLicenseStatusID { get; set; }
        [ForeignKey("StateLicenseStatusID")]
        public StateLicenseStatus StateLicenseStatus { get; set; }

        #endregion

        //[Required]
        public string IssueState { get; set; }

        #region IsCurrentPracticeState

        public string IsCurrentPracticeState { get; set; }

        [NotMapped]
        public YesNoOption? CurrentPracticeStateYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsCurrentPracticeState))
                    return null;

                if (this.IsCurrentPracticeState.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsCurrentPracticeState);
            }
            set
            {
                this.IsCurrentPracticeState = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime? IssueDate { get; set; }

        #region LicenseInGoodStanding

        //[Required]
        public string LicenseInGoodStanding { get; set; }

        [NotMapped]
        public YesNoOption? GoodStandingYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.LicenseInGoodStanding))
                    return null;

                if (this.LicenseInGoodStanding.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.LicenseInGoodStanding);
            }
            set
            {
                this.LicenseInGoodStanding = value.ToString();
            }
        }

        #endregion

        public string StateLicenseDocumentPath { get; set; }

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
