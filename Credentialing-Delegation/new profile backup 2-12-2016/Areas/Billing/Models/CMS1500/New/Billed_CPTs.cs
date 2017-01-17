using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Billed_CPTs
    {
        [Key]
        public long BilledProcedure_PK_Id { get; set; }
        public Nullable<long> BilledEncounter_FK_Id { get; set; }
        public Nullable<long> Procedure_FK_Id { get; set; }
        public string Status { get; set; }
        public string StatusCode { get; set; }

        [ForeignKey("BilledEncounter_FK_Id")]
        public virtual BilledEncounter BilledEncounter { get; set; }
        [ForeignKey("Procedure_FK_Id")]
        public virtual Procedure Procedure { get; set; }
    }
}