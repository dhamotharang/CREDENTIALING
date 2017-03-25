using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Credentialing
{
    public class InitiateCreadentialingVM
    {

        public int ProviderID { get; set; }

        public List<int> SelectedPlans { get; set; }

        public string Remarks { get; set; }

        public string CredentialedBy { get; set; }


    }
}