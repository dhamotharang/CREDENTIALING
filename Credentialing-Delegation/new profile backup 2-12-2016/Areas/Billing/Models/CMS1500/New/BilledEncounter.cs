using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class BilledEncounter
    {
        public BilledEncounter()
        {
            this.Billed_CPTs = new HashSet<Billed_CPTs>();
            this.Billed_ICDs = new HashSet<Billed_ICDs>();
        }

        [Key]
        public long BilledEncounter_PK_Id { get; set; }
        public Nullable<long> CodedEncounter_FK_Id { get; set; }
        public Nullable<long> BillingInfo_FK_Id { get; set; }


        public virtual ICollection<Billed_CPTs> Billed_CPTs { get; set; }
        public virtual ICollection<Billed_ICDs> Billed_ICDs { get; set; }
        [ForeignKey("BillingInfo_FK_Id")]
        public virtual BillingInfo BillingInfo { get; set; }
        [ForeignKey("CodedEncounter_FK_Id")]
        public virtual CodedEncounter CodedEncounter { get; set; }
    }
}