using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.Dashboard
{
    public class EncounterDashboardViewModel
    {
        public EnounterBiscuitViewModel EnounterBiscuitViewModel { get; set; }

        public List<ReasonsForOnHoldEncountersViewModel> ReasonsForOnHoldEncountersViewModel { get; set; }
    }
}