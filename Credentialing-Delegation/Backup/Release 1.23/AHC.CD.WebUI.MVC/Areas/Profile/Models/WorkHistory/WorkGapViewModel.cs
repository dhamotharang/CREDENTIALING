using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.WorkHistory
{
    public class WorkGapViewModel
    {
        public int WorkGapID { get; set; }

        [Required(ErrorMessage = "Please enter Start Date")]
        [Display(Name="Start Date *")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = "Start Date should be less than current date!!")]
        //[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please enter End Date")]
        [Display(Name="End Date *")]
        [GreaterThan("StartDate", ErrorMessage = "End Date should be greater than start date!!")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "End Date should be less than current date")]
        //[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime EndDate { get; set; }


        [MaxLength(500, ErrorMessage = "Description should be between 2 and 500 characters")]
        [MinLength(2, ErrorMessage = "Description should be between 2 and 500 characters")]
        //[RequiredIfMonthGreaterThan(StartDependentProperty="StartDate", EndDependentProperty="EndDate", Range=3, ErrorMessage="Description is required when the gap is more than three months!!")]
        [Display(Name="Description")]
        public string Description { get; set; }
    }
}