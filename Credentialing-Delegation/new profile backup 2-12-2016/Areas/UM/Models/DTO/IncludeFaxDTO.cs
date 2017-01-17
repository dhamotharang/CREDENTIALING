using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.DTO
{
    public class IncludeFaxDTO
    {
        public String EntityType { get; set; }
        public int EntityId { get; set; }
        public bool IncFaxStatus { get; set; }
    }
}