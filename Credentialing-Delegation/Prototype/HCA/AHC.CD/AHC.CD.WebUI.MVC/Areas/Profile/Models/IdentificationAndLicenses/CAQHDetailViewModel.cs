using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class CAQHDetailViewModel
    {
        public int CAQHDetailID { get; set; }

        [MaxLength(100)]
        [Index(IsUnique = true)]
        [Display(Name = "CAQH Number *")]
       
        public string Number { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}