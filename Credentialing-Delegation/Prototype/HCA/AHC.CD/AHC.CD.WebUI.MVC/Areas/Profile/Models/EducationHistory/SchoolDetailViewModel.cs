using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class SchoolDetailViewModel
    {
        public int SchoolDetailID { get; set; }

        [Required]
        [Display(Name = "School Name")]
        public string SchoolName { get; set; }
        
        [Display(Name = "School Code")]
        public string SchoolCode { get; set; }
        [Required]
        [Display(Name = "Degree Awarded")]
        public string Degree { get; set; }
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Telephone Number")]
        public string Phone { get; set; }
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "End Date")]
        
        
        public DateTime EndDate { get; set; }
        
        
        public SchoolAddressViewModel SchoolAddress { get; set; }
        [Display(Name = "UG Certificate")]
        public string UGCertificatePath { get; set; }
        
        
        [Display(Name = "Did you complete your UG education at this school?")]
        public bool UGCompletedInThisSchool { get; set; }

        

    }
}
