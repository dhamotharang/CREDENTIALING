using PortalTemplate.Areas.MH.IServices;
using PortalTemplate.Areas.MH.Models.ViewModels.AdditionalModels;
using PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile;
using PortalTemplate.Areas.MH.Models.ViewModels.ProfileManagement;
using PortalTemplate.Areas.MH.Models.ViewModels.SearchMember;
using PortalTemplate.Areas.MH.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.MH.Controllers
{
    public class MemberProfileController : Controller
    {
        //
        // GET: /MH/MemberProfile/

        IMemberService _memberService;

        public MemberProfileController()
        {
            _memberService = new MemberService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetPartial(string partialURL, GeneralInformationViewModel generalInfo)
        {
            return PartialView(partialURL, generalInfo);
        }
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
        /// <summary>This Method is Used to Get the View of Member Demographics Tab Information.</summary>
        /// <returns>Returns PartialView of Demographics Tab with Data Binded.</returns>


        /// <summary>This Method is Used to Get the View of Memberships Tab Information.</summary>
        /// <returns>Returns PartialView of Memberships Tab with Data Binded.</returns>
        public ActionResult GetDemographicsDetails(string id)
        {
            return PartialView(GetPartialFile("_DemographicsInformation.cshtml"), _memberService.GetDemographicsDetails(id));
        }

        public ActionResult GetAllMemberships()
        {
            return PartialView(GetPartialFile("_MembershipsInformation.cshtml"), _memberService.GetAllMemberships());
        }

        public ActionResult GetMembershipDetailsById(string MemberId)
        {
            return PartialView("~/Areas/MH/Views/MemberProfile/InnerTabs/_MembershipDetails.cshtml");
        }

        public ActionResult GetContactDetails()
        {
            return PartialView(GetPartialFile("_ContactInformation.cshtml"));
        }

        public ActionResult GetEmploymentDetails()
        {
            return PartialView(GetPartialFile("_EmploymentInformation.cshtml"));
        }

        public ActionResult GetPCPDetails()
        {
            return PartialView(GetPartialFile("_PCPInformation.cshtml"));
        }

        public ActionResult GetBillingProviderDetails()
        {
            return PartialView(GetPartialFile("_BillingProviderInformation.cshtml"));
        }

        public ActionResult GetIPADetails()
        {
            return PartialView(GetPartialFile("_IPAInformation.cshtml"));
        }

        public ActionResult GetAdditionalDetails()
        {
            return PartialView(GetPartialFile("_AdditionalInformation.cshtml"));
        }

        public ActionResult GetAttchementsDetails()
        {
            return PartialView(GetPartialFile("_DocumentsInformation.cshtml"));
        }

        private string GetPartialFile(string SourceFileName)
        {
            return "~/Areas/MH/Views/MemberProfile/ProfileTabs/" + SourceFileName;
        }

        /// <summary>This Method is Used to Get the Updated Member Deatils and Send to Business Layer.</summary>
        public ActionResult UpdateDemographics(GeneralInformationViewModel   generalInfo)
        {
            return PartialView("~/Areas/MH/Views/MemberProfile/ProfileTabs/Demographics/View/_ViewGeneralInformation.cshtml", generalInfo);
        }

        public ActionResult GetMemberById(string subscriberId, string client, string type)
        {
            var user = GetUserPrivileges();
            string view = null;
            if (user.IsPrivilegedUser)
            {
                if (client == "UM")
                {
                    view = "~/Areas/MH/Views/MemberProfile/_MemberDetails.cshtml";
                }
                else if(type=="view")
                {
                    view = "~/Areas/MH/Views/NewMember/View/_ViewNewMember.cshtml";
                }
                else
                {
                    view = "~/Areas/MH/Views/NewMember/Edit/_EditNewMember.cshtml";
                }
            }
            return PartialView(view);
        }
        
	}
}