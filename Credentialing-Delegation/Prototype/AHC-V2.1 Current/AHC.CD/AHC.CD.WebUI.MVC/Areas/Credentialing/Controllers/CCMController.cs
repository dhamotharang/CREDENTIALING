using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class CCMController : Controller
    {
        //
        // GET: /Credentialing/Index/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Credentialing/SPA/
        public ActionResult SPA()
        {
            return View();
        }
	}
}