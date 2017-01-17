using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterProfile.IdentificationAndLicenses
{
    public class CDSCInformation
    {
        public CDSCInformation()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CDSCInformationID { get; set; }

        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string CertNumber { get; set; }

        [Required]
        public string State { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime IssueDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime ExpiryDate { get; set; }

        public string CDSCCertPath { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType StatusType
        {
            get
            {
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

        public virtual ICollection<CDSCInfoHistory> CDSCInfoHistories { get; set; }
    }
}
