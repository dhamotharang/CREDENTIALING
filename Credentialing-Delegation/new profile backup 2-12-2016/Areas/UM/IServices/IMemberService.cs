using PortalTemplate.Areas.UM.Models.ViewModels.Member;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IMemberService 
    {
        List<MemberSearchResultViewModel> GetAllMembers(SearchMember searchParams);
    }
}