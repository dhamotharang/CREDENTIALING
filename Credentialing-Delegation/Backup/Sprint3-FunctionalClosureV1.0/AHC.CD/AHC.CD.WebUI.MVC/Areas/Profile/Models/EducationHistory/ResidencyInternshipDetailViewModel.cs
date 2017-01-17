using AHC.CD.Entities.MasterData;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class ResidencyInternshipDetailViewModel
    {
        
        public int ResidencyInternshipDetailID { get; set; }

        [Required]
        [Display(Name = "Program Type*")]
        public ResidencyInternshipProgramType ResidencyInternshipProgramType { get; set; }

        [Display(Name = "Preference Type*")]
        public PreferenceType PreferenceType { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9 \w#-.]+$", ErrorMessage = "Enter only alpha numeric ")]
        [Display(Name = "Department*")]
        public string Department { get; set; }

        [Required]
        [Display(Name = "Specialty*")]
        public int SpecialtyID { get; set; }


        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "{0} must be between {2} and {1} numbers.")]
        [RegularExpression(@"^[a-zA-Z ]+$", ErrorMessage = "Enter only alphabets")]
        [Display(Name = "Director Name*")]
        public string DirectorName { get; set; }

        [Required]
        [Display(Name = "Start Date*")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = "Date should be in between past 100 years from now!!")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date*")]
        [DateEnd(DateStartProperty = "StartDate", IsRequired = true, ErrorMessage = "Date should be greater than Start Date")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = "Date should be in between past 100 years from now!!")] 
        public DateTime EndDate { get; set; }

        [Display(Name = "Supporting Document")]
        public string DocumentPath { get; set; }


        [Display(Name = "Supporting Document*")]
        [RequiredIfEmpty("DocumentPath", ErrorMessage = "The file is required")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = "Upload pdf, jpeg, jpg, png, bmp files only")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "File too large..!!")]
        public HttpPostedFileBase ResidecncyCertificateDocumentFile { get; set; }

        
    }
}
