using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class ScheduleStatusReason
    {
        [Key]
        public long ScheduleStatusReasonID { get; set; }
        public int ScheduleStatus_FK_Id { get; set; }
        public int Reason_FK_Id { get; set; }
        //  public long Schedule_FK_Id { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }

        [ForeignKey("Reason_FK_Id")]
        public virtual Reason Reason { get; set; }
        //[ForeignKey("Schedule_FK_Id")]
        //public virtual Schedule Schedule { get; set; }
        [ForeignKey("ScheduleStatus_FK_Id")]
        public virtual ScheduleStatus ScheduleStatus { get; set; }
    }
}