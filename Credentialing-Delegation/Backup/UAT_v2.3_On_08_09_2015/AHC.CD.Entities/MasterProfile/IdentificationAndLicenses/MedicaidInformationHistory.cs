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
    public class MedicaidInformationHistory
    {
        public MedicaidInformationHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int MedicaidInformationHistoryID { get; set; }

        //[Required]
        [MaxLength(100)]
       // [Index(IsUnique = true)]
        public string LicenseNumber { get; set; }

        //[Required]
        public string State { get; set; }

        [Column(TypeName="datetime2")]
        public DateTime? IssueDate { get; set; }

        public string CertificatePath { get; set; }

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
