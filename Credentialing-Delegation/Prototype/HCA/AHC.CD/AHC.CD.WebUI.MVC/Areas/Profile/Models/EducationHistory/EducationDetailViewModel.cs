using AHC.CD.Entities.MasterData;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class EducationDetailViewModel
    {
        public EducationDetailViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int EducationDetailID { get; set; }

        [NotMapped]
        [Display(Name = "Graduate Type")]
        public EducationGraduateType GraduateType { get; set; } 
        

        [NotMapped]        
        public EducationQualificationType EducationQualificationType { get; set; }

        public int QualificationDegreeID { get; set; }

        [Required]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }

        [Display(Name = "Degree Awarded")]
        public QualificationDegree QualificationDegree { get; set; }
        
        public EducationAddressViewModel EducationAddress { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Telephone Number")]
        public string Phone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Do you completed your education in this school ?")]
        public YesNoOption IsCompleted { get; set; }
        
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public ECFMGDetailsViewModel ECFMGDetail { get; set; }

        [Display(Name = "Certificate")]
        public string CertificatePath { get; set; }
       
        public DateTime LastModifiedDate { get; set; }
    }
}
