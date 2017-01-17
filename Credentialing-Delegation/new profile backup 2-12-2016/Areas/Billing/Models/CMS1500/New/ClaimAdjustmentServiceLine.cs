using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class ClaimAdjustmentServiceLine
    {
        [Key]
        public long CASCode_PK_Id { get; set; }
        public Nullable<long> ProcedureInformation_FK_Id { get; set; }
        public Nullable<long> CASCode { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        [ForeignKey("ProcedureInformation_FK_Id")]
        public virtual Procedure Procedure { get; set; }
    }
}