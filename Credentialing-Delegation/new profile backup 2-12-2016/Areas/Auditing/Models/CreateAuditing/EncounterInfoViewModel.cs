using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.SharedView.Models.Encounter;
using System.ComponentModel;


namespace PortalTemplate.Areas.Auditing.Models.CreateAuditing
{
    public class EncounterInfoViewModel
    {
        public EncounterInfoViewModel()
        {
            this.EncounterDetails = new EncounterDetailsViewModel();
            this.Categories = new List<CategoryViewModel>();
        }
        public EncounterDetailsViewModel EncounterDetails { get; set; }

        [DisplayName("Audit Decision")]
        public Boolean IsAgree { get; set; }
        public List<CategoryViewModel> Categories { get; set; }
    }
}