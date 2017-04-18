using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.CCMPortal
{
    public class CCMAppiontment
    {
        public CCMAppiontment()
        {
            //setting default status as false
            SelectStatus = false;
        }

        public int Provider_ID { get; set; }

        public string NPI { get; set; }

        public string ProviderName { get; set; }

        public string Specialty { get; set; }

        public string PlanName { get; set; }

        public DateTime AppointmentDate { get; set; }

        public DateTime CredInitiationDate { get; set; }

        public string RecommendedLevel { get; set; }

        public int ProviderCredentialingInfoID { get; set; }

        public string Status { get; set; }

        public string CCM { get; set; }

        public bool SelectStatus { get; set; }



    }
}
