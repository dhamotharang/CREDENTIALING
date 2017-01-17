using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class PrimaryCredentialingContactViewModel
    {
        public int PrimaryCredentialingContactID { get; set; }

        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Number")]
        public string Number { get; set; }

        [Display(Name = "Street/P. O. Box No:")]
        public string Street { get; set; }

        [Display(Name = "Suite/Building")]
        public string Building { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State")]
        public string State { get; set; }

        [Display(Name = "ZIP Code")]
        public string Zipcode { get; set; }

        [Required]
        [Display(Name = "Telephone *")]
        public string Telephone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Email ID")]
        public string EmailID { get; set; }
    }
}