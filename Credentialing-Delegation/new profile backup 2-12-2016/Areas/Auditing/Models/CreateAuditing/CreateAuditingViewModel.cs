using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.SharedView.Models.Encounter;

namespace PortalTemplate.Areas.Auditing.Models.CreateAuditing
{
    public class CreateAuditingViewModel
    {
        public CreateAuditingViewModel()
        {
            this.CodingDetails = new CodingDetailsViewModel();
            this.EncounterDetails = new EncounterInfoViewModel();
        }
        public EncounterInfoViewModel EncounterDetails { get; set; }
        public CodingDetailsViewModel CodingDetails { get; set; }
       
    }
}