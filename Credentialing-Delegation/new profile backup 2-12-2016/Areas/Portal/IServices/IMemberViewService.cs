
using PortalTemplate.Areas.Portal.Models.Contact;
using PortalTemplate.Areas.Portal.Models.Member;
using PortalTemplate.Areas.Portal.Models.Note;
using PortalTemplate.Areas.Portal.Models.PriorAuth.Attachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Portal.IServices
{
    public interface IMemberViewService
    {
        MemberViewModel GetMemberDetailsBySubsriberID(string SubscriberId);
        object GetCOBInformationofMemberBySubscriberID(string SubscriberId);
        List<MemberRatingsOverrideViewModel> GetRatingOverRideInformationOfMemberBySubscriberID(string SubscriberId);
        List<MemberImmunHealthScreeningViewModel> GetHealthScreeningInformationOfMemberBySubscriberID(string SubscriberId);
        List<MemberAddressViewModel> GetAddressInformationOfMemberBySubscriberID(string SubscriberId);
        List<MemberEligibilityViewModel> GetEligibilityInformationOfMemberBySubscriberID(string SubscriberId);
        List<MemberMedicareViewModel> GetMedicareInformationBySubscriberID(string SubscriberId);
        List<MemberProviderViewModel> GetProviderInformationRelatedToMemberBySubscriberID(string SubscriberId);
        MemberOtherDemographicsViewModel GetDemographicInformationBySubscriberID(string SubscriberId);
        MemberICEViewModel GetEmergencyContactInformationBySubscriberID(string SubscriberId);
        MemberRepresentativeViewModel GetResponsiblePersonInformationBySubscriberID(string SubscriberId);
        MemberEnrollmentViewModel GetEnrollementInformationOfMemberBySubscriberID(string SubscriberId);
        List<NoteViewModel> GetNotesRelatedToMemberBySubscriberID(string SubscriberId);
        object GetFilteredServiceDataBySubsriberID(string module, string serviceName,int ID);
        List<PortalTemplate.Areas.Portal.Models.Attachment.AttachmentViewModel> GetAttachmentsRelatedToMemberBySubscriberID(string SubscriberId);
        List<AuthorizationContactViewModel> GetContactsRelatedToMemberBySubscriberID(string SubscriberId);
        object GetFilteredDataBySubsriberID(string module, List<string> moduleArray, string SubscriberID);
        //object GetFilteredDataBySubsriberID(string module, List<string> moduleArray, string SubscriberID);
        PortalTemplate.Areas.UM.Models.ViewModels.Authorization.MemberViewModel GetMemberInfoBySubscriberID(string SubscriberID);
        
        //ProviderViewModal GetPCPInfo(string SubscriberID);
    }
}
