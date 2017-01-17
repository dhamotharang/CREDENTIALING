using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Reason
    {
        public Reason()
        {
            //this.DiagnosisReasons = new HashSet<DiagnosisReason>();
            //this.ProcedureReasons = new HashSet<ProcedureReason>();
            //this.EncounterStatusReasons = new HashSet<EncounterStatusReason>();
            //this.ScheduleStatusReasons = new HashSet<ScheduleStatusReason>();
        }
        [Key]
        public int Reason_PK_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreateOn { get; set; }
        public int CategoryId { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
    }
}