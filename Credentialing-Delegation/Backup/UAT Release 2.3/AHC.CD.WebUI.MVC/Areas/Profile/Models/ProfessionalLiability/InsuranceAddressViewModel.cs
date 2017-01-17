using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalLiability
{
    public class InsuranceAddressViewModel
    {

        public int InsuranceAddressID { get; set; }
        [Required]
        public string Number { get; set; }

        [Required]
        public string Building { get; set; }

        [Required]
        public string Street { get; set; }

        [Required]
        public string State { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string Zipcode { get; set; }

        [Required]
        public string County { get; set; }

        [Required]
        public string Country { get; set; }

    
    }
}