using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.IdentificationAndLicenses
{
    public class FederalDEAInfoHistory
    {
        public FederalDEAInfoHistory()
        {
            LastModifiedDate = DateTime.Now;

           this.DeletedDate = DateTime.Now.ToUniversalTime();
        }

        public int FederalDEAInfoHistoryID { get; set; }

        //[Required]
        [MaxLength(100)]
        //[Index(IsUnique = true)]
        public string DEANumber { get; set; }

        //[Required]
        public string StateOfReg { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? ExpiryDate { get; set; }

        //[Required]
        public virtual ICollection<DEAScheduleInfoHistory> DEAScheduleInfoHistory { get; set; }

        #region Is In Good Standing

        //[Required]
        public string IsInGoodStanding { get; set; }

        [NotMapped]
        public YesNoOption? GoodStandingYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsInGoodStanding))
                    return null;

                if (this.IsInGoodStanding.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsInGoodStanding);
            }
            set
            {
                this.IsInGoodStanding = value.ToString();
            }
        }

        #endregion

        public string FederalDEADocumentPath { get; set; }

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

        public int? DeletedById { get; set; }
        [ForeignKey("DeletedById")]
        public CDUser DeletedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? DeletedDate { get; set; }
    }
}
