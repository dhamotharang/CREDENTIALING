using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.HospitalPrivilege
{
    public class HospitalPrivilegeInformationViewModel
    {
        public int HospitalPrivilegeInformationID { get; set; }
        
        [Required]
        [Display(Name="Do You Have Hospital Privileges? *")]
        public bool HaveHospitalPrivilege { get; set; }
        
        [Required]
        [Display(Name = "Primary *")]
        public bool Primary { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [Display(Name = "Hospital Name")]
        public string HospitalName { get; set; }

        [Required]
      
        [Display(Name = "Hospital Type")]
        public HospitalType HospitalType { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Alphabets Accepted!!")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }
         [Required]
         [Display(Name = "Number")]
        public string Number { get; set; }
         [Required]
         [Display(Name = "Suite/Building")]
        public string Building { get; set; }

   
        [Required]
        [DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = "Start Date should fall between 50 years from now!!")]
        [Display(Name = "From Date")]
        public DateTime FromDate { get; set; }

    
        [Required]
       [DateEnd(DateStartProperty = "FromDate", ErrorMessage = "Date should be greater than Start Date!!")]
        [Display(Name = "To Date")]
        public DateTime ToDate { get; set; }

        [Required]
        [Display(Name = "Staff Category")]
        public StaffCategory StaffCategory { get; set; }

        [Required]
        [Display(Name = "Explanation for status of Hospital")]
        public string HospitalStatusExplanation { get; set; }

        
        [Required]
        [Display(Name = "State *")]
        public string State { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Zip Code *")]
        public string Zipcode { get; set; }

        [Required]
        [Display(Name = "County")]
        public string County { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string HospitalPhone { get; set; }

        [Required]
        [Display(Name = "Fax")]
        public string HospitalFax { get; set; }

        [Required]
        [Display(Name = "Department Name *")]
        public string DepartmentName { get; set; }

        [Required]
        [Display(Name = "Department Chief *")]
        public string DepartmentChief { get; set; }

        [Required]
        [Display(Name = "Chief Of Staff *")]
        public string ChiefOfStaff { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = "Start Date should fall between 50 years from now!!")]
        [Display(Name = "Affiliation Start Date")]
        public DateTime AffilicationStartDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [DateEnd(DateStartProperty = "Affiliation Start Date", ErrorMessage = "Date should be greater than Start Date!!")]
        [Display(Name = "Affiliation End Date")]
        public DateTime AffiliationEndDate { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Alphabets Accepted!!")]
        [Display(Name = "Contact Person")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string ContactPerson { get; set; }

        [Required]
        [Display(Name = "Contact Person Phone Number")]
        public string ContactPersonPhone { get; set; }

        [Required]
        [Display(Name = "Contact Person Fax Number")]
        public string ContactPersonFax { get; set; }

        [Required]
        [Display(Name = "E-Mail Address")]
        [EmailAddress]
        public string EmailID { get; set; }

        [Required]
        [Display(Name = "Specialty")]
        public string Speciality { get; set; }

        [Required]
        [Display(Name = "Full Unrestricted Privileges")]
        public bool FullUnrestrictedPrevilages { get; set; }

        [Required]
        [Display(Name = "Admitting Privilege Status")]
        public string AddmittingPrivilegeStatus { get; set; }
     
        [Required]
        [Display(Name = "Are Privileges Temporary?")]
        public bool ArePrevilegesTemporary { get; set; }
        
        [Required]
        [Display(Name = "Annual Admission Percentage")]
        public double AnnualAdmisionPercentage { get; set; }
        
        [Required]
        [Display(Name = "Hospital Privilege Letter")]
        public string HospitalPrevilegeLetterPath { get; set; }
    }

    public enum HospitalType
    {
        Primary =1,
        Secondary
    }
    public enum StaffCategory
    {
        Active = 1,
        Inactive,
        Courtesy,
        Expelled,
        Suspended
    }
}
