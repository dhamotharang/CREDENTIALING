using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class PersonalDetailViewModel
    {
        public int PersonalDetailID { get; set; }

        #region Salutation

        [Required(ErrorMessage = "Please select a Salutation.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a Salutation.")]
        [Display(Name = "Salutation *")]
        public SalutationType SalutationType { get; set; }
        
        #endregion

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "First Name *")]
    //    [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
      //  [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string FirstName { get; set; }
        
        [Display(Name = "Middle Name")]
        //[StringLength(50,ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string MiddleName { get; set; }

        [Display(Name = "Last Name *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
       // [StringLength(50, MinimumLength = 1, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string LastName { get; set; }

        [Display(Name = "Suffix")]
       // [StringLength(10, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX)]
       // [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN)]
        public string Suffix { get; set; }

        #region Gender

       // [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Gender")]
        public GenderType GenderType { get; set; }

        #endregion  
        
        [Display(Name = "Maiden Name")]
        //[RequiredIf("GenderType", (int)GenderType.Female, ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
    //    [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
      //  [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string MaidenName { get; set; }

        #region MaritalStatus

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        //[Range(1, int.MaxValue, ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Marital Status")]
        public MaritalStatusType? MaritalStatusType { get; set; }
        
        #endregion 

        [Display(Name = "Spouse Name")]
        //[RequiredIf("MaritalStatusType", (int)MaritalStatusType.Married, ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        //[RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_COMMA_HYPHEN_DOT, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE_COMMA_HYPHEN_DOT)]
        public string SpouseName { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Title *")]
        public ICollection<ProviderTitleViewModel> ProviderTitles { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Provider Level *")]
        public int ProviderLevelID { get; set; }
    }
}
