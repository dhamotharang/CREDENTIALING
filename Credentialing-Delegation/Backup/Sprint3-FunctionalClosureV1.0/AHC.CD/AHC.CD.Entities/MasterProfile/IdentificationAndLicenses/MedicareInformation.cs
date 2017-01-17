using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.IdentificationAndLicenses
{
    public class MedicareInformation
    {
        public MedicareInformation()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int MedicareInformationID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index("MedicareLicenseNumber", IsUnique = true)]
        public string LicenseNumber { get; set; }

        [Column(TypeName="datetime2")]
        [Required]
        public DateTime IssueDate { get; set; }

        public string CertificatePath { get; set; }

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
