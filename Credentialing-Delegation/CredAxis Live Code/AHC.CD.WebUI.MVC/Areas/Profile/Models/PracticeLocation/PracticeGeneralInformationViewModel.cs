using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class PracticeGeneralInformationViewModel
    {
        public int PracticeGeneralInformationID { get; set; }

        #region IsPrimary

        
        public string IsPrimary { get; private set; }

        [Required]
        [Display(Name = "Is it your primary practice location? *")]
        public YesNoOption PrimaryYesNoOption { get; set; }

        public string CurrentlyPracticingAtThisAddress { get; set; }

        [Required]
        [Display(Name="Currently Practicing At This Address? *")]
        public YesNoOption? CurrentlyPracticingYesNoOption { get; set; }
        
        #endregion

        [Column(TypeName = "datetime2")]
        [Display(Name = "Practice Start Date")]
        public DateTime? PracticeStartDate { get; set; }

        [Required]
        [Display(Name ="Corporate or Practice Name")]
        public string CorporateOrPracticeName { get; set; }

        [Display(Name = "TELEPHONE")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter only numbers in Telephone number. ")]
        [MaxLength(10, ErrorMessage = "Telephone number must be of 10 digits")]
        [MinLength(10, ErrorMessage = "Telephone number must be of 10 digits")]
        public string Telephone { get; set; }

        public string CountryCodeTelephone { get; set; }

        [Display(Name = "FAX")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Please enter only numbers in Fax number. ")]
        [MaxLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        [MinLength(10, ErrorMessage = "Fax number must be of 10 digits")]
        public string Fax { get; set; }

        public string CountryCodeFax { get; set; }

        [EmailAddress(ErrorMessage = "Please enter the valid email address")]
        [Display(Name="OFFICE E-MAIL ADDRESS")]
        public string OffcialEmailID { get; set; }

        #region SendGeneralCorrespondence
        
        public string SendGeneralCorrespondence { get; private set; }

        [Display(Name="SEND GENERAL CORRESPONDENCE HERE ?")]
        public YesNoOption? GeneralCorrespondenceYesNoOption { get; set; }

        #endregion

        #region practice exclusively within inpatient setting

        public string PracticeExclusively { get; private set; }

        [Display(Name = "Do you practice exclusively within inpatient setting ?")]
        public YesNoOption? PracticeExclusivelyYesNoOption { get; set; }

        #endregion

        [Display(Name = "Individual Tax ID")]
        [MinLength(9, ErrorMessage = "Individual Tax ID must be of 9 characters")]
        [MaxLength(9, ErrorMessage = "Individual Tax ID must be of 9 characters")]
        public string IndividualTaxID { get; set; }

        [Display(Name = "Group Tax ID")]
        [MinLength(9, ErrorMessage = "Group Tax ID must be of 9 characters")]
        [MaxLength(9, ErrorMessage = "Group Tax ID must be of 9 characters")]
        public string GroupTaxID { get; set; }

        #region PrimaryTaxId

        public string PrimaryTaxId { get; set; }

        [Display(Name = "Select primary tax ID")]
        public PrimaryTaxId? PrimaryTax { get; set; }
      
        #endregion

        #region address

        [Required(ErrorMessage = "Please enter the State")]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        //[MaxLength(100, ErrorMessage = "State must be between 1 and 100 characters")]
        //[MinLength(1, ErrorMessage = "State must be between 1 and 100 characters")]
        [Display(Name = "State *")]
        public string State { get; set; }

        [Required(ErrorMessage = "Please enter the City")]
       // [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
       // [MaxLength(100, ErrorMessage = "City must be between 1 and 100 characters")]
       // [MinLength(1, ErrorMessage = "City must be between 1 and 100 characters")]
        [Display(Name = "City *")]
        public string City { get; set; }

       // [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
       // [MaxLength(100, ErrorMessage = "County must be between 1 and 100 characters")]
       // [MinLength(1, ErrorMessage = "County must be between 1 and 100 characters")]
        [Display(Name = "County")]
        public string County { get; set; }

       // [Required(ErrorMessage = "Please enter the Country")]
       // [MaxLength(100, ErrorMessage = "Country name must be between 2 and 50 characters")]
       // [MinLength(1, ErrorMessage = "Country name must be between 2 and 50 characters")]
        [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
        [Display(Name = "Country *")]
        public string Country { get; set; }

        [Display(Name = "Suite/Building")]
      //  [MaxLength(50, ErrorMessage = "Suite/Building must be between 2 and 50 characters")]
      //  [MinLength(2, ErrorMessage = "Suite/Building must be between 2 and 50 characters")]
      //  [RegularExpression("([a-zA-Z0-9+ ?|#|.,'-]+)", ErrorMessage = "Please enter valid Building.Only alpha-numerals, space, comma, hyphen ,hash accepted")]
        public string Building { get; set; }

        [Required(ErrorMessage = "Please enter the Street")]
      //  [MaxLength(100, ErrorMessage = "Street must be between 1 and 100 characters")]
      //  [MinLength(1, ErrorMessage = "Street must be between 1 and 100 characters")]
      //  [RegularExpression("([a-zA-Z0-9+ ?|#|.,'-]+)", ErrorMessage = "Please enter valid Street.Only alpha-numerals, space, comma, hyphen ,hash accepted")]
        [Display(Name = "Street *")]
        public string Street { get; set; }

        
        [Display(Name = "Zip Code")]
        [RegularExpression("([a-zA-Z0-9+?-]+)", ErrorMessage = "Please enter valid Zipcode. Only alphabets, numbers and hyphens accepted")]
        [MaxLength(10, ErrorMessage = "ZipCode must be between 5 and 10 characters.")]
        [MinLength(5, ErrorMessage = "ZipCode must be between 5 and 10 characters.")]
        public string Zipcode { get; set; }

        #endregion
    }
}