using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Controllers
{
    public class AddProviderController : Controller
    {
        //
        // GET: /SearchProvider/
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult GetProviderProfile()
        {
            return PartialView("~/Views/AddProvider/_ProviderProfile.cshtml");
        }
	}
}