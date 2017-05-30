using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.DTO
{
    public class AuditMessageDTO
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string DateTime { get; set; }
        public string User { get; set; }
        public string URL { get; set; }
        public string Category { get; set; }
        public string IP { get; set; }
    }
}
