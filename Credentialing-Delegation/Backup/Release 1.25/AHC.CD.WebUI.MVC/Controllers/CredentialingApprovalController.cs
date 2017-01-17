using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class CredentialingApprovalController : Controller
    {
        // GET: CredentialingApproval
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProvidersList()
        {
            return View();
        }
    }
}