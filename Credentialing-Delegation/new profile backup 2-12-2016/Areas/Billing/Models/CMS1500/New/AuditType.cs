using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class AuditType
    {
        [Key]
        public int AuditType_PK_ID { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }
    }
}