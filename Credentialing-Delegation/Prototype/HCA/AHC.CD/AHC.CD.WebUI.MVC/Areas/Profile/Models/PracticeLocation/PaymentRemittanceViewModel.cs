using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class PaymentRemittanceViewModel
    {
        public int PaymentRemittanceID { get; set; }

        [Required]
        [Display(Name = "First Name *")]
        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Last Name *")]
        public string LastName { get; set; }

        public PracticeAddressViewModel PracticeAddress { get; set; }

        [Required]
        [Display(Name = "telephone *")]
        public string Telephone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "E-Mail Address")]
        public string EmailID { get; set; }
    }
}