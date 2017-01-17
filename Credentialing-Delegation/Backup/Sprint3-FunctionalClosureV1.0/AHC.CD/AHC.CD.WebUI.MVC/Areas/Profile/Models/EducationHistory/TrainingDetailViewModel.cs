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
    public class TrainingDetailViewModel
    {
        
        public int TrainingDetailID { get; set; }
        
        [Required]
        [Display(Name = "Affiliated University/Hospital*")]
        public string HospitalName { get; set; }

        public EducationAddressViewModel SchoolInformation { get; set; }        

        [Required]
        [Display(Name = "Did you complete the training at this institution?*")]
        public YesNoOption CompletedYesNoOption { get; set; }

        [RequiredIf("CompletedYesNoOption", (int)YesNoOption.NO, ErrorMessage="Enter the reason")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} Characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Enter only alphabets")]
        [Display(Name = "If No, Give the reason*")]
        public string InCompleteReason { get; set; }

        public ICollection<ResidencyInternshipDetailViewModel> ResidencyInternshipDetails { get; set; }
        
    }
}
