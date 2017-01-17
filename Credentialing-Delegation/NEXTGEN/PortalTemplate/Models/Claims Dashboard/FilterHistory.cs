using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.Claims_Dashboard
{
    public class FilterHistory
    {
        public int ID { get; set; }

        public string DivId { get; set; }

        public string FilterName { get; set; }

        public List<FilterElement> Elements { get; set; }
    }
}