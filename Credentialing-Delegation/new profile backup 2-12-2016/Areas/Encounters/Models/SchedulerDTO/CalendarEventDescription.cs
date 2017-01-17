using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.SchedulerDTO
{
    public class CalendarEventDescription
    {
        public string MemberName { get; set; }
        public string ProviderName { get; set; }
        public string Facility { get; set; }
        public string ChiefComplaint { get; set; }
    }
}