using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class DenialLOSViewModel
    {
        public int DenialLOSID { get; set; }

        public DateTime? DenialDate { get; set; }

        public string Reason { get; set; }
    }
}
