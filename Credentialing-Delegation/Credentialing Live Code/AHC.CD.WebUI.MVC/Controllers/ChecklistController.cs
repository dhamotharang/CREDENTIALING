using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class ChecklistController : Controller
    {
        // GET: Checklist
        public ActionResult Index()
        {
            //For checklists
            return View();
        }
    }
}