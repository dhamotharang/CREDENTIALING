using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models
{
    public class ApplicationFormDataViewModel
    {
        public int ApplkicationFormDataID { get; set; }

        public ProfileData ProfileData { get; set; }

        public NonProfileData NonProfileData { get; set; }
    }
}
