using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class QuickUpdateController : Controller
    {
        // GET: Profile/QuickUpdate
        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult CallSubSection(string ViewName)
        {
            return PartialView("~/Areas/Profile/Views/Demographic/"+ViewName+".cshtml");
        }
    }
}