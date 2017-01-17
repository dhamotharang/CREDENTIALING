using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class MemberProfileViewModel
    {
        public MemberProfileViewModel()
        {
            ContactInformation = new List<ContactInformationViewModel>();
            AddressInformation = new List<AddressInformationViewModel>();

            ContactSchedulesInformation = new List<ContactScheduleViewModel>();
            DeductionsInformation = new List<DeductionViewModel>();
            Disabilities = new List<DisabilitiesViewModel>();
            Diseases = new List<DiseasesViewModel>();

            Educations = new List<EducationViewModel>();
            Documents = new List<DocumentViewModel>();
            Incomes = new List<IncomeViewModel>();

            Languages = new List<LanguageViewModel>();
            MemberProfileAddons = new List<MemberProfileAddonViewModel>();

            MemberMemberships = new List<MemberMembershipViewModel>();
            MembershipHistories = new List<MembershipHistoryViewModel>();

            IdentificationInformation = new List<IdentificationInformationViewModel>();
            //IdentificationHistories = new List<IdentificationHistoryViewModel>();

            BankDetails = new List<BankDetailViewModel>();
            Notes = new List<NoteViewModel>();
        }

        public int MemberProfileId { get; set; }

        public string UMID { get; set; }

        public string UUID { get; set; }

        public string PersonId { get; set; }

        public string NextOfKinId { get; set; }

        public string Status { get; set; }

        public string SourceCode { get; set; }

        public string SourceName { get; set; }

        public string TimeStamp { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }


        [Display(Name = "Patient Account Number")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ProfileAccountID { get; set; }

        public PersonalInformationViewModel PersonalInformation { get; set; }

        public List<ContactInformationViewModel> ContactInformation { get; set; } //Member has Many of Contacts

        public List<AddressInformationViewModel> AddressInformation { get; set; } //Member has Many of Addresses

        public List<IdentificationInformationViewModel> IdentificationInformation { get; set; }

        public List<ContactScheduleViewModel> ContactSchedulesInformation { get; set; }

        public List<DeductionViewModel> DeductionsInformation { get; set; }

        public List<DisabilitiesViewModel> Disabilities { get; set; }

        public List<DiseasesViewModel> Diseases { get; set; }

        public List<DocumentViewModel> Documents { get; set; }

        public List<EducationViewModel> Educations { get; set; }

        public List<IncomeViewModel> Incomes { get; set; }

        public List<LanguageViewModel> Languages { get; set; }

        public List<MemberProfileAddonViewModel> MemberProfileAddons { get; set; }

        //public List<IdentificationHistoryViewModel> IdentificationHistories { get; set; }

        public List<NoteViewModel> Notes { get; set; }

        public List<BankDetailViewModel> BankDetails { get; set; }

        public OtherPersonInformationViewModel OtherPersonInformation { get; set; }

        public List<MemberMembershipViewModel> MemberMemberships { get; set; }

        public List<MembershipHistoryViewModel> MembershipHistories { get; set; }

    }
}