using PortalTemplate.Areas.Portal.Models.MemberManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.IManager.Member
{
    public interface IMemberServiceManager
    {
        MemberManagerModel GetMemberPartialTab(string tabId, string SubscriberId);
        MemberManagerModel GetMemberHeaderDetailsBySubscriberID(string SubscriberID,string RefID);
        MemberManagerModel GetMemberDetailsBySubsriberID(string SubscriberID);
        MemberManagerModel GetFilteredDataBySubsriberID(string module,List<string> moduleArray,string SubscriberID);
        
    }
}