using PortalTemplate.Areas.CredAxis.Models.SpecialtyViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.SummaryViewModel
{
    public class CredentialingDetailsViewModel
    {
        public CredentialingDetailsViewModel()
        {
            //Specialties = new List<string>();
        }

        public string LocationType { get; set; }
        public string Location { get; set; }
        public string Specialties { get; set; }
        public string AgeLimit { get; set; }
        public string AssociatedPlans { get; set; }
    }
}