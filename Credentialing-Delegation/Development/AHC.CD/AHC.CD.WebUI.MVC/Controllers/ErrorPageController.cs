using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult Index()
        {
            return View("Error");
        }
        public ActionResult Error404()
        {
            return View("Error404");
        }
        public ActionResult Error500()
        {
            return View("Error500");
        }
    }
}