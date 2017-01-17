using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ServiceModels
{
    public class ProviderServiceModel
    {
        public int ProviderID { get; set; }

        public string NPI { get; set; }
        
        public string SSN { get; set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Type { get; set; }
        
        public string Level { get; set; }
        
        public string PhoneNumber { get; set; }
        
        public string FaxNumber { get; set; }
        
        public string ContactName { get; set; }
        
        public string Speciality { get; set; }
        
        public string IPA { get; set; }
       
        public string StartDate { get; set; }

        public string EndDate { get; set; }
    }
}