using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models
{
    public enum EncounterStatus
    {
        SCHEDULED = 1, RESCHEDULED = 2, ACTIVE = 3, OPEN = 4, READYTOCODE = 5, NOSHOW = 6, DRAFT = 7, INACTIVE= 8
    }
}