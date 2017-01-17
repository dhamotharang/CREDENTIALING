using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class ProgramDetailViewModel
    {

        public int ProgramDetailID { get; set; }

        [Required]
        [Display(Name = "Program Type *")]
        public ResidencyInternshipProgramType? ResidencyInternshipProgramType { get; set; }

        [Display(Name = "Department")]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Specialty *")]
        public int? SpecialtyID { get; set; }

        [Required]
        [Display(Name = "Preference Type *")]
        public PreferenceType? PreferenceType { get; set; }

        [Display(Name = "Affiliated University/Hospital")]
        public string HospitalName { get; set; }

        [Display(Name = "Director Name")]
        public string DirectorName { get; set; }

        [Display(Name = "Start Date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        public EducationAddressViewModel SchoolInformation { get; set; }

        [Display(Name = "Did you complete your training at this school ?")]
        public YesNoOption? CompletedYesNoOption { get; set; }

        [Display(Name = "If No, Give the reason")]
        public string InCompleteReason { get; set; }

        public HttpPostedFileBase ProgramDocumentPath { get; set; }

        [Display(Name = "Supporting Document")]
        public string DocumentPath { get; set; }
    }
}