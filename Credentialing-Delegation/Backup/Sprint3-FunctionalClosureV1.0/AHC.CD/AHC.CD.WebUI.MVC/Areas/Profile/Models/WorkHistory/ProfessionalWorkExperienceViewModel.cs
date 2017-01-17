using AHC.CD.Entities.MasterData.Enums;
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
        [MaxLength(50, ErrorMessage = "Employer name must be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "Employer name must be between 2 and 50 characters")]
        [RegularExpression("([a-zA-Z0-9+ ?.'-]+)", ErrorMessage = "Please enter valid Employer name.Only alpha-numerals, space, comma, hyphen accepted")]
        public string EmployerName { get; set; }

        [Required(ErrorMessage = "Please enter State")]
        [RegularExpression("([a-zA-Z0-9+ ?.'-]+)", ErrorMessage = "Please enter valid State.Only alpha-numerals, space, comma, hyphen accepted")]
        [MaxLength(50, ErrorMessage = "State must be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "State must be between 2 and 50 characters")]
        [Display(Name = "State *")]
        public string State { get; set; }

        [RegularExpression("([a-zA-Z0-9+ ?.'-]+)", ErrorMessage = "Please enter valid City.Only alpha-numerals, space, comma, hyphen accepted")]
        [MaxLength(50, ErrorMessage = "City must be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "City must be between 2 and 50 characters")]
        [Display(Name = "City")]
        public string City { get; set; }

        [RegularExpression("([a-zA-Z0-9+ ?.'-]+)", ErrorMessage = "Please enter valid County.Only alpha-numerals, space, comma, hyphen accepted")]
        [MaxLength(50, ErrorMessage = "County must be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "County must be between 2 and 50 characters")]
        [Display(Name = "County")]
        public string County { get; set; }

        [Required(ErrorMessage = "Please enter the Country")]
        [MaxLength(50, ErrorMessage = "Employer name must be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "Employer name must be between 2 and 50 characters")]
        [Display(Name = "Country *")]
        public string Country { get; set; }

        [Display(Name = "Suite/Building")]
        [MaxLength(50, ErrorMessage = "Suite/Building must be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "Suite/Building must be between 2 and 50 characters")]
        [RegularExpression("([A-Za-z0-9_@./#&+-]+)", ErrorMessage = "Please enter valid Building.Only alpha-numerals, space, comma, hyphen ,hash accepted")]
        public string Building { get; set; }

        [Display(Name = "Street")]
        [MaxLength(50, ErrorMessage = "Street must be longer than 50 characters")]
        [MinLength(2, ErrorMessage = "Street must be minimum 2 characters.")]
        [RegularExpression("([a-zA-Z0-9+ ?|#|.'-]+)", ErrorMessage = "Please enter valid Street.Only alpha-numerals, space, comma, hyphen ,hash accepted")]
        public string Street { get; set; }

        [Display(Name = "ZipCode")]
        [RegularExpression("([a-zA-Z0-9+ ?-]+)", ErrorMessage = "Please enter valid Zipcode. Only alphabets, numbers and hyphens accepted")]
        [MaxLength(10, ErrorMessage = "ZipCode must be between 5 and 10 characters.")]
        [MinLength(5, ErrorMessage = "ZipCode must be between 5 and 10 characters.")]
        public string ZipCode { get; set; }

        [Display(Name = "Mobile Number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter only numbers in Mobile number. ")]
        [MaxLength(10, ErrorMessage = "Mobile number must be of 10 digits")]
        [MinLength(10, ErrorMessage = "Mobile number must be of 10 digits")]
        public string EmployerMobile { get; set; }

        [RequiredIfNotEmpty("EmployerMobile", ErrorMessage = "Employer mobile country code is required")]
        public string CountryCodeMobile { get; set; }

        [Display(Name = "Fax Number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter only numbers in Fax number. ")]
        [MaxLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        [MinLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        public string EmployerFax { get; set; }

        public string CountryCodeFax { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Please enter the valid email address")]
        public string EmployerEmail { get; set; }

        [Display(Name = "Job Title")]
        [MaxLength(50, ErrorMessage = "Job Title must be of 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "Job Title must be of 2 and 50 characters")]
        [RegularExpression("([a-zA-Z0-9 ?+.'-]+)", ErrorMessage = "Please enter valid JobTitle.Only Character, space, comma, hyphen accepted")]
        public string JobTitle { get; set; }
        
        [Display(Name = "Supervisor Name")]
        [MaxLength(50, ErrorMessage = "Supervisor Name must be between 2 and 50 characters.")]
        [MinLength(2, ErrorMessage = "Supervisor Name must be between 2 and 50 characters.")]
        [RegularExpression("([a-zA-Z ?+]+)", ErrorMessage = "Please enter valid Supervisor name.Only Character, space, comma, hyphen accepted")]
        public string SupervisorName { get; set; }  

        [Display(Name = "Department")]
        [MaxLength(50, ErrorMessage = "Department must be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "Department must be between 2 and 50 characters")]
        [RegularExpression("([a-zA-Z0-9 ?+]+)", ErrorMessage = "Please enter valid Department .Only alpha-numerals, space, comma, hyphen accepted")]
        public string Department { get; set; }

        [Display(Name = "Provider Type *")]
        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Provider Type is required")]
        public int ProviderTypeID { get; set; }

        [Required]
        [Display(Name = "Can Contact Employer Option")]
        public YesNoOption? CanContactEmployerOption { get; set; }

        [Display(Name = "Currently Working Here *")]
        [Required(ErrorMessage = "Please specify are you currently working here?")]
        public YesNoOption CurrentlyWorkingOption { get; set; }

        [Display(Name = "End Date ")]
        [RequiredIf("CurrentlyWorkingOption",(int)YesNoOption.NO,ErrorMessage="End Date is required")]
        [DateEnd(DateStartProperty = "StartDate", ErrorMessage = "End Date should be greater than start date")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "End Date should not be less than current date")]        
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Start Date *")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = "Start Date should not be less than current date & should fall between 50 years from now!!")]        
        public DateTime StartDate { get; set; }

        [RegularExpression("([A-Za-z0-9 ?_@./#&+-]+)", ErrorMessage = "Please enter Departure Reason .Only alpha-numerals, special characters accepted")]
        [MaxLength(50, ErrorMessage = "Reason For Departure should be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "Reason For Departure should be between 2 and 50 characters")]
        [Display(Name = "Reason For Departure")]
        public string DepartureReason { get; set; }

        public string WorkExperienceDocPath { get; set; }
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = "Please select the file of type .pdf, .jpeg, .png, .jpg, .bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Experience document should be less than 10mb in size")]
        public HttpPostedFileBase File { get; set; }
    }
}
