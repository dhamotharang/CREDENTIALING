using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.Credentialing.PSVInformation;
using AHC.CD.Entities.Credentialing.AppointmentInformation;
using AHC.CD.Entities.MasterData.Enums;

namespace AHC.CD.Entities.Credentialing.LoadingInformation
{
    public class CredentialingLog
    {
        public CredentialingLog()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CredentialingLogID { get; set; }

        public CredentialingVerificationInfo CredentialingVerificationInfo { get; set; }

        public CredentialingAppointmentDetail CredentialingAppointmentDetail { get; set; }

        public ICollection<CredentialingActivityLog> CredentialingActivityLogs { get; set; }

        #region Credentialing Type

        public string Credentialing { get; private set; }

        [NotMapped]
        public CredentialingType? CredentialingType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Credentialing))
                    return null;

                if (this.Credentialing.Equals("Not Available"))
                    return null;

                return (CredentialingType)Enum.Parse(typeof(CredentialingType), this.Credentialing);
            }
            set
            {
                this.Credentialing = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime? LastModifiedDate { get; set; }
    }
}
