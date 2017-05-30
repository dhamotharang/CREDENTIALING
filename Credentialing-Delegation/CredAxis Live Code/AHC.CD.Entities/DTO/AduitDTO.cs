using AHC.ActivityLogging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.DTO
{
    public class AduitDTO
    {
        public AduitDTO()
        {
            AuditMessages = new List<AuditMessageDTO>();
        }
        public int TotalLogCount { get; set; }
        public int InformationLogCount { get; set; }
        public int AlertLogCount { get; set; }
        public List<AuditMessageDTO> AuditMessages { get; set; }
    }
}
