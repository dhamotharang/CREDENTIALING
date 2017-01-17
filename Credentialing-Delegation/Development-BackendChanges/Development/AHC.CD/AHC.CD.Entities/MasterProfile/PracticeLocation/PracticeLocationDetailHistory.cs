using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeLocationDetailHistory
    {
        public PracticeLocationDetailHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PracticeLocationDetailHistoryID { get; set; }

        #region General Information

        #region IsPrimary

        //[Required]
        public string IsPrimary { get; set; }

        [NotMapped]
        public YesNoOption? PrimaryYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.IsPrimary))
                    return null;

                if (this.IsPrimary.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.IsPrimary);
            }
            set
            {
                this.IsPrimary = value.ToString();
            }
        }

        #endregion

        #region PracticeExclusively

        public string PracticeExclusively { get; private set; }

        [NotMapped]
        public YesNoOption? PracticeExclusivelyYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.PracticeExclusively))
                    return null;

                if (this.PracticeExclusively.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.PracticeExclusively);
            }
            set
            {
                this.PracticeExclusively = value.ToString();
            }
        }

        #endregion

        #region CurrentlyPracticingAtThisAddress

        //[Required]
        public string CurrentlyPracticingAtThisAddress { get; set; }

        [NotMapped]
        public YesNoOption? CurrentlyPracticingYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.CurrentlyPracticingAtThisAddress))
                    return null;

                if (this.CurrentlyPracticingAtThisAddress.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.CurrentlyPracticingAtThisAddress);
            }
            set
            {
                this.CurrentlyPracticingAtThisAddress = value.ToString();
            }
        }

        #endregion

        #region SendGeneralCorrespondence

        public string SendGeneralCorrespondence { get; private set; }

        [NotMapped]
        public YesNoOption? GeneralCorrespondenceYesNoOption
        {
            get
            {
                if (String.IsNullOrEmpty(this.SendGeneralCorrespondence))
                    return null;

                if (this.SendGeneralCorrespondence.Equals("Not Available"))
                    return null;

                return (YesNoOption)Enum.Parse(typeof(YesNoOption), this.SendGeneralCorrespondence);
            }
            set
            {
                this.SendGeneralCorrespondence = value.ToString();
            }
        }

        #endregion

        #region PrimaryTaxId

        public string PrimaryTaxId { get; private set; }

        [NotMapped]
        public PrimaryTaxId? PrimaryTax
        {
            get
            {
                if (String.IsNullOrEmpty(this.PrimaryTaxId))
                    return null;

                if (this.PrimaryTaxId.Equals("Not Available"))
                    return null;

                return (PrimaryTaxId)Enum.Parse(typeof(PrimaryTaxId), this.PrimaryTaxId);
            }
            set
            {
                this.PrimaryTaxId = value.ToString();
            }
        }

        #endregion

        public string PracticeLocationCorporateName { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? StartDate { get; set; }

        #endregion

        public int? BusinessOfficeManagerOrStaffId { get; set; }
        [ForeignKey("BusinessOfficeManagerOrStaffId")]
        public Employee BusinessOfficeManagerOrStaff { get; set; }

        public int? PaymentAndRemittanceId { get; set; }
        [ForeignKey("PaymentAndRemittanceId")]
        public PracticePaymentAndRemittance PaymentAndRemittance { get; set; }

        public int? OfficeHourId { get; set; }
        [ForeignKey("OfficeHourId")]
        public virtual ProviderPracticeOfficeHour OfficeHour { get; set; }

        public virtual OpenPracticeStatus OpenPracticeStatus { get; set; }

        public int? BillingContactPersonId { get; set; }
        [ForeignKey("BillingContactPersonId")]
        public Employee BillingContactPerson { get; set; }

        public ICollection<PracticeProvider> PracticeProviders { get; set; }

        public int? PrimaryCredentialingContactPersonId { get; set; }
        [ForeignKey("PrimaryCredentialingContactPersonId")]
        public Employee PrimaryCredentialingContactPerson { get; set; }

        public virtual WorkersCompensationInformation WorkersCompensationInformation { get; set; }

        //[Required]
        public int? FacilityId { get; set; }
        [ForeignKey("FacilityId")]
        public AHC.CD.Entities.MasterData.Account.Branch.Facility Facility { get; set; }

        public int? OrganizationId { get; set; }
        [ForeignKey("OrganizationId")]
        public AHC.CD.Entities.MasterData.Account.Organization Organization { get; set; }

        public int? PracticingGroupId { get; set; }
        [ForeignKey("PracticingGroupId")]
        public virtual AHC.CD.Entities.MasterData.Account.PracticingGroup Group { get; set; }

        public string GroupName { get; set; }

        #region History Status

        public string HistoryStatus { get; private set; }

        [NotMapped]
        public HistoryStatusType? HistoryStatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.HistoryStatus))
                    return null;

                if (this.HistoryStatus.Equals("Not Available"))
                    return null;

                return (HistoryStatusType)Enum.Parse(typeof(HistoryStatusType), this.HistoryStatus);
            }
            set
            {
                this.HistoryStatus = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
