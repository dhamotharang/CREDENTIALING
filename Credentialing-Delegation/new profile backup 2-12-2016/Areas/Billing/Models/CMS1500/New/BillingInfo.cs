using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class BillingInfo
    {
        public BillingInfo()
        {
            this.BilledEncounter = new HashSet<BilledEncounter>();
            this.BillingServiceAdjustment = new HashSet<BillingServiceAdjustment>();
            this.ClaimAdjustment = new HashSet<ClaimAdjustment>();
            this.BillingLog = new HashSet<BillingLog>();
            this.BillingNote = new HashSet<BillingNote>();
        }

        public long Billing_PK_Id { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public string LastModifiedBy { get; set; }
        public string ClaimReferenceNumber { get; set; }

        [Display(Name = "Patient Account Number")]
        public string BillingCtrlNo { get; set; }
        public Nullable<long> GenerationClaimId { get; set; }
        public Nullable<long> BillingReference_FK_Id { get; set; }
        public Nullable<int> BillingStatus_FK_Id { get; set; }
        public Nullable<long> CodedEncounter_FK_Id { get; set; }
        public double AmountPaid { get; set; }
        public DateTime? HospitalisationAdmissionDateFrom { get; set; }
        public DateTime? HospitalisationAdmissionDateTo { get; set; }
        public ClaimDate OtherDate { get; set; }
        public DateTime? DatePatientUnableToWorkFrom { get; set; }
        public DateTime? DatePatientUnableToWorkTo { get; set; }
        public ClaimDate DateOfCurrrentIllness { get; set; }
        public ClaimDate DateOfInitialTreatment { get; set; }
        public ClaimDate LatestConsultationDate { get; set; }

        public virtual ICollection<BilledEncounter> BilledEncounter { get; set; }
        public virtual BillingStatus BillingStatus { get; set; }
        public virtual ICollection<BillingServiceAdjustment> BillingServiceAdjustment { get; set; }
        public virtual ICollection<ClaimAdjustment> ClaimAdjustment { get; set; }

        public virtual BillingInfo BillingInfoReference { get; set; }
        public CodedEncounter CodedEncounter { get; set; }
        public virtual ICollection<BillingLog> BillingLog { get; set; }
        public virtual ICollection<BillingNote> BillingNote { get; set; }


        public OtherBillingInfo OtherBillingInfo { get; set; }
    }
}
