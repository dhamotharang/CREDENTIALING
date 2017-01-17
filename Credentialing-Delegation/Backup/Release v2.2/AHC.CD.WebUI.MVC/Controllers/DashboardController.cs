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

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private IExpiryNotificationManager ExpiryNotification { get; set; }

        private IProfileManager profileManager = null;

        private IUserManager userManager=null;

        public DashboardController(IExpiryNotificationManager expiryNotification, IProfileManager profileManager, IUserManager userManager)
	    {
            this.ExpiryNotification = expiryNotification;
            this.profileManager = profileManager;
            this.userManager = userManager;
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

                var Role = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());
                ViewBag.Roles=Role;
                
                if(Role[0]=="PRO")
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
    }
}
