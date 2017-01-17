using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
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

        [Required(ErrorMessage = "Please select the Board Name")]
        [Display(Name = "Board Name *")]
        public int SpecialtyBoardID { get; set; }

        [Required(ErrorMessage="Please select the Specialty Type")]
        [Display(Name = "Specialty Type *")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Specialty Preference!!")]
        public PreferenceType PreferenceType { get; set; }

        [Required(ErrorMessage = "Please select the Specialty Name")]
        [Display(Name = "Specialty Name *")]
        public int SpecialtyID { get; set; }

        [Range(0.00, 100.00)]
        [RegularExpression(@"(^[0-9]\d{0,4}(\.\d{1,1})?%?)\d{0,1}$", ErrorMessage = "Please enter valid Percentage Of Time. Only numbers and decimal accepted.")]
        //[RegularExpression(@"[0-9]{0,3}[.]{1}[0-9]{0,2}$", ErrorMessage = "Provide proper positive numeric values up to two decimal places only.")]
        [Display(Name = "% Of Time")]
        public Double? PercentageOfTime { get; set; }

        [Required(ErrorMessage = "Please specify whether you wish to be listed for HMO")]
        [Display(Name = "HMO *")]
        [Range(1, int.MaxValue, ErrorMessage = "Select Yes or No for HMO!!")]
        public YesNoOption? ListedInHMOYesNoOption { get; set; }

        [Required(ErrorMessage = "Please specify whether you wish to be listed for PPO")]
        [Display(Name = "PPO *")]
        [Range(1, int.MaxValue, ErrorMessage = "Select Yes or No for PPO!!")]
        public YesNoOption? ListedInPPOYesNoOption { get; set; }

        [Required(ErrorMessage = "Please specify whether you wish to be listed for POS")]
        [Display(Name = "POS *")]
        [Range(1, int.MaxValue, ErrorMessage = "Select Yes or No for POS!!")]
        public YesNoOption? ListedInPOSYesNoOption { get; set; }

        [Required(ErrorMessage = "Please specify whether you are Board Certified?")]
        [Display(Name = "Board Certified? *")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Board Certification!!")]
        public YesNoOption BoardCertifiedYesNoOption { get; set; }

        [RequiredIf("BoardCertifiedYesNoOption", (int)YesNoOption.YES, ErrorMessage = "SpecialtyBoardCertifiedDetail is required!!")]
        public SpecialtyBoardCertifiedDetailViewModel SpecialtyBoardCertifiedDetail { get; set; }

        [RequiredIf("BoardCertifiedYesNoOption", (int)YesNoOption.NO, ErrorMessage = "SpecialtyBoardNotCertifiedDetail is required!!")]
        public SpecialtyBoardNotCertifiedDetailViewModel SpecialtyBoardNotCertifiedDetail { get; set; }
    }
}