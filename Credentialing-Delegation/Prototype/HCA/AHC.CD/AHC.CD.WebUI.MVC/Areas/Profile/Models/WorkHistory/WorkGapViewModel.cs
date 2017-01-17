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
        [DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = "Start Date should fall between 50 years from now!!")]
        
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name="End Date *")]
        [DateEnd(DateStartProperty = "StartDate", ErrorMessage = "Should be greater than Start Date!!")]
        //[GreaterThan("StartDate", ErrorMessage = "Should be greater than Start Date!!")]
        public DateTime EndDate { get; set; }

       
        [MaxLength(50, ErrorMessage = "Description can't be longer than 50 characters")]
        [MinLength(2, ErrorMessage = "Description should be minimum 2 characters.")]
        [RequiredIfMonthGreaterThan(StartDependentProperty="StartDate", EndDependentProperty="EndDate", Range=6, ErrorMessage="Description is Required!!")]
        [Display(Name="Description")]
        public string Description { get; set; }

    }
}