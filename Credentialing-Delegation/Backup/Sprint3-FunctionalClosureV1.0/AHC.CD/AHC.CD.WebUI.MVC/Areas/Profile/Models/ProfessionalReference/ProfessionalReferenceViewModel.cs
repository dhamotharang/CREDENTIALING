using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference
{
    public class ProfessionalReferenceViewModel
    {
        public int ProfessionalReferenceInfoID { get; set; }

        [Required(ErrorMessage = "Please select the Provider type")]
        [Display(Name="Provider Type *")]
        public int ProviderTypeID { get; set; }


        [Required(ErrorMessage = "Please enter First Name")]
        [Display(Name= "First Name *")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters")]
        [RegularExpression("([a-zA-Z]+)", ErrorMessage = "Please enter valid First Name. Only alphabets, space, comma, hyphen")]
        public string FirstName { get; set; }

        [Display(Name="Middle Name ")]
        [RegularExpression("([a-zA-Z]+)", ErrorMessage = "Please enter valid Middle Name. Only alphabets, space, comma, hyphen")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Middle Name must be less than 50 characters")]
       public string MiddleName { get; set; }


        [Required(ErrorMessage = "Please enter valid Middle Name")]
        [Display(Name="Last Name *")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last Name must be between 1 and 50 characters")]
        [RegularExpression("([a-zA-Z]+)", ErrorMessage = "Please enter valid last Name. Only alphabets, space, comma, hyphen")]
        public string LastName { get; set; }


      
        public string Degree { get; set; }

        [Required(ErrorMessage = "Please select a Specialty")]
        [Display(Name="Specialty *")]
        public int SpecialtyID { get; set; }


        [StringLength(20, MinimumLength = 2, ErrorMessage = "Relationship must be between 2 to 20 characters")]
        [RegularExpression("([a-zA-Z]+)", ErrorMessage = "Please enter a valid Relationship.Only alphabets accepted")]
        public string Relationship { get; set; }

        public string IsBoardCerified { get; private set; }

        [Display(Name = "Board Certified ? *")]
        [Required(ErrorMessage = "Please specify are you board certified?")]
        public YesNoOption? BoardCerifiedOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsBoardCerified))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsBoardCerified);
            }
            set
            {
                this.IsBoardCerified = value.ToString();
            }
        }

        [EmailAddress(ErrorMessage = "Please enter the valid email address. ")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Please enter the Apartment/Unit Number.")]
        [RegularExpression("([a-zA-Z 0-9.'-,]+)", ErrorMessage = "Please enter valid Apartment/Unit Number. Only alphabets, special characters and numbers accepted.")]
        [Display(Name="Suite/Building *")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Apartment/Unit Number must be between 1 and 50 characters")]
        public string Building { get; set; }

        [Required(ErrorMessage = "Please enter the Street Address")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Street Address must be between 1 and 100 characters")]
        [Display(Name="Street *")]
        [RegularExpression(@"[a-zA-Z0-9.'-]*$", ErrorMessage = "Please enter valid Street. Only alphabets, special characters and numbers accepted")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Please enter the State")]
        [Display(Name="State *")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "State  must be between 1 and 100 characters")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter the Country")]
        [Display(Name="Country *")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Country must be between 1 and 100 characters")]
        public string Country { get; set; }

      
        [Display(Name = "County ")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "County must be between 1 and 100 characters")]
        public string County { get; set; }

        [Required(ErrorMessage = "Please enter the City")]
        [Display(Name="City *")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "City must be between 1 and 100 characters")]
        public string City { get; set; }

        [Required]
        [Display(Name="Zip Code *")]
        [RegularExpression("([a-zA-Z 0-9]+)", ErrorMessage = " Only Alphanumeric accepted")]
        public string Zipcode { get; set; }

        [Required]
        [RegularExpression(@"^\d+$", ErrorMessage = "Mobile number must be only number")]
        [MaxLength(10, ErrorMessage = "Mobile number should be 10 characters")]
        [MinLength(10, ErrorMessage = "Mobile number should be 10 characters")]
        [Display(Name ="Telephone *")]
        public string Telephone { get; set; }

        [Required]
        public string PhoneCountryCode { get; set; }


        [RegularExpression(@"^\d+$", ErrorMessage = "Fax number must be only number")]
        [MaxLength(10, ErrorMessage = "Fax number should be 10 characters")]
        [MinLength(10, ErrorMessage = "Fax number should be 10 characters")]
        [Display(Name="Fax ")]
        public string Fax { get; set; }

        
        public string FaxCountryCode { get; set; }
    }
}