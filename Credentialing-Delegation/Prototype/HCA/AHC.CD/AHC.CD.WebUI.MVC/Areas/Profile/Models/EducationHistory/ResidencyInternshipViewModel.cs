using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class ResidencyInternshipViewModel
    {
        public int ResidencyInternshipID { get; set; }
        [Required]
        [Display(Name = "Program Type")]
        public ProgramType ProgramType { get; set; }
        public SchoolDetailViewModel SchoolDetail { get; set; }
        [Required]
        [Display(Name = "Specialty")]
        public string DepartmentSpeciality { get; set; }
        [Required]
        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }
        [Display(Name = "ProgramCertPath")]
        public string ProgramCertPath { get; set; }
        [Required]
        [Display(Name = "Did you complete your training at this institution?")]
        public bool CompletedTrainingHere { get; set; }
        [Display(Name = "If no, explain why?")]
        public string Reason { get; set; }
        
        public string PrimaryPracticingDegree { get; set; }
        [Display(Name = "Affiliated University/Hospital")]
        public string AffiliatedUniversityHospital { get; set; }
        [Display(Name = "Department")]
        public string Department { get; set; }
    }

    public enum ProgramType
    {
        Internship = 1,
        Fellowship,
        Residency
    }
}