using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Models.PratianComponents
{
    public class Filter
    {
        public string PropertyName { get; set; }
        public Operation Operation { get; set; }
        public object Value { get; set; }
        public AndOR AndOR = AndOR.AND;
    }
}