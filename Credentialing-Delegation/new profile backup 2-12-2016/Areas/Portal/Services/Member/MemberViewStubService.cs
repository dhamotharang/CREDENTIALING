using PortalTemplate.Areas.Portal.IServices;
using PortalTemplate.Areas.Portal.Models.Contact;
using PortalTemplate.Areas.Portal.Models.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.Portal.Services.Member
{
    public class MemberViewStubService : IMemberViewService
    {
        public MemberViewModel GetMemberDetailsBySubsriberID(string subsriberId)
        {

            MemberViewModel MemberModel = GetStubMemberData(subsriberId);
            return MemberModel;
        }

        public object GetCOBInformationofMemberBySubscriberID(string subsriberId)
        {
            MemberViewModel MemberModel = GetStubMemberData(subsriberId);
            return MemberModel.COB;
        }
        public List<MemberRatingsOverrideViewModel> GetRatingOverRideInformationOfMemberBySubscriberID(string subsriberId)
        {
            MemberViewModel MemberModel = GetStubMemberData(subsriberId);
            return MemberModel.RatingsOveride;
        }
        public List<MemberImmunHealthScreeningViewModel> GetHealthScreeningInformationOfMemberBySubscriberID(string subsriberId)
        {
            MemberViewModel MemberModel = GetStubMemberData(subsriberId);
            return MemberModel.Immune;
        }
        public List<MemberAddressViewModel> GetAddressInformationOfMemberBySubscriberID(string subsriberId)
        {
            MemberViewModel MemberModel = GetStubMemberData(subsriberId);
            return MemberModel.Addresses;
        }
        public List<MemberEligibilityViewModel> GetEligibilityInformationOfMemberBySubscriberID(string subsriberId)
        {
            MemberViewModel MemberModel = GetStubMemberData(subsriberId);
            return MemberModel.Eligibility;
        }
        public List<MemberMedicareViewModel> GetMedicareInformationBySubscriberID(string subsriberId)
        {
            MemberViewModel MemberModel = GetStubMemberData(subsriberId);
            return MemberModel.Medicare;
        }
        public List<MemberProviderViewModel> GetProviderInformationRelatedToMemberBySubscriberID(string SubscriberId)
        {
            MemberViewModel MemberModel = GetStubMemberData(SubscriberId);
            return MemberModel.Provider;
        }
        public MemberEnrollmentViewModel GetEnrollementInformationOfMemberBySubscriberID(string SubscriberId)
        {
            MemberViewModel MemberModel = GetStubMemberData(SubscriberId);
            return MemberModel.Enrollment;
        }
        public MemberOtherDemographicsViewModel GetDemographicInformationBySubscriberID(string SubscriberId) {

            MemberViewModel MemberModel = GetStubMemberData(SubscriberId);
            return MemberModel.OtherDemographics;
        }
        private MemberViewModel GetStubMemberData(string subsriberId)
        {
            string file = HostingEnvironment.MapPath("~/Areas/Portal/Resources/ViewMember/MemberInformation.json");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            MemberViewModel MemberModel = new MemberViewModel();
            MemberModel = serial.Deserialize<MemberViewModel>(json);
            return MemberModel;
        }

        public MemberICEViewModel GetEmergencyContactInformationBySubscriberID(string SubscriberId)
        {
            MemberViewModel MemberModel = GetStubMemberData(SubscriberId);
            return MemberModel.ICE;
        }


        public MemberRepresentativeViewModel GetResponsiblePersonInformationBySubscriberID(string SubscriberId)
        {
            MemberViewModel MemberModel = GetStubMemberData(SubscriberId);
            return MemberModel.Representative;
        }


        public UM.Models.ViewModels.Authorization.MemberViewModel GetMemberInfoBySubscriberID(string SubscriberID)
        {
            throw new NotImplementedException();
        }
        public List<PortalTemplate.Areas.Portal.Models.Note.NoteViewModel> GetNotesRelatedToMemberBySubscriberID(string SubscriberId)
        {
            throw new NotImplementedException();
        }
        public List<AuthorizationContactViewModel> GetContactsRelatedToMemberBySubscriberID(string SubscriberId)
        {
            throw new NotImplementedException();
        }
        public List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel> GetAttachmentsRelatedToMemberBySubscriberID(string SubscriberId)
        {
            throw new NotImplementedException();
        }
        public object GetFilteredDataBySubsriberID(string module, List<string> moduleArray, string SubscriberID)
        {
            throw new NotImplementedException();
        }


        


        public object GetFilteredServiceDataBySubsriberID(string module, string serviceName, int ID)
        {
            throw new NotImplementedException();
        }
    }
}