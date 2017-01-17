using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class PlanFormController : Controller
    {
        // GET: PlanForm
        public ActionResult Index()
        {
            return View();
        }

        // GET: Dynamic Plan Form
        [ChildActionOnly]
        public ActionResult GetPlanForm(string form)
        {
            return PartialView(form);
        }
    }
}