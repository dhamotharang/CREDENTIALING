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

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class TrainingDetailViewModel
    {
        
        public int TrainingDetailID { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Affiliated University/Hospital")]
        public string HospitalName { get; set; }

        public EducationAddressViewModel SchoolInformation { get; set; }

        //[Required(ErrorMessage = "Please specify if you completed your Education in this institution ?*")]
        [Display(Name = "Did you complete your training at this school ?")]
        public YesNoOption? CompletedYesNoOption { get; set; }

        //[RequiredIf("CompletedYesNoOption", (int)YesNoOption.NO, ErrorMessage="Enter the reason")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        [RegularExpression(RegularExpression.ALPHA_SPACE, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_ALPHABETS_SPACE)]
        [Display(Name = "If No, Give the reason")]
        public string InCompleteReason { get; set; }

        public ICollection<ResidencyInternshipDetailViewModel> ResidencyInternshipDetails { get; set; }
        
    }
}
