using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.EducationHistory
{
    public class CMECertification
    {
        public CMECertification()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int CMECertificationID { get; set; }

        [Required]
        public string QualificationDegree { get; set; }

        [Required]
        public string Certification { get; set; }

        public virtual SchoolInformation SchoolInformation { get; set; }

        [Required]
        public string SponsorName { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime ExpiryDate { get; set; }
        
        [Required]
        [Range(0,double.MaxValue)]
        public double CreditHours { get; set; }        

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime StartDate { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime EndDate { get; set; }

        public string CertificatePath { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
