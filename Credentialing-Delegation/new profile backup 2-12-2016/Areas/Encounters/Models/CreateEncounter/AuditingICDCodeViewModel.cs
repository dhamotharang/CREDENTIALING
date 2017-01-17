using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.CreateEncounter
{
    public class AuditingICDCodeViewModel
    {
        public string ICDCode { get; set; }

        public string CodeDescription { get; set; }

        public string HCCCode { get; set; }

        public string HCCVersion { get; set; }

        public string HCCDescription { get; set; }

        public string HCCWeight { get; set; }

        public string IsAgree { get; set; }

        public List<AuditingCategoryRemarks> AuditingCategoryRemarks { get; set; }
    }
}