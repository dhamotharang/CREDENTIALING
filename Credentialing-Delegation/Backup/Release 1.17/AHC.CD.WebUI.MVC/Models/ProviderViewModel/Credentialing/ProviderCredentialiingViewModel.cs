using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Credentialing
{
    public class ProviderCredentialiingViewModel
    {
       public ProviderCredentialiingViewModel()
        {
            this.relatedPlans = new List<PlanCredentialingViewModel>();
            this.Specialities = new List<string>();
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public string NPI { get; set; }

         public bool IsSelected { get; set; }    

        public List<string> Specialities{set;get;}
        public List<PlanCredentialingViewModel> relatedPlans { get; set; }

    }
}