using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class MemberPCPViewModel 
    {
        public string ProviderID { get; set; }

        public string NPI { get; set; }

        public string Name { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        public string TaxID { get; set; }

        public string Specialty { get; set; }
  
        public string ContactName { get; set; }

        public string ProviderType { get; set; }

        public string ProviderStatus { get; set; }

        public string PlanName { get; set; }

        public string PhysicianGroupName { get; set; }

      
    }
}