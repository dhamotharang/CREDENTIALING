using AHC.CD.Business;
using AHC.CD.Business.Notification;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using AHC.CD.Business.Users;
using Newtonsoft.Json;
using AHC.CD.WebUI.MVC.Models;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private IExpiryNotificationManager ExpiryNotification { get; set; }

        private IProfileManager profileManager = null;

        private IUserManager userManager = null;

        public DashboardController(IExpiryNotificationManager expiryNotification, IProfileManager profileManager, IUserManager userManager)
        {
            this.ExpiryNotification = expiryNotification;
            this.profileManager = profileManager;
            this.userManager = userManager;
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
        //
        // GET: /Dashboard/

        //IProvidersManager providersManager = null;

        //public DashboardController(IProvidersManager providersManager)
        //{
        //    this.providersManager = providersManager;

        //}

        public async Task<ActionResult> Index()
        {
            try
            {
                //await this.ExpiryNotification.SaveExpiryNotificationAsync();
                var a = User.Identity.GetUserId();
                var Role = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());
                ViewBag.Roles = Role;
                if (Role[0] == "PRO")
                {
                    int UserId = Convert.ToInt32(this.userManager.GetProfileId(User.Identity.GetUserId()));
                    if (profileManager.GetProfileStatus(UserId) != "Active")
                    {
                        return RedirectToAction("LogoutProfile", "Account", new { msg = "Your Profile is deactivated, Kindly contact your credentialing coordinator to re-activate your account !!!!" });
                    }
                }
                if (Role[0] != "PRO")
                {
                    int UserId = Convert.ToInt32(this.userManager.GetCDuserIdforLogin(User.Identity.GetUserId()));
                    string CdUserStatus = profileManager.GetUserStatus(UserId);
                    if (UserId != 0)
                    {
                        if (CdUserStatus == "Inactive")
                        {
                            return RedirectToAction("LogoutProfile", "Account", new { msg = "Your Profile is deactivated, Kindly contact your Administrator to re-activate your account !!!!" });
                        }
                    }
                }
                var expires = await this.ExpiryNotification.GetExpiries(User.Identity.GetUserId());
                ViewBag.expiresData = expires;
                return View();
            }
            catch (Exception)
            {
                //return RedirectToAction("LogoutProfile", "Account", new {msg="Sorry For Inconvenience !! Your Session Has Expired !!!!" });
                throw;
            }
        }

        /// <summary>
        /// This method is create for provider dashboard testing.
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetAllExpiresForAProvider(int profileID)
        {
            try
            {
                var Role = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());

                string Status = "true";

                //if (Role[0] == "CCO")
                //{
                var expires = await this.ExpiryNotification.GetAllExpiryForAProvider(profileID);
                //  ViewBag.expiresData = expires;
                //}

                return Json(new { status = Status, data = expires }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<JsonResult> GetTaskExpiryCounts(int? cdUserID)
        {
            var Result = await profileManager.GetTaskExpiryCounts(cdUserID);
            return Json(new { Result = Result }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> GetMyNotification()
        {
            string UserAuthId = await GetUserAuthId();
            int CDUserId = profileManager.GetCDUserIdFromAuthId(UserAuthId);
            var data = await profileManager.GetMyNotification(CDUserId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetInitialProviderData()
        {

            var data = profileManager.GetAllProvidersCountByProviderLevels();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult GetExpiredDataCount()
        //{

        //    var data = profileManager.GetExpiredDataCountForUser();

        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetProviderTypesGraphData(ProviderRelation? providerRelation)
        //{

        //  //  graphData = relationId!=null ? providersManager.GetAllProviderTypeGraphData(ProviderRelation.InHouse) : providersManager.GetAllProviderTypeGraphData();

        //    return Json(GraphDataTransformer.TransformProviderTypes(providersManager.GetAllProviderTypeGraphData(providerRelation)), JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult GetProviderOccupationData(string providerType, ProviderRelation? ProviderRelation,int year)
        //{
        //    return Json(GraphDataTransformer.TransformProviderTypes(providersManager.GetProviderTypeGraphDataAsync(providerType, year, ProviderRelation)), JsonRequestBehavior.AllowGet);
        //}

        public ActionResult Provider()
        {
            return View();
        }

        public ActionResult Plan()
        {
            return View();
        }

        public ActionResult TPA()
        {
            return View();
        }

        public ActionResult IPA()
        {
            return View();
        }

        #region Private Methods

        private async Task<string> GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user.Id;
        }

        private async Task<bool> GetUserRole()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            var Role = RoleManager.Roles.FirstOrDefault(r => r.Name == "CCO");

            return user.Roles.Any(r => r.RoleId == Role.Id);
        }

        #endregion
    }
}
