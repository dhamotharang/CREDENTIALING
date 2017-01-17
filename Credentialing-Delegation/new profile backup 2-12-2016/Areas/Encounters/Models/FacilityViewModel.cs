using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models
{
    public class FacilityViewModel
    {
        public string FacilityID { get; set; }

        [DisplayName("Place of Service")]
        public string FacilityName { get; set; }

        [DisplayName("Facility Address")]
        public string FacilityAddress { get; set; }
    }
}