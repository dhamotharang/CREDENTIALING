using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
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

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name="Provider Type *")]
        public int ProviderTypeID { get; set; }

        [Required(ErrorMessage = "Please enter First Name")]
        [Display(Name= "First Name *")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name must be between 2 and 50 characters")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = "Please enter valid First Name. Only alphabets, space, comma, dot and hyphen accepted")]
        public string FirstName { get; set; }

        [Display(Name="Middle Name ")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = "Please enter valid Middle Name. Only alphabets, space, comma, dot and hyphen accepted")]
        [StringLength(50, MinimumLength = 0, ErrorMessage = "Middle Name must be between 0 and 50 characters.")]
        public string MiddleName { get; set; }

       // [Required(ErrorMessage = "Please enter Last Name.")]
        [Display(Name="Last Name ")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Last Name must be between 1 and 50 characters")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = "Please enter valid Last Name. Only alphabets, space, comma, dot and hyphen accepted")]
        public string LastName { get; set; }
      
        public string Degree { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name="Specialty")]
        public int? SpecialtyID { get; set; }
      
        [StringLength(20, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string Relationship { get; set; }

        public string IsBoardCerified { get; private set; }

        [Display(Name = "Board Certified ?")]
        //[Required(ErrorMessage = "Please specify are you board certified?")]
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

        [EmailAddress(ErrorMessage = ValidationErrorMessage.INVALID_EMAIL_FORMAT)]
        public string Email { get; set; }
     
        [Display(Name="Suite/Building")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Apartment/Unit Number must be between 1 and 50 characters")]
        public string Building { get; set; }

       // [Required(ErrorMessage = "Please enter the Street/P. O. Box No: Address")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [Display(Name="Street/P. O. Box No: *")]
        public string Street { get; set; }

        //Required(ErrorMessage = "Please enter the State")]
        [Display(Name="State")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string State { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name="Country")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string Country { get; set; }
      
        [Display(Name = "County ")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string County { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name="City")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        public string City { get; set; }

        //[Required(ErrorMessage= ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name="Zip Code")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_NUMBER_HYPHEN, ErrorMessage = "Please enter valid ZipCode. Only alphabets, numbers and hyphens accepted.")]
        [StringLength(10, MinimumLength = 5, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string Zipcode { get; set; }

        //[Required(ErrorMessage = "Please enter the Telephone  number")]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = "Please enter only numbers for Telephone number")]
        [Display(Name ="Telephone")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Telephone number must be of 10 digits")]
        public string Telephone { get; set; }
     
        public string PhoneCountryCode { get; set; }

        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = "Please enter only numbers for Fax number")]
        [Display(Name="Fax")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Fax number must be of 10 digits")]
        public string Fax { get; set; }
        
        public string FaxCountryCode { get; set; }

        public StatusType? StatusType { get; set; }
    }
}