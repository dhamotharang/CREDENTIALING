using PortalTemplate.Areas.Portal.IManager.Member;
using PortalTemplate.Areas.Portal.IServices;
using PortalTemplate.Areas.Portal.Models.Member;
using PortalTemplate.Areas.Portal.Models.MemberManager;
using PortalTemplate.Areas.Portal.Services.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Manager.Manager
{
    public class MemberServiceManager : IMemberServiceManager
    {
        IMemberViewService memberViewService = new MemberService();
        IMemberHeaderService memberHeaderService = new MemberService();

        public MemberManagerModel GetMemberDetailsBySubsriberID(string SubscriberID)
        {
            MemberManagerModel manager = new MemberManagerModel();
            manager.ResultObject = memberViewService.GetMemberDetailsBySubsriberID(SubscriberID);
            manager.URL = "~/Areas/Portal/Views/Member/_MemberDetails.cshtml";
            return manager;
        }
        public MemberManagerModel GetMemberHeaderDetailsBySubscriberID(string SubscriberID,string RefID)
        {
            MemberManagerModel manager = new MemberManagerModel();
            MemberHeaderViewModel memberHeader = memberHeaderService.GetMemberHeaderDetailsBySubscriberID(SubscriberID);
            memberHeader.REFID = RefID;
            manager.ResultObject = memberHeader;
            manager.URL = "~/Areas/Portal/Views/Member/_MemberHeader.cshtml";
            return manager;
            
        }
        public MemberManagerModel GetFilteredDataBySubsriberID(string module, List<string> moduleArray, string SubscriberID)
        {
            object Result;
            string url;
            switch (module)
            {
                case "NOTE":
                    //Result = memberViewService.GetNotesRelatedToMemberBySubscriberID(SubscriberID);
                    url = "~/Areas/Portal/Views/Member/Tabs/_NotesArea.cshtml";
                    break;
                case "CONTACT":
                    //Result = memberViewService.GetContactsRelatedToMemberBySubscriberID(SubscriberID);
                    url = "~/Areas/Portal/Views/Member/Tabs/_ContactsArea.cshtml";
                    break;
                case "ATTACHMENT":
                    //Result = memberViewService.GetAttachmentsRelatedToMemberBySubscriberID(SubscriberID);
                    url = "~/Areas/Portal/Views/Member/Tabs/_AttachmentArea.cshtml";
                    //url = "~/Areas/Portal/Views/Member/Tabs/_Attachments.cshtml";
                    break;
                default:
                     //Result = memberViewService.GetNotesRelatedToMemberBySubscriberID(SubscriberID);
                    url = "~/Areas/Portal/Views/Member/Tabs/_NotesArea.cshtml";
                    break;
            }
            Result = memberViewService.GetFilteredDataBySubsriberID(module, moduleArray, SubscriberID);
            //TODO: Implement module array filter
            MemberManagerModel manager = new MemberManagerModel();
            manager.URL = url;
            manager.ResultObject = Result;
            return manager;
        }
        public MemberManagerModel GetMemberPartialTab(string tabId, string SubscriberId)
        {
            object Result;
            string url;
            List<string> module = new List<string>();
            switch (tabId)
            {
                case "COB":
                     Result = memberViewService.GetCOBInformationofMemberBySubscriberID(SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_COB.cshtml";
                     break;
                case "RATING":
                     Result = memberViewService.GetRatingOverRideInformationOfMemberBySubscriberID(SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_RatingOverride.cshtml";
                     break;
                case "IMMUNE":
                     Result = memberViewService.GetHealthScreeningInformationOfMemberBySubscriberID(SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_ImmunHealthScreening.cshtml";
                     break;
                case "ADDRESS":
                     Result = memberViewService.GetAddressInformationOfMemberBySubscriberID(SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_Address.cshtml";
                     break;
                case "ELIGIBILITY":
                     Result = memberViewService.GetEligibilityInformationOfMemberBySubscriberID(SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_Eligibility.cshtml";
                     break;
                case "MEDICARE":
                     Result = memberViewService.GetMedicareInformationBySubscriberID(SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_Medicare.cshtml";
                     break;
                case "PROVIDER":
                     Result = memberViewService.GetProviderInformationRelatedToMemberBySubscriberID(SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_ProviderInformation.cshtml";
                     break;
                case "OTHERDEMOGRAPHICS":
                     Result = memberViewService.GetDemographicInformationBySubscriberID(SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_OtherDemo.cshtml";
                     break;
                case "ENROLLMENT":
                     Result = memberViewService.GetEnrollementInformationOfMemberBySubscriberID(SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_Enrollment.cshtml";
                     break;
                case "ICE":
                     Result = memberViewService.GetEmergencyContactInformationBySubscriberID(SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_ICE.cshtml";
                     break;
                case "REPRESENTATIVE":
                     Result = memberViewService.GetResponsiblePersonInformationBySubscriberID(SubscriberId);
                      url = "~/Areas/Portal/Views/Member/Tabs/_ResponsiblePerson.cshtml";
                     break;
                case "NOTE":
                     module.Add("UM");
                     Result = memberViewService.GetFilteredDataBySubsriberID(tabId,module, SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_Notes.cshtml";
                     break;
                case "CONTACT": 
                     module.Add("UM");
                     Result = memberViewService.GetFilteredDataBySubsriberID(tabId, module, SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_Contacts.cshtml";
                     break;
                case "ATTACHMENT": 
                     module.Add("UM");
                     Result = memberViewService.GetFilteredDataBySubsriberID(tabId, module, SubscriberId);
                     url = "~/Areas/Portal/Views/Member/Tabs/_Attachments.cshtml";
                     break;
                default:
                     Result = null;
                     url = "/url/for/fourofour";//TODO:Give the url for 404
                     break;
            }

            MemberManagerModel manager = new MemberManagerModel();
            manager.URL = url;
            manager.ResultObject = Result;
            return manager;
        }
      
        
    }
}