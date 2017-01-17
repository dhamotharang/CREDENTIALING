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

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private IExpiryNotificationManager ExpiryNotification { get; set; }

        public DashboardController(IExpiryNotificationManager expiryNotification)
	    {
            this.ExpiryNotification = expiryNotification;
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
                //var expires = await this.ExpiryNotification.GetExpiries(User.Identity.GetUserId());
                await this.ExpiryNotification.SaveExpiryNotificationAsync();

                ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());

                //ViewBag.expiresData = expires;

                return View();
            }
            catch (Exception)
            {
                
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
