using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal
{
    public class ProfileUpdatesTracker
    {
        public ProfileUpdatesTracker()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProfileUpdatesTrackerId { get; set; }

        public string oldData { get; set; }

        public string NewData { get; set; }

        public string NewConvertedData { get; set; }

        public string Section { get; set; }

        public string SubSection { get; set; }

        public string Url { get; set; }

        #region Approval Status

        public string ApprovalStatus { get; set; }

        [NotMapped]
        public ApprovalStatusType? ApprovalStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.ApprovalStatus))
                    return null;

                if (this.ApprovalStatus.Equals("Not Available"))
                    return null;

                return (ApprovalStatusType)Enum.Parse(typeof(ApprovalStatusType), this.ApprovalStatus);
            }
            set
            {
                this.ApprovalStatus = value.ToString();
            }
        }

        #endregion

        #region Modification Type

        public string Modification { get; set; }

        [NotMapped]
        public ModificationType? ModificationType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Modification))
                    return null;

                if (this.Modification.Equals("Not Available"))
                    return null;

                return (ModificationType)Enum.Parse(typeof(ModificationType), this.Modification);
            }
            set
            {
                this.Modification = value.ToString();
            }
        }

        #endregion

        public string RejectionReason { get; set; }

        //public string RedisId { get; set; }

        public int ProfileId { get; set; }
        [ForeignKey("ProfileId")]
        public virtual Profile Profile { get; set; }

        public int LastModifiedBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
        
    }
}
