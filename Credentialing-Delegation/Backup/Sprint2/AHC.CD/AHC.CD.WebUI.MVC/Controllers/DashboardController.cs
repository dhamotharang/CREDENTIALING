using AHC.CD.Business;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.WebUI.MVC.Models.Utility.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    
    
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        IProvidersManager providersManager = null;

        public DashboardController(IProvidersManager providersManager)
        {
            this.providersManager = providersManager;
            
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetProviderTypesGraphData(ProviderRelation? providerRelation)
        {

          //  graphData = relationId!=null ? providersManager.GetAllProviderTypeGraphData(ProviderRelation.InHouse) : providersManager.GetAllProviderTypeGraphData();

            return Json(GraphDataTransformer.TransformProviderTypes(providersManager.GetAllProviderTypeGraphData(providerRelation)), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetProviderOccupationData(string providerType, ProviderRelation? ProviderRelation,int year)
        {
            return Json(GraphDataTransformer.TransformProviderTypes(providersManager.GetProviderTypeGraphDataAsync(providerType, year, ProviderRelation)), JsonRequestBehavior.AllowGet);
        }

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
