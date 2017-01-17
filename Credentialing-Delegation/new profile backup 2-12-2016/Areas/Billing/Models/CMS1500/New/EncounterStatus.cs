using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class EncounterStatus
    {
        public EncounterStatus()
        {
            //this.Encounter = new HashSet<Encounter>();
            this.EncounterStatusReason = new HashSet<EncounterStatusReason>();
        }
        [Key]
        public int EncounterStatus_PK_Id { get; set; }
        public string Title { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }

        //  public virtual ICollection<Encounter> Encounter { get; set; }
        public virtual ICollection<EncounterStatusReason> EncounterStatusReason { get; set; }
    }
}