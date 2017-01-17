using PortalTemplate.Areas.Portal.IServices;
using PortalTemplate.Areas.Portal.Models.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.Portal.Services.Member
{
    public class MemberHeaderStubService : IMemberHeaderService
    {
        public MemberHeaderViewModel GetMemberHeaderDetailsBySubscriberID(string SubscriberId)
        {

            string file = HostingEnvironment.MapPath("~/Areas/Portal/Resources/ViewMember/MemberHeader.json");
            string json = System.IO.File.ReadAllText(file);
            JavaScriptSerializer serial = new JavaScriptSerializer();
            MemberHeaderViewModel MemberModel = new MemberHeaderViewModel();
            MemberModel = serial.Deserialize<MemberHeaderViewModel>(json);
            return MemberModel;
        }
    }
}