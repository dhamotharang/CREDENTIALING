using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.AppointmentInformation
{
    public class CredentialingAppointmentResult
    {
        public CredentialingAppointmentResult()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CredentialingAppointmentResultID { get; set; }

        public string SignaturePath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? SignedDate { get; set; }

        public int? SignedByID { get; set; }
        [ForeignKey("SignedByID")]
        public CDUser SignedBy { get; set; }

        #region Level

        public string Level { get; set; }

        [NotMapped]
        public CredentialingLevel? CredentialingLevel
        {
            get
            {
                if (String.IsNullOrEmpty(this.Level))
                    return null;

                if (this.Level.Equals("Not Available"))
                    return null;

                return (CredentialingLevel)Enum.Parse(typeof(CredentialingLevel), this.Level);
            }
            set
            {
                this.Level = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
