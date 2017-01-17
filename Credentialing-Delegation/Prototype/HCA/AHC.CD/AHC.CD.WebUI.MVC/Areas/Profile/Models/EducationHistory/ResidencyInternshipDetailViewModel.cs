using AHC.CD.Entities.MasterData;
using AHC.CD.Entities.MasterData.Enums;
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
    public class ResidencyInternshipDetailViewModel
    {
        public ResidencyInternshipDetailViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int ResidencyInternshipDetailID { get; set; }

        [NotMapped]
        [Display(Name = "Program Type")]
        public ResidencyInternshipProgramType ResidencyInternshipProgramType { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        public int SpecialityID { get; set; }
        [Display(Name = "Specialty")]
        public Speciality Speciality { get; set; }
        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }
       
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
       
        public DateTime LastModifiedDate { get; set; }
    }
}
