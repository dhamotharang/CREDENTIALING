using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.Claims_Dashboard
{
    public class Adjustment
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Count { get; set; }
        public string Cost { get; set; }
        public string Description { get; set; }
    }
}