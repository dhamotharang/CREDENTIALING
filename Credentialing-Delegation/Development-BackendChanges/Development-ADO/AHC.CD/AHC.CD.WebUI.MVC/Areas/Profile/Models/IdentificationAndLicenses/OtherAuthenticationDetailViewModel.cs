using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class OtherAuthenticationDetailViewModel
    {
        public int OtherAuthenticationDetailID { get; set; }
        
        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
