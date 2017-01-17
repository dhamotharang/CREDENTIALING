using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class OtherIdentificationNumberViewModel
    {
        public int OtherIdentificationNumberID { get; set; }

        [Required]
        [Display(Name = "NPI Number *")]
        public NPIDetailViewModel NPIDetails { get; set; }

        [Display(Name = "CAQH Number")]
        public CAQHDetailViewModel CAQHDetails { get; set; }

        [Display(Name = "UPIN Number")]
        public string UPINNumber { get; set; }

        [Display(Name = "USMLE Number")]
        public string USMLENumber { get; set; }

    }
}
