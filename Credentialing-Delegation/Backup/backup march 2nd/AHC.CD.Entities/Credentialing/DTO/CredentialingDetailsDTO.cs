using AHC.CD.Entities.Credentialing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing.DTO
{
    public class CredentialingDetailsDTO
    {
        public int ProviderID { get; set; }
        //public IEnumerable<Plan> credentialingPlans { get; set; }
        public string Remarks { get; set; }
        public string CredentialedBy { get; set; }
    }
}
