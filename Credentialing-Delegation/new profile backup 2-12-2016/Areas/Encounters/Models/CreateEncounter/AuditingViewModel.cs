using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.CreateEncounter
{
    public class AuditingViewModel
    {

        public PrimaryEncounterViewModel EncounterDetails { get; set; }

        public PortalTemplate.Areas.Auditing.Models.CreateAuditing.CodingDetailsViewModel CodingDetails { get; set; }

    }
}