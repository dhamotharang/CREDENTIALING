using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class StatusType
    {
        public StatusType()
        {
            //   this.CodedEncounterStatus = new HashSet<CodedEncounterStatus>();
        }
        [Key]
        public int StatusType_PK_ID { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}