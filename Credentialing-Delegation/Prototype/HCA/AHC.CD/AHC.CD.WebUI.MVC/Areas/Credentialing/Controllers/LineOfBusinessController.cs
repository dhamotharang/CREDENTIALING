using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class LineOfBusinessController : Controller
    {
        //
        // GET: /Credentialing/LineOfBusiness/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ListLOB()
        {
            return View();
        }

        public ActionResult AddEdit() {

            return View();
        }
	}
}