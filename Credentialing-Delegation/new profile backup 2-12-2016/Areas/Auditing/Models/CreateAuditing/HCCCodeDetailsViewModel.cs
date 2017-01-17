using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.CreateAuditing
{
    public class HCCCodeDetailsViewModel
    {
        public string HCCCode { get; set; }
        public string HCCType { get; set; }
        public string HCCVersion { get; set; }
        public string HCCDescription { get; set; }
        public string HCCWeight { get; set; }
    }
}