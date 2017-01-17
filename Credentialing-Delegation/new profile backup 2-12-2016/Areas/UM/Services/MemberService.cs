using Newtonsoft.Json;
using PortalTemplate.Areas.UM.IServices;
using PortalTemplate.Areas.UM.Models.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.Services
{
    public class MemberService :IMemberService
    {
        public List<MemberSearchResultViewModel> GetAllMembers(SearchMember searchParams)
        {
            List<MemberSearchResultViewModel> SearchResultData;
           {
               string file = HostingEnvironment.MapPath("~/Areas/UM/Resources/ServiceData/UMSearchMemberServiceData.txt");
               string json = System.IO.File.ReadAllText(file);

                SearchResultData = JsonConvert.DeserializeObject<List<MemberSearchResultViewModel>>(json);
  
            }

            return SearchResultData;
        }
    }
}