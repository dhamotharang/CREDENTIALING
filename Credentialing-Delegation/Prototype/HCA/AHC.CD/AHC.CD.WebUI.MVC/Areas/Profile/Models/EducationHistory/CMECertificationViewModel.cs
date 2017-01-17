using AHC.CD.Entities.MasterData;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class CMECertificationViewModel
    {
        public CMECertificationViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int CMECertificationID { get; set; }

        public int QualificationDegreeID { get; set; }

        [Display(Name="Degree Awarded")]
        public QualificationDegree QualificationDegree { get; set; }

        public int CertificationID { get; set; }

        [Display(Name = "Certification Name")]
        public Certification Certification { get; set; }

        [Display(Name = "Institute Name")]
        public string InstituteName { get; set; }

        [Display(Name = "Sponsor Name")]
        public string SponsorName { get; set; }

        [Display(Name = "Expiriation Date")]
        public DateTime ExpiryDate { get; set; }
        
        [Required]
        [Range(0,double.MaxValue)]
        [Display(Name = "Credit Hours")]
        public double CreditHours { get; set; }

        public EducationAddressViewModel EducationAddress { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select Certificate")]
        public string CertificatePath { get; set; }
        
        public DateTime LastModifiedDate { get; set; }
    }
}
