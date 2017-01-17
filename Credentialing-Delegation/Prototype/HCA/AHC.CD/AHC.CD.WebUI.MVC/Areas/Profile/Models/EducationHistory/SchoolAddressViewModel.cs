using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class SchoolAddressViewModel
    {
        public int SchoolAddressID { get; set; }
        [Required]
        [Display(Name = "Number")]
        public string Number { get; set; }
        [Required]
        [Display(Name = "Building")]
        public string Building { get; set; }
        [Required]
        [Display(Name = "Street")]
        public string Street { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "County")]
        public string County { get; set; }
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "Zipcode")]
        public string Zipcode { get; set; }
    }
}
