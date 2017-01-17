using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using AHC.CD.Entities.MasterData.Enums;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.WorkHistory
{
    public class PublicHealthServiceViewModel
    {
        public int PublicHealthServiceID { get; set; }

        [Display(Name = "Last Location *")]
        [Required(ErrorMessage = "Please enter last location")]
     //   [MaxLength(50, ErrorMessage="Last Location should be less than 100 characters")]
        public string LastLocation { get; set; }

        //[Required(ErrorMessage = "Please enter Start Date")]
        [Display(Name = "Start Date")]
        //[DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = "Start Date should be less than current date")]
        //[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? StartDate { get; set; }

        //[Required(ErrorMessage = "Please enter End Date")]
        [Display(Name = "End Date")]
        [DateEnd(DateStartProperty = "StartDate", ErrorMessage = "End Date should be greater than start date.")]
        //[GreaterThan("StartDate", ErrorMessage = "End Date should be greater than start date")]
        //[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? EndDate { get; set; }

        public StatusType? StatusType { get; set; }
    }
}