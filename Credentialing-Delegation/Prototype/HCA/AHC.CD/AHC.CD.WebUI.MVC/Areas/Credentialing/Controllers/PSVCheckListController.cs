using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class PSVCheckListController : Controller
    {
        //
        // GET: /Credentialing/PSVCheckList/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Credentialing/PSVCheckList/
        public ActionResult AddNew()
        {
            return View();
        }

        //
        // GET: /Credentialing/PSVCheckList/
        public ActionResult PSVChecklistPlanMapping()
        {
            return View();
        }
	}
}