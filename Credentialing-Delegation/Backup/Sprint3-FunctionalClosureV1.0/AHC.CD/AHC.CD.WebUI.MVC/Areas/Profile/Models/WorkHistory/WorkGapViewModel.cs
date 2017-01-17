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

        [Required]
        [Display(Name="Start Date *")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = "Start Date should not be less than current date!!")]        
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name="End Date *")]
        [DateEnd(DateStartProperty = "StartDate", ErrorMessage = "Should be greater than Start Date!!")]
        [GreaterThan("StartDate", ErrorMessage = "End Date should be greater than start date!!")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "End Date should not be less than current date")]        
        public DateTime EndDate { get; set; }


        [MaxLength(50, ErrorMessage = "Description should be between 2 and 50 characters")]
        [MinLength(2, ErrorMessage = "Description should be between 2 and 50 characters")]
        [RequiredIfMonthGreaterThan(StartDependentProperty="StartDate", EndDependentProperty="EndDate", Range=3, ErrorMessage="Description is required when the gap is more than three months!!")]
        [Display(Name="Description")]
        public string Description { get; set; }
    }
}