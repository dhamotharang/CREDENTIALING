using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class BillingInfoViewModel
    {

        public BillingInfoViewModel()
        {
            this.BilledEncounter = new HashSet<BilledEncounter>();
            this.BillingServiceAdjustment = new HashSet<BillingServiceAdjustment>();
            this.ClaimAdjustment = new HashSet<ClaimAdjustment>();
            this.BillingLog = new HashSet<BillingLog>();
            this.BillingNote = new HashSet<BillingNote>();
        }

        [Key]
        public long Billing_PK_Id { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string ClaimReferenceNumber { get; set; }
        public string BillingCtrlNo { get; set; }
        public Nullable<long> GenerationClaimId { get; set; }
        public Nullable<long> BillingReference_FK_Id { get; set; }
        public Nullable<int> BillingStatus_FK_Id { get; set; }
        public Nullable<long> CodedEncounter_FK_Id { get; set; }

        public virtual ICollection<BilledEncounter> BilledEncounter { get; set; }
        [ForeignKey("BillingStatus_FK_Id")]
        public virtual BillingStatus BillingStatus { get; set; }
        public virtual ICollection<BillingServiceAdjustment> BillingServiceAdjustment { get; set; }
        public virtual ICollection<ClaimAdjustment> ClaimAdjustment { get; set; }
        [ForeignKey("BillingReference_FK_Id")]
        public virtual BillingInfo BillingInfoReference { get; set; }
        public CodedEncounter CodedEncounter { get; set; }
        public virtual ICollection<BillingLog> BillingLog { get; set; }
        public virtual ICollection<BillingNote> BillingNote { get; set; }
    
    }
}