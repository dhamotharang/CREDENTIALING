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


namespace AHC.CD.WebUI.MVC.Areas.MobileSite.Controllers
{
    public class CCODashboardMobileController : Controller
    {
        private IExpiryNotificationManager ExpiryNotification { get; set; }

        private IProfileManager profileManager = null;

        private IUserManager userManager=null;

        public CCODashboardMobileController(IExpiryNotificationManager expiryNotification, IProfileManager profileManager, IUserManager userManager)
	    {
            this.ExpiryNotification = expiryNotification;
            this.profileManager = profileManager;
            this.userManager = userManager;
	    }
        //
        // GET: /MobileSite/ProviderDashboard/
        public async Task<ActionResult> CCODashboardMobile()
        {
            try
            {
                //await this.ExpiryNotification.SaveExpiryNotificationAsync();

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
	}
}