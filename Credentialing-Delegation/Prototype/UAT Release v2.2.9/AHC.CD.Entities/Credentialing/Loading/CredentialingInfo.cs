using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.Credentialing.Appointment;
using AHC.CD.Entities.Credentialing.PSVInformation;
using AHC.CD.Entities.Credentialing.LoadingInformation;

namespace AHC.CD.Entities.Credentialing.Loading
{
    public class CredentialingInfo
    {
        public CredentialingInfo()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CredentialingInfoID { get; set; }

        public int? ProfileID { get; set; }
        [ForeignKey("ProfileID")]
        public Profile Profile { get; set; }

        public int? PlanID { get; set; }
        [ForeignKey("PlanID")]
        public Plan Plan { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? InitiationDate { get; set; }

        public int? InitiatedByID { get; set; }
        [ForeignKey("InitiatedByID")]
        public CDUser InitiatedBy { get; set; }

        #region Delegated Type

        public string IsDelegated { get; private set; }

        [NotMapped]
        public YesNoOption? IsDelegatedYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsDelegated))
                    return null;

                if (this.IsDelegated.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsDelegated);
            }
            set
            {
                this.IsDelegated = value.ToString();
            }
        }

        #endregion

        public ICollection<CredentialingVerificationInfo> CredentialingVerificationInfos { get; set; }

        public ICollection<CredentialingLog> CredentialingLogs { get; set; }

        public ICollection<CredentialingContractRequest> CredentialingContractRequests { get; set; }

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
