using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.DTO
{
   public class ProviderServiceDTO
    {
        public List<string> PlanNames { get; set; }
        public List<object> Specialties { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public string ContactName { get; set; }
        public string EmailID { get; set; }
        public object CurrentPraticeLocation { get; set; }
        public ProviderPracticeLocationAddressDTO ProviderPracticeLocationAddress { get; set; }
    }
}
