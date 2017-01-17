using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ServiceModels
{
    public class ICDServiceModel
    {
        public int ICDCodeID { get; set; }

        public string ICDCodeNumber { get; set; }

        public string ICDCodeDescription { get; set; }

        public string ICDCodeVersion { get; set; }

        public string ICDCodeShortDescription { get; set; }
    }
}