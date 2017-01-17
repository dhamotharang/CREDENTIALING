using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class InsuranceCompanyController : Controller
    {
        // GET: InsuranceCompany
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyPlan()
        {
            return View();
        }

        public ActionResult AddEdit()
        {
            return View();
        }
     
    }
}