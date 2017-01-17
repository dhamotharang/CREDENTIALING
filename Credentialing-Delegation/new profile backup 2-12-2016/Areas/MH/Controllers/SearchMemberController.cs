using Newtonsoft.Json;
using PortalTemplate.Areas.MH.IServices;
using PortalTemplate.Areas.MH.Models.ViewModels.AdditionalModels;
using PortalTemplate.Areas.MH.Models.ViewModels.SearchMember;
using PortalTemplate.Areas.MH.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.MH.Controllers
{
    public class SearchMemberController : Controller
    {
        //
        // GET: /MH/SearchMember/

        IMemberService _memberService;
        MasterDataController masterDataController = new MasterDataController();

        //constructor of this Class
        public SearchMemberController()
        {
            _memberService = new MemberService();
        }

        public ActionResult GetMembersListByIndex(int index, string sortingType, string sortBy, SearchMemberResultViewModel SearchObject)
        {
            SearchMemberViewModel sm = new SearchMemberViewModel();
            sm.Members = _memberService.GetAllMembers();
            return PartialView("~/Areas/MH/Views/SearchMember/SearchResultGrid/_SearchMemberResultGrid.cshtml", sm);
        }


        /// <summary>This Method is Used to Get Index Page of Application.</summary>
        /// <returns>Returns Members Index View.</returns>
        public ActionResult Index()
        {
            //masterDataController.GetAllMasterData();   
            //masterDataController.GetProviderServiceData();
            SearchMemberViewModel sm = new SearchMemberViewModel();
            //sm.Members = _memberService.GetAllMembers();
            return PartialView("~/Areas/MH/Views/SearchMember/_SearchMember.cshtml",sm);
        }

        // Multiple parameters.
        /// <param name="param">Used to indicate Enrollment Status of Member.</param>
        /// <param name="searchdata">Used to specify the Search Factors.</param>
        /// <returns>Returns Members Grid PartialView of Requested Type.</returns>
        public ActionResult SearchMember(string param, SearchMemberViewModel searchdata)
        {
            List<SearchFactorViewModel> searchlist = new List<SearchFactorViewModel>();
            foreach (PropertyInfo propertyInfo in searchdata.SearchFactors.GetType().GetProperties())
            {
                SearchFactorViewModel searchobj = new SearchFactorViewModel();
                dynamic value = propertyInfo.GetValue(searchdata.SearchFactors);
                if (value != null)
                {
                    searchobj.FieldName = propertyInfo.Name;
                    searchobj.FieldValue = value;
                    searchlist.Add(searchobj);
                }
            }
            IEnumerable<SearchFactorViewModel> iSearchlist = searchlist;
            if(param == "Enrolled"){
                SearchMemberViewModel sm = new SearchMemberViewModel();
                sm.Members = _memberService.SearchMember(param, iSearchlist);
                return PartialView("~/Areas/MH/Views/SearchMember/SearchResultGrid/_EnrolledMemberResultGrid.cshtml", sm.Members);
            }
            else
            {
                SearchMemberViewModel sm = new SearchMemberViewModel();
                sm.Members = _memberService.SearchMember(param, iSearchlist);
                return PartialView("~/Areas/MH/Views/SearchMember/SearchResultGrid/_SearchMemberResultGrid.cshtml", sm);
            }
        }


        /// <summary>This Method is Used to Get All the Members.</summary>
        /// <returns>Returns Members Grid PartialView.</returns>
        public ActionResult GetAllMembers()
        {
            return PartialView("~/Areas/MH/Views/SearchMember/SearchResultGrid/_SearchMemberResultGrid.cshtml", _memberService.GetAllMembers());
        }

        /// <summary>This Method is Used to Get the View Or Edit of Specific Member.</summary>
        // Multiple parameters.
        /// <param name="subscriberId">Used to indicate subscriberId of Member.</param>
        /// <param name="type">Used to specify Requesting type(View or Edit).</param>
        /// <returns>Returns PartialView of Requested Type.</returns>
        

        //public ActionResult GetMember(string subscriberId, string client)
        //{
        //    var user = GetUserPrivileges();
        //    string view = null;
        //    if (user.IsPrivilegedUser)
        //    {
        //        if (client == "UM")
        //        {
        //           view = "~/Areas/MH/Views/MemberProfile/_MemberDetails.cshtml";
        //        }
        //        else
        //        {
        //           view = "~/Areas/MH/Views/NewMember/View/_ViewNewMember.cshtml";
        //        }
        //    }
        //    return PartialView(view);
        //}

        private dynamic GetUserPrivileges()
        {
            UserPrivilegesViewModel userPrivileges = new UserPrivilegesViewModel();
            userPrivileges.IsPrivilegedUser = true;
            userPrivileges.Role = "Admin";
            userPrivileges.UserId = "0012345";
            userPrivileges.UserType = "Registered";
            userPrivileges.Status = "Active";
            return userPrivileges;
        }

        /// <summary>This Method is Used to Get the View of Member Profile Header.</summary>
        // Multiple parameters.
        /// <param name="subscriberId">Used to indicate subscriberId of Member.</param>
        /// <param name="type">Used to specify Requesting type.</param>
        /// <param name="status">Used to specify Status of Member(Enrolled or Disenrolled or etc).</param>
        /// <returns>Returns PartialView of Requested Type.</returns>
        public ActionResult GetMemberHeader(string subscriberId, string type, string status)
        {
            return PartialView("~/Areas/MH/Views/MemberProfile/_MemberHeader.cshtml", _memberService.GetMemberHeader(subscriberId, type, status));
        }

        
     
	}
}