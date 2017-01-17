using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class HomeAddressViewModel
    {
        public int HomeAddressID { get; set; }

        [Required]
        [Display(Name = "Number *")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Apartment/Unit Number *")]
        public string UnitNumber { get; set; }

        [Required]
        [Display(Name = "Street Address *")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "State *")]
        public string State { get; set; }

        [Required]
        [Display(Name = "City *")]
        public string City { get; set; }

        [Required]
        [Display(Name = "County *")]
        public string County { get; set; }

        [Required]
        [Display(Name = "Country *")]
        public string Country { get; set; }

        [Required]
        [Display(Name = "Zip Code *")]
        public string ZipCode { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Living Here From *")]
        public DateTime LivingFromDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        [Display(Name = "Living Here To *")]
        public DateTime LivingEndDate { get; set; }

        [Required]
        [Display(Name = "Is Present Home Address ?")]
        public bool IsPresentlyStaying { get; set; }

        [Display(Name = "Is Primary Home Address ?")]
        public bool IsPrimary { get; set; }

        [Required]
        public string AddressPreference { get; set; }

        [Required]
        public string Status { get; set; }
    }
}
