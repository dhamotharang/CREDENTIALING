using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.CreateEncounter
{
    public class AuditingICDCodeDetailsViewModel
    {
        public string ICDIndicator { get; set; }

        public List<AuditingICDCodeViewModel> ICDCodes { get; set; }
    }
}