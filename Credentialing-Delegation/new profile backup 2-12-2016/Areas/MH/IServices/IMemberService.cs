using PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile;
using PortalTemplate.Areas.MH.Models.ViewModels.SearchMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using PortalTemplate.Areas.MH.Common;
using PortalTemplate.Areas.MH.Models.ViewModels.ProfileManagement;

namespace PortalTemplate.Areas.MH.IServices
{
    public interface IMemberService
    {
        List<SearchMemberResultViewModel> GetAllMembers();

        List<SearchMemberResultViewModel> SearchMember(string param, IEnumerable<SearchFactorViewModel> searchdata);

        MemberProfileHeaderViewModel GetMemberHeader(string subscriberId, string type, string status);

        GeneralInformationViewModel GetDemographicsDetails(string umId);

        List<MembershipListViewModel> GetAllMemberships();
    }
}