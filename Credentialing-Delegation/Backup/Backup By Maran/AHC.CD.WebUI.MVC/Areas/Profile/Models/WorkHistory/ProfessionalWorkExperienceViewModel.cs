using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
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

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.WorkHistory
{
    public class ProfessionalWorkExperienceViewModel
    {
        public int ProfessionalWorkExperienceID { get; set; }

        [Required(ErrorMessage="Please enter Employer name")]
        [Display(Name = "Employer Name *")]
      //  [MaxLength(50, ErrorMessage = "Employer name must be between 2 and 50 characters")]
      //  [MinLength(2, ErrorMessage = "Employer name must be between 2 and 50 characters")]
      //  [RegularExpression("([a-zA-Z0-9+ ?.,'-]+)", ErrorMessage = "Please enter valid Employer name.Only alpha-numerals, space, comma, hyphen accepted")]
        public string EmployerName { get; set; }

        //[Required(ErrorMessage = "Please enter the State")]
       //[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
       //[MaxLength(100, ErrorMessage = "State must be between 1 and 100 characters")]
       //[MinLength(1, ErrorMessage = "State must be between 1 and 100 characters")]
        [Display(Name = "State")]
        public string State { get; set; }

       // [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
       // [MaxLength(100, ErrorMessage = "City must be between 1 and 100 characters")]
       // [MinLength(1, ErrorMessage = "City must be between 1 and 100 characters")]
        [Display(Name = "City")]
        public string City { get; set; }

      // [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
      // [MaxLength(100, ErrorMessage = "County must be between 1 and 100 characters")]
      // [MinLength(1, ErrorMessage = "County must be between 1 and 100 characters")]
        [Display(Name = "County")]
        public string County { get; set; }

        //[Required(ErrorMessage = "Please enter the Country")]
     //   [MaxLength(100, ErrorMessage = "Country name must be between 2 and 50 characters")]
     //   [MinLength(1, ErrorMessage = "Country name must be between 2 and 50 characters")]
     //   [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [Display(Name = "Country")]
        public string Country { get; set; }

        
        //[MaxLength(50, ErrorMessage = "Suite/Building must be between 2 and 50 characters")]
        //[MinLength(2, ErrorMessage = "Suite/Building must be between 2 and 50 characters")]
        //[RegularExpression("([a-zA-Z0-9+ ?|#|.,'-]+)", ErrorMessage = "Please enter valid Building.Only alpha-numerals, space, comma, hyphen ,hash accepted")]
        [Display(Name = "Suite/Building")]
        //[StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string Building { get; set; }

        
        //[MaxLength(100, ErrorMessage = "Street must be between 1 and 100 characters")]
        //[MinLength(1, ErrorMessage = "Street must be between 1 and 100 characters")]
        //[RegularExpression("([a-zA-Z0-9+ ?|#|.,'-]+)", ErrorMessage = "Please enter valid Street.Only alpha-numerals, space, comma, hyphen ,hash accepted")]
        [Display(Name = "Street/P. O. Box No:")]
       // [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string Street { get; set; }

        [Display(Name = "Zip Code")]
        [RegularExpression("([a-zA-Z0-9+?-]+)", ErrorMessage = "Please enter valid Zipcode. Only alphabets, numbers and hyphens accepted")]
        [MaxLength(10, ErrorMessage = "ZipCode must be between 5 and 10 characters.")]
        [MinLength(5, ErrorMessage = "ZipCode must be between 5 and 10 characters.")]
        public string ZipCode { get; set; }

        [Display(Name = "Phone Number")]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = "Please enter only numbers in Phone number. ")]
        //[MaxLength(10, ErrorMessage = "Phone number must be of 10 digits")]
       // [MinLength(10, ErrorMessage = "Phone number must be of 10 digits")]
        public string EmployerMobile { get; set; }

        [RequiredIfNotEmpty("EmployerMobile", ErrorMessage = "Employer Phone country code is required")]
        public string CountryCodeMobile { get; set; }

        [Display(Name = "Fax Number")]
        //[RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = "Please enter only numbers in Fax number. ")]
        //[MaxLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        //[MinLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        public string EmployerFax { get; set; }

        public string CountryCodeFax { get; set; }

        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,5}$", ErrorMessage = ValidationErrorMessage.INVALID_EMAIL_FORMAT)]
        public string EmployerEmail { get; set; }

        [Display(Name = "Job Title")]
      // [MaxLength(50, ErrorMessage = "Job Title must be of 2 and 50 characters")]
      // [MinLength(2, ErrorMessage = "Job Title must be of 2 and 50 characters")]
      // [RegularExpression("([a-zA-Z ?+.,'-]+)", ErrorMessage = "Please enter valid Job Title.Only Character, space, comma, hyphen accepted")]
        public string JobTitle { get; set; }
        
        [Display(Name = "Supervisor Name")]
       // [MaxLength(50, ErrorMessage = "Supervisor Name must be between 2 and 50 characters.")]
       // [MinLength(2, ErrorMessage = "Supervisor Name must be between 2 and 50 characters.")]
       // [RegularExpression("([a-zA-Z ?+.,'-]+)", ErrorMessage = "Please enter valid Supervisor Name.Only Character, space, comma, hyphen accepted")]
        public string SupervisorName { get; set; }  

        [Display(Name = "Department")]
     //   [MaxLength(100, ErrorMessage = "Department must be between 2 and 50 characters")]
     //   [MinLength(2, ErrorMessage = "Department must be between 2 and 50 characters")]
        //[RegularExpression("([a-zA-Z ?+.,'-]+)", ErrorMessage = "Please enter valid Department.Only Character, space, comma, hyphen accepted")]
        public string Department { get; set; }

        [Display(Name = "Provider Type")]
        //[Required]
        //[RegularExpression(@"^\d+$", ErrorMessage = "Provider Type is required")]
        public int? ProviderTypeID { get; set; }

        //[Required(ErrorMessage = "Can Contact Employer Option is required")]
        [Display(Name = "Can Contact Employer Option ?")]
        public YesNoOption? CanContactEmployerOption { get; set; }

        [Display(Name = "Currently Working Here ?")]
        //[Required(ErrorMessage = "Please specify are you currently working here?")]
        public YesNoOption? CurrentlyWorkingOption { get; set; }

        [Display(Name = "End Date ")]
        //[RequiredIf("CurrentlyWorkingOption",(int)YesNoOption.NO,ErrorMessage="End Date is required")]
        [DateEnd(DateStartProperty = "StartDate",IsRequired=false, ErrorMessage = "End Date should be greater than start date")]
        //[DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "End Date should be less than current date")]        
        //[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? EndDate { get; set; }

        //[Required(ErrorMessage="Please enter Start Date")]
        [Display(Name = "Start Date")]
        //[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        //[DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = "Start Date should be less than current date & should fall between 50 years from now!!")]        
        public DateTime? StartDate { get; set; }

        
      //  [MaxLength(50, ErrorMessage = "Reason For Departure should be between 2 and 50 characters")]
      //  [MinLength(2, ErrorMessage = "Reason For Departure should be between 2 and 50 characters")]
        [Display(Name = "Reason For Departure")]
        public string DepartureReason { get; set; }

        public string WorkExperienceDocPath { get; set; }
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
//        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = "Please select the file of type .pdf, .jpeg, .png, .jpg, .bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Experience document should be less than 10mb in size")]
        public HttpPostedFileBase File { get; set; }

        public StatusType? StatusType { get; set; }
    }
}
