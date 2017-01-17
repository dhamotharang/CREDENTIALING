using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Billed_ICDs
    {
        [Key]
        public long Billed_ICDs_PK_Id { get; set; }
        public Nullable<long> DiagnosisInfo_FK_Id { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }
        public Nullable<long> BilledEncounter_FK_Id { get; set; }

        [ForeignKey("BilledEncounter_FK_Id")]
        public virtual BilledEncounter BilledEncounter { get; set; }
        [ForeignKey("DiagnosisInfo_FK_Id")]
        public virtual Diagnosis Diagnosis { get; set; }
    }
}