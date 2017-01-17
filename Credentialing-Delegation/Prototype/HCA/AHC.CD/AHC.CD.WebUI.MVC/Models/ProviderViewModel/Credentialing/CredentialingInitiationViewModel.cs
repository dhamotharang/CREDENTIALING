using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Credentialing
{
    public class CredentialingInitiationViewModel
    {
       public CredentialingInitiationViewModel()
        {
           // this.relatedPlans = new List<PlanCredentialingViewModel>();
            this.Specialities = new List<string>();
            this.Groups = new List<string>();
            this.Plan = new List<string>();
        }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        public string NPI { get; set; }

        public string CAQH { get; set; }

        public string Type { get; set; }
        public string CredType { get; set; }
        public DateTime CredDate { get; set; }

        public List<string> Groups { get; set; }

      
        public List<string> Plan { get; set; }

         public List<string> Specialities { get; set; }
      //  public List<PlanCredentialingViewModel> relatedPlans { get; set; }

    }
}