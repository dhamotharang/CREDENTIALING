using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Contract
{
    public class ContractGroupInfo
    {
        public ContractGroupInfo()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ContractGroupInfoId { get; set; }

        public int PracticingGroupId { get; set; }
        [ForeignKey("PracticingGroupId")]
        public PracticingGroup PracticingGroup { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? JoiningDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpiryDate { get; set; }

        public string ContractGroupCerificatePath { get; set; }

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

        public ICollection<ContractGroupInfoHistory> ContractGroupInfoHistory { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
                    return null;

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
    }
}
