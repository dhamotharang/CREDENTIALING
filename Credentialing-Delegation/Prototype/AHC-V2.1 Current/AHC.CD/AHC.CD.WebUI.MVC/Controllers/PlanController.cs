using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class PlanController : Controller
    {
        // GET: Plan
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult PlanTypeIndex()
        {
            return View();
        }

        public ActionResult UltimateHealthPlansFrom()
        {
            return View();
        }
        public ActionResult NewPlan()
        {
            return View();
        }
        public ActionResult GetPartialView(string viewName)
        {
            return PartialView(viewName);
        }

        public ActionResult ViewUltimatePlans()
        {
            return View();
        }

        public ActionResult AHCPlansFrom()
        {
            return View();
        }

        public ActionResult ProfessionalHistoricalDataQuestionnaire()   
        {
            return View();
        }
    }
}