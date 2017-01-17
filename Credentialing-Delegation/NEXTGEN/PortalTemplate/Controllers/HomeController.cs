using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetView(string viewName)
        {
            return PartialView("~/Views/ViewAuth/_ViewAuth.cshtml");
            //return PartialView("~/Views/" + viewName + ".cshtml");
        }

        public ActionResult GetPartial(string partialURL)
        {
             PartialViewResult part = PartialView(partialURL);
             string p = part.ToString();
             return part;
        }
    }
}