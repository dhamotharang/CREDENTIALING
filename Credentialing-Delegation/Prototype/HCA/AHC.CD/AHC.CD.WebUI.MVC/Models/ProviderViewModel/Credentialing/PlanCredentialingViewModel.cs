using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Credentialing
{
    public class PlanCredentialingViewModel
    {

        public string Status { get; set; }

        public PlanViewModel Plan { get; set; }

        public string Speciality { get; set; }

        public string Type { get; set; }

        public string Location { get; set; }

        public string GroupName { get; set; }

    }
}