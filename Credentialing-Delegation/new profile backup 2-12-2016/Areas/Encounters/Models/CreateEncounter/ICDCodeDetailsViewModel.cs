using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.CreateEncounter
{
    public class ICDCodeDetailsViewModel
    {
        public string ICDCodeType { get; set; }

        public List<ICDCodeViewModel> ICDCodes { get; set; }
    }
}