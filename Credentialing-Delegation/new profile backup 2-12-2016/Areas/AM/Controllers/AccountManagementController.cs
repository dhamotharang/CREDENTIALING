using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.AM.Controllers
{
    public class AccountManagementController : Controller
    {
        //
        // GET: /AM/AccountManagement/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Accounts()
        {
            return View();
        }
        public ActionResult Users()
        {
            return View();
        }
        public ActionResult BusinessProcesses()
        {
            return View();
        }
	}
}