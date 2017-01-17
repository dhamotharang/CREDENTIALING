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
    public class PublicHealthServiceViewModel
    {
        public int PublicHealthServiceID { get; set; }

        [Display(Name = "Last Location *")]
        [Required(ErrorMessage = "Please enter last location")]
        public string LastLocation { get; set; }

        [Required(ErrorMessage = "Please enter Start Date")]
        [Display(Name = "Start Date *")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-50", ErrorMessage = "Start Date should be less than current date")]        
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Please enter End Date")]
        [Display(Name = "End Date *")]
        [GreaterThan("StartDate", ErrorMessage = "End Date should be greater than start date")]
        public DateTime EndDate { get; set; }
    }
}