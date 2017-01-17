using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class ScheduleNote
    {
        [Key]
        public long ScheduleNote_PK_Id { get; set; }
        public Nullable<long> Notes_FK_Id { get; set; }
        public Nullable<long> Schedule_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }

        [ForeignKey("Notes_FK_Id")]
        public virtual Note Note { get; set; }
        [ForeignKey("Schedule_FK_Id")]
        public virtual Schedule Schedule { get; set; }
    }
}
