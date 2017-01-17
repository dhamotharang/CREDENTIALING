using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class CodedEncounterNote
    {
        [Key]
        public long CodedEncounterNote_PK_Id { get; set; }
        public Nullable<long> Note_FK_Id { get; set; }
        public Nullable<long> CodingInfo_FK_Id { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        [ForeignKey("CodingInfo_FK_Id")]
        public virtual CodedEncounter CodedEncounter { get; set; }
        [ForeignKey("Note_FK_Id")]
        public virtual Note Note { get; set; }
    }
}