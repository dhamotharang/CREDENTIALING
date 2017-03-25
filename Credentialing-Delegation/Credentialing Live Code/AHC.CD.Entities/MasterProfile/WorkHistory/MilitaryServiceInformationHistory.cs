using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.WorkHistory
{
    public class MilitaryServiceInformationHistory
    {
        public MilitaryServiceInformationHistory()
        {
            LastModifiedDate = DateTime.Now;
            this.DeletedDate = DateTime.Now.ToUniversalTime();
        }

        public int MilitaryServiceInformationHistoryID { get; set; }

        //[Required]
        public string MilitaryBranch { get; set; }

        //[Required]
        public string MilitaryRank { get; set; }

        public string MilitaryDischarge { get; set; }

        //[Required]
        public string MilitaryPresentDuty { get; set; }        

        public string MilitaryDischargeExplanation { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime DischargeDate { get; set; }

        public string HonorableExplanation { get; set; }

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
