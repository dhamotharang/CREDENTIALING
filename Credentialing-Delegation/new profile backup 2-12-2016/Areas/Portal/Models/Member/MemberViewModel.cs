using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.Member
{
    public class MemberViewModel
    {
        public string FistName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MemberID { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string SubscriberID { get; set; }
        public List<MemberCOBViewModel> COB { get; set; }
        public List<MemberRatingsOverrideViewModel> RatingsOveride { get; set; }
        public List<MemberImmunHealthScreeningViewModel> Immune { get; set; }
        public List<MemberAddressViewModel> Addresses { get; set; }
        public List<MemberEligibilityViewModel> Eligibility { get; set; }
        public List<MemberMedicareViewModel> Medicare { get; set; }
        public List<MemberProviderViewModel> Provider { get; set; }
        public MemberOtherDemographicsViewModel OtherDemographics { get; set; }
        public MemberICEViewModel ICE { get; set; }
        public MemberEnrollmentViewModel Enrollment { get; set; }
        public MemberInformationViewModel MemberInformation { get; set; }
        public MemberRepresentativeViewModel Representative { get; set; }

        public MemberViewModel()
        {
            MemberInformation = new MemberInformationViewModel();
            COB = new List<MemberCOBViewModel>();
            RatingsOveride = new List<MemberRatingsOverrideViewModel>();
            Representative = new MemberRepresentativeViewModel();
            Enrollment = new MemberEnrollmentViewModel();
            ICE = new MemberICEViewModel();
            OtherDemographics = new MemberOtherDemographicsViewModel();
            Provider = new List<MemberProviderViewModel>();
            Medicare = new List<MemberMedicareViewModel>();
            Eligibility = new List<MemberEligibilityViewModel>();
            Addresses = new List<MemberAddressViewModel>();
            Immune = new List<MemberImmunHealthScreeningViewModel>();
            RatingsOveride = new List<MemberRatingsOverrideViewModel>();

        }


      
    }
}