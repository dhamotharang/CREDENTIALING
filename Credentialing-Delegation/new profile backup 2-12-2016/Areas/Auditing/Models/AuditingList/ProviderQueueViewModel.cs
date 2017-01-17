using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.AuditingList
{
    public class ProviderQueueViewModel
    {
        public int EncounterId { get; set; }

        public int MemberId { get; set; }

        public string MemberLastName { get; set; }

        public string MemberFirstName { get; set; }

        public string ProviderNPI { get; set; }

        public string ProviderLastName { get; set; }

        public string ProviderFirstName { get; set; }

        public string Facility { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DOS { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DOC { get; set; }

        public string CreatedBy { get; set; }

        public string EncounterType { get; set; }

        public string Status { get; set; }
    }
}