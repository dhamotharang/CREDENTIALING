using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.EducationHistory
{
    public class CMECertificationHistory
    {
        public CMECertificationHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CMECertificationHistoryID { get; set; }

        //[Required]
        public string QualificationDegree { get; set; }

        //[Required]
        public string Certification { get; set; }

        public virtual SchoolInformation SchoolInformation { get; set; }

        //[Required]
        public string SponsorName { get; set; }

        //[Required]
        [Column(TypeName = "datetime2")]
        public DateTime? ExpiryDate { get; set; }
        
        //[Required]
        //[Range(0,double.MaxValue)]
        public double? CreditHours { get; set; }        

        //[Required]
        [Column(TypeName = "datetime2")]
        public DateTime? StartDate { get; set; }

        //[Required]
        [Column(TypeName = "datetime2")]
        public DateTime? EndDate { get; set; }

        //[Required]
        public int StartYear { get; set; }

        public int StartMonth { get; set; }

        //[Required]
        public int EndYear { get; set; }

        public int EndMonth { get; set; }

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