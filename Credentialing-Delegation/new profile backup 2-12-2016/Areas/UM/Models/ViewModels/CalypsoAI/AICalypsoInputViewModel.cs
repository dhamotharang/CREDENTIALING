using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.CalypsoAI
{
    public class AICalypsoInputViewModel
    {
        public string SubscriberID { get; set; }
        public List<string> CptCodes { get; set; }
        public string ServiceOrAttendingSpeciality { get; set; }
        public string ServiceOrAttendingNPI { get; set; }
        public string FacilityNPI { get; set; }
    }
}