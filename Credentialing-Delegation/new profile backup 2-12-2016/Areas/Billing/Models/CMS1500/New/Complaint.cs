using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Complaint
    {
        [Key]
        public long Complaint_PK_Id { get; set; }
        public Nullable<long> ComplaintCode { get; set; }
        public string Description { get; set; }
        public Nullable<long> Schedule_FK_Id { get; set; }
        public Nullable<bool> IsChiefCompliant { get; set; }
        public string LastModifiedBy { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }

        [ForeignKey("Schedule_FK_Id")]
        public virtual Schedule Schedule { get; set; }
    }
}