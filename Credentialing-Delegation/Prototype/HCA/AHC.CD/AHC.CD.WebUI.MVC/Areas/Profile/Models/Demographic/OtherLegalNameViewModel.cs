using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class OtherLegalNameViewModel
    {
        public int OtherLegalNameID { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Alphabets Accepted!!")]
        [Display(Name = "Other First Name *")]
        public string OtherFirstName { get; set; }

        [Display(Name = "Other Middle Name")]
        [StringLength(50, ErrorMessage = "{0} must be of max {1} characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Alphabets Accepted!!")]
        public string OtherMiddleName { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Only Alphabets Accepted!!")]
        [Display(Name = "Other Last Name *")]
        public string OtherLastName { get; set; }

        [Display(Name = "Suffix (JR,III)")]
        public string Suffix { get; set; }

        [Required]
        [DateStart(MaxPastYear="0", MinPastYear="-100", ErrorMessage = "Start Date should fall between 100 years from now!!")]
        [Display(Name = "Date start using other name *")]
        public DateTime StartDate { get; set; }

        [Required]
        [DateEnd(DateStartProperty = "StartDate", ErrorMessage="Should be greater than Start Date!!")]
        [Display(Name = "Date stop using other name *")]
        public DateTime EndDate { get; set; }

        public string DocumentPath { get; set; }

        public HttpPostedFileBase File { get; set; }

        public string Status { get; set; }

        public ActiveInactive ActiveInactive
        {
            get;
            set;
        }
    }
    public enum ActiveInactive
    {
        Active = 1,
        Inactive
    }
}
