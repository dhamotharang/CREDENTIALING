using AHC.CD.Entities.MasterData.Enums;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class EducationAddressViewModel
    {        

        public int SchoolInformationID { get; set; }

        [Required(ErrorMessage = "Please select the School Name")]
        [Display(Name = "School Name*")]
        public string SchoolName { get; set; }
        
        [Required]
        [RegularExpression(@"^[a-z]+[a-z0-9._]+@[a-z]+\.[a-z.]{2,5}$", ErrorMessage = "Please enter the valid Email Id")]        
        [Display(Name = "Email*")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter the Telephone Number")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter only 10 numbers in Telephone Number")]
        
        [Display(Name = "Telephone Number*")]
        public string Phone { get; set; }

        public string PhoneCountryCode { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please enter only 10 numbers for Fax number")]
        [Display(Name = "Fax")]
        public string Fax { get; set; }

        public string FaxCountryCode { get; set; }

        [Required(ErrorMessage = "Please enter the Suite/Building")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Suite/Building must be between 1 and 50 characters")]
        [RegularExpression(@"^[A-Za-z0-9]+$", ErrorMessage="Enter only alpha numeric ")]
        [Display(Name = "Suite/Building*")]
        public string Building { get; set; }

        [Required]
        [RegularExpression(@"^[A-Za-z0-9 \w#-.]+$", ErrorMessage = "Enter only alpha numeric")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [Display(Name = "Street*")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "State*")]
        public string State { get; set; }

        [Required]
        [Display(Name = "City*")]
        public string City { get; set; }

        
        [Display(Name = "County")]
        public string County { get; set; }

        [Required(ErrorMessage = "Please select the Country")]
        [Display(Name = "Country*")]
        public string Country { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Enter only numeric values")]        
        [Display(Name = "Zip code*")]
        public string ZipCode { get; set; }        
       
    }
}
