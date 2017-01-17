using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class ClaimDate
    {

        [Key]
        public long ClaimDate_PK_Id { get; set; }
        public string DateQlfrFormat { get; set; }
        public Nullable<DateTime> Date { get; set; }
        public Nullable<int> DateQlfr_FK_Id { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string LastModifiedBY { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<long> Encounter_FK_ID { get; set; }
        public int DateQlfrCode { get; set; }

        [ForeignKey("Encounter_FK_ID")]
        public virtual Encounter Encounter { get; set; }
        [ForeignKey("DateQlfrCode")]
        public virtual DateQlfr DateQlfr { get; set; }
    }
}