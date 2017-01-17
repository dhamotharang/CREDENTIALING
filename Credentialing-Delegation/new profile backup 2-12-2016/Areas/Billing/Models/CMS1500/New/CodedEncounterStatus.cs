using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class CodedEncounterStatus
    {
        public CodedEncounterStatus()
        {
            //this.CodedEncounters = new HashSet<CodedEncounter>();
        }
        [Key]
        public int CodedEncounterStatus_PK_Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public Nullable<int> StatusType_FK_ID { get; set; }

        //public virtual ICollection<CodedEncounter> CodedEncounters { get; set; }
        [ForeignKey("StatusType_FK_ID")]
        public virtual StatusType StatusType { get; set; }
    }
}