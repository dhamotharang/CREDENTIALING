using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.ActivityLogging
{
    public class AuditMessage
    {
        public int ID { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public DateTime DateTime { get; set; }
        public string User { get; set; }
        public string URL { get; set; }
        public string QueryString { get; set; }
        public string Category { get; set; }
        public string IP { get; set; }
        public List<ParameterValue> Values { get; set; }
    }

    public class ParameterValue
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public object Value { get; set; }
    }
    
}
