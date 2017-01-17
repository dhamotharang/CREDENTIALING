using PortalTemplate.Areas.SharedView.Models.Encounter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.CreateCoding
{
    public class EncounterViewModel
    {
        public EncounterViewModel()
        {
            this.EncounterDetails = new EncounterDetailsViewModel();
            this.EncounterDecision = new EncounterDecisionViewModel();
        }
        public EncounterDetailsViewModel EncounterDetails { get; set; }
        [DisplayName("Audit Decision")]
        public EncounterDecisionViewModel EncounterDecision { get; set; }
    }
}