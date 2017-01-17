using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class CnDController : Controller
    {
        // GET: CredentialingDelegation/SPA
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult PrintCheckList()
        {
            return PartialView();
        }
        //---- Credentialing Action Appointment -----------
        public ActionResult CredentialingAppointment()
        {
            return View();
        }
    }
}