using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class MidlevelEmployeeViewModel
    {
        public int EmployeeID { get; set; }

        public int PracticeLocationDetailID { get; set; }

        #region Name

        [Display(Name = "Last Name *")]
        // [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
      //  [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
      //  [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
      //  [StringLength(50, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
      //  [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string MiddleName { get; set; }

        [Display(Name = "First Name *")]
        // [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
    //    [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
    //    [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string FirstName { get; set; }

        #endregion

        #region Address

        // [Required(ErrorMessage = "Please enter the State")]
       //[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE, ErrorMessage = ValidationErrorMessage.ALPHABETS_SPACE_COMMA_HYPHEN_DOT_QUOTES_APOSTROPHE)]
       //[MaxLength(100, ErrorMessage = "State must be between 1 and 100 characters")]
       //[MinLength(1, ErrorMessage = "State must be between 1 and 100 characters")]
        [Display(Name = "State *")]
        public string State { get; set; }

        #endregion
    }
}