using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.SchedulerDTO
{
    public class CalendarEventDTO
    {
        public string Status { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public CalendarEventDescription Description { get; set; }
    }
}