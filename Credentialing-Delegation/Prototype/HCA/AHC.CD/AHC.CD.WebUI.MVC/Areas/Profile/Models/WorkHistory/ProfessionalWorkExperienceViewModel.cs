using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.WorkHistory
{
    public class ProfessionalWorkExperienceViewModel
    {
        public ProfessionalWorkExperienceViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int ProfessionalWorkExperienceID { get; set; }
        
        public int EmployeerDetailID { get; set; }

        [Required]
        [Display(Name = "Employer Name *")]
        [MaxLength(50, ErrorMessage = "EmployerName should be longer than 50 characters")]
        [MinLength(2, ErrorMessage = "EmployerName should be minimum 2 characters.")]
        [RegularExpression("([a-zA-Z0-9.'-]+)", ErrorMessage = "Allowed alpha-numerals and Special characters (.) (-) (‘)")]
        public string EmployerName { get; set; }

        [Required]
        [Display(Name = "Supervisor Name *")]
        [MaxLength(50, ErrorMessage = "Employer Name should be longer than 50 characters")]
        [MinLength(2, ErrorMessage = "Employer Name should be minimum 2 characters.")]
        [RegularExpression("([a-zA-Z]+)", ErrorMessage = "Allowed only alphabets")]
        public string SupervisorName { get; set; }

        [Required]
        [Display(Name = "Job Title *")]
        public string JobTitle { get; set; }
        [Required]
        [Display(Name = "Mobile Number *")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Mobile number must be only number")]
        [MaxLength(10, ErrorMessage = "Mobile number should be 10 characters")]
        [MinLength(10, ErrorMessage = "Mobile number should be 10 characters")]
        public string EmployerMobile { get; set; }
        [Required]
        public string CountryCodeMobile { get; set; }
        [Required]
        [Display(Name = "Fax Number *")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Fax number must be only number")]
        [MaxLength(10, ErrorMessage = "Fax number should be 10 characters")]
        [MinLength(10, ErrorMessage = "Fax number should be 10 characters")]
        public string EmployerFax { get; set; }
        [Required]
        public string CountryCodeFax { get; set; }
        [Required]
        [Display(Name = "May we contact your employer ? *")]
        public bool CanContactEmployer { get; set; }
        [Required]
        [Display(Name = "Department *")]
        [MaxLength(50, ErrorMessage = "Department should be longer than 50 characters")]
        [MinLength(2, ErrorMessage = "Department should be minimum 2 characters.")]
        [RegularExpression("([a-zA-Z]+)", ErrorMessage = "Allowed only alphabets")]
        public string Department { get; set; }
        [Required]
        [Display(Name = "Title *")]
        public string Title { get; set; }
        [Display(Name = "Experience Document")]
        public string ResumePath { get; set; }

        public int OtherWorkInfoID { get; set; }
        [MaxLength(50, ErrorMessage = "Reason can't be longer than 50 characters")]
        [MinLength(2, ErrorMessage = "Reason should be minimum 2 characters.")]
        public string DepartureReason { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        public DateTime EndDate { get; set; }

        public int WorkAddressID { get; set; }
        [Required]
        [Display(Name = "State *")]
        public string State { get; set; }
        [Required]
        [Display(Name = "City *")]
        public string City { get; set; }
        [Required]
        [Display(Name = "County *")]
        public string County { get; set; }
        [Required]
        [Display(Name = "Country *")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Number *")]
        [MaxLength(50, ErrorMessage = "Number can't be longer than 50 characters")]
        [MinLength(2, ErrorMessage = "Number should be minimum 2 characters.")]
        public string Number { get; set; }
        [Required]
        [Display(Name = "Suite/Building *")]
        public string Building { get; set; }
        [Required]
        [Display(Name = "Street *")]
        public string Street { get; set; }
        [Required]
        [Display(Name = "Zipcode *")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Zipcode must be only number")]
        [MaxLength(5, ErrorMessage = "Zipcode should be 5 characters")]
        [MinLength(5, ErrorMessage = "Zipcode should be 5 characters")]
        public string Zipcode { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }


    }
}
