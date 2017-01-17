using AHC.CD.Business.Profiles;
using AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;
using System.Threading.Tasks;
using AHC.CD.Business.BusinessModels.ProfileUpdates;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class ProfileUpdatesController : Controller
    {
        private IProfileUpdateManager profileUpdateManager = null;

        public ProfileUpdatesController(IProfileUpdateManager profileUpdateManager)
        {
            
            this.profileUpdateManager = profileUpdateManager;
        }

        protected ApplicationUserManager _authUserManager;
        protected ApplicationUserManager AuthUserManager
        {
            get
            {
                return _authUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _authUserManager = value;
            }
        }

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: CredentialingDelegation/ProfileUpdates
        public ActionResult Index()
        {

            return View();
        }

        [HttpGet]
        public ActionResult GetAllUpdates()
        {
            var upadtedData = profileUpdateManager.GetAllUpdates();

            return Json(upadtedData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetDataById(int trackerId)
        {
            var upadtedData = profileUpdateManager.GetDataById(trackerId);
           
            return Json(upadtedData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SetApproval(ApprovalSubmission tracker)
        {
            var status = "true";
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            await profileUpdateManager.SetApproval(tracker, user.Id);

            return Json(status, JsonRequestBehavior.AllowGet);
        }
    }
}