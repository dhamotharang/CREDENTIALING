using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class InitiationController : Controller
    {
        //
        // GET: /Credentialing/Initiation/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CredentialingList()
        {
            return View();
        }
	}
}