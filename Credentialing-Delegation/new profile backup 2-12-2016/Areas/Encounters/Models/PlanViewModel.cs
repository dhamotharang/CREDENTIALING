using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.Encounters.Models
{
    public class PlanViewModel
    {
        public int PlanID { get; set; }

        [DisplayName("Plan Name")]
        public string PlanName { get; set; }
    }
}
