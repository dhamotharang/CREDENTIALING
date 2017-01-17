using PortalTemplate.Areas.Portal.Models.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortalTemplate.Areas.Portal.IServices
{
    interface IMemberHeaderService
    {
        MemberHeaderViewModel GetMemberHeaderDetailsBySubscriberID(string SubscriberId);

    }
}
