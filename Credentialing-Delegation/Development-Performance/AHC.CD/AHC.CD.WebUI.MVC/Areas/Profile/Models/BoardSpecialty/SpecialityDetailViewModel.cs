using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
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

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpecialty
{
    public class SpecialtyDetailViewModel
    {
        public int SpecialtyDetailID { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Specialty Type *")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Specialty Preference!!")]
        public PreferenceType PreferenceType { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Specialty Name *")]
        public int SpecialtyID { get; set; }

        [Range(0.00, 100.00)]
        [RegularExpression(RegularExpression.PERCENT_TWO_DECIMAL_PLACES, ErrorMessage = ValidationErrorMessage.PERCENT_TWO_DECIMAL_PLACES)]
        [Display(Name = "Percentage Of Time")]
        public Double? PercentageOfTime { get; set; }

        [Display(Name = "TaxonomyCode")]
        public string TaxonomyCode { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SPECIFY_LISTED)]
        [Display(Name = "HMO")]
        //[Range(1, int.MaxValue, ErrorMessage = "Select Yes or No for HMO!!")]
        public YesNoOption? ListedInHMOYesNoOption { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SPECIFY_LISTED)]
        [Display(Name = "PPO")]
        //[Range(1, int.MaxValue, ErrorMessage = "Select Yes or No for PPO!!")]
        public YesNoOption? ListedInPPOYesNoOption { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SPECIFY_LISTED)]
        [Display(Name = "POS")]
        //[Range(1, int.MaxValue, ErrorMessage = "Select Yes or No for POS!!")]
        public YesNoOption? ListedInPOSYesNoOption { get; set; }

        [Required(ErrorMessage = "Please specify whether you are Board Certified?")]
        [Display(Name = "Board Certified? *")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Board Certification!!")]
        public YesNoOption BoardCertifiedYesNoOption { get; set; }

        [RequiredIf("BoardCertifiedYesNoOption", (int)YesNoOption.YES, ErrorMessage = "SpecialtyBoardCertifiedDetail is required!!")]
        public SpecialtyBoardCertifiedDetailViewModel SpecialtyBoardCertifiedDetail { get; set; }

        [RequiredIf("BoardCertifiedYesNoOption", (int)YesNoOption.NO, ErrorMessage = "SpecialtyBoardNotCertifiedDetail is required!!")]
        public SpecialtyBoardNotCertifiedDetailViewModel SpecialtyBoardNotCertifiedDetail { get; set; }

        public StatusType? StatusType { get; set; }
    }
}