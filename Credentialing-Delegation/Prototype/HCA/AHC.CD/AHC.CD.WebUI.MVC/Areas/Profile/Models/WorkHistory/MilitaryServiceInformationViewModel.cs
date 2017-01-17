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

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name="Start Date *")]
        public DateTime StartDate { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name="Discharge Date *")]
        public DateTime DischargeDate { get; set; }

        [Required]
        [Display(Name="Rank At Discharge *")]
        public string RankAtDischarge { get; set; }

        [Required]
        [Display(Name="Type Of Discharge *")]
        public string DischargeType { get; set; }

        [Display(Name="Other Than Honorable, Please Explain")]
        public string OtherHonorable { get; set; }

        [Required]
        [Display(Name="Present Duty Status/Assignment")]
        public string PresentDutyStatus { get; set; }

        //public string ReserveStatus { get; set; }

        //[Required]
        //[Display(Name="I Have No Military Obligation *")]

        //public bool NoMilitaryObligation { get; set; }

        public bool CurrentlyOnMilitaryDuty { get; set; }

        public string ObligationExplanation { get; set; }

    }

    public enum  DutyStatus
    {
        ReserveStatus = 1,
        NoMilitaryObligation,
    }
}
