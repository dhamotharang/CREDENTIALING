using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.WorkHistory
{
    public class MilitaryServiceInformationViewModel
    {
        public int MilitaryServiceInformationID { get; set; }

        [Required]
        [Display(Name="Military Branch *")]
        public string MilitaryBranch { get; set; }

        [Required(ErrorMessage = "Please enter Start Date")]
        [Display(Name="Start Date *")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = "Start Date should be less than current date")]   
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please enter Discharge Date")]
        [Display(Name="Discharge Date *")]
        // Functional conflict may happen in below two validations
        [DateEnd(DateStartProperty = "StartDate", ErrorMessage = "Discharge Date should be greater than start date")]
        [GreaterThan("StartDate", ErrorMessage = "Discharge Date should be greater than start date")]
        public DateTime DischargeDate { get; set; }

        [Required]
        [Display(Name="Rank At Discharge *")]
        public string MilitaryRank { get; set; }

        
        [Display(Name="Type Of Discharge")]
        public string MilitaryDischarge { get; set; }

        [Display(Name="Other Than Honourable, Please Explain")]
        public string HonorableExplanation { get; set; }

        
        [Display(Name="Present Duty Status/Assignment")]
        public string MilitaryPresentDuty { get; set; }
    }
}
