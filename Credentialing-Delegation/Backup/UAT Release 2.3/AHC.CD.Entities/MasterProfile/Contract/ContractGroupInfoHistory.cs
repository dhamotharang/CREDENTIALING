using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Contract
{
    public class ContractGroupInfoHistory
    {
        public ContractGroupInfoHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ContractGroupInfoHistoryID { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        //[Required]
        public DateTime? ExpiryDate { get; set; }

        public string ContractGroupCerificatePath { get; set; }

        public int PracticingGroupId { get; set; }
        [ForeignKey("PracticingGroupId")]
        public PracticingGroup PracticingGroup { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? JoiningDate { get; set; }

        #region Contract Group Status

        public string ContractGroupStatus { get; set; }

        [NotMapped]
        public ContractGroupStatus? ContractGroupStatusOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.ContractGroupStatus))
                    return null;

                if (this.ContractGroupStatus.Equals("Not Available"))
                    return null;

                return (ContractGroupStatus)Enum.Parse(typeof(ContractGroupStatus), this.ContractGroupStatus);
            }
            set
            {
                this.ContractGroupStatus = value.ToString();
            }
        }

        #endregion

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
