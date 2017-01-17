using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.AuditingList
{
    public class AuditingQueuesCountViewModel
    {
        public int ProviderQueueCount { get; set; }
        public int CoderQueueCount { get; set; }
        public int CommitteeQueueCount { get; set; }
        public int QcQueueCount { get; set; }
        public int RBQueueCount { get; set; }
        public int DraftListCount { get; set; }
        public int InactiveQueueCount { get; set; }
    }
}