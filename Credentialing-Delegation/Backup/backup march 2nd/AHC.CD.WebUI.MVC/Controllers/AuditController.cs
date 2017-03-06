using AHC.CD.Business.AuditLogManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class AuditController : Controller
    {
        //
        // GET: /Audit/

        private readonly IAuditManager iAuditManager = null;
        public AuditController(IAuditManager iAuditManager)
        {
            this.iAuditManager = iAuditManager;
        }
        public ActionResult Index()
        {
            ViewBag.AuditLog = iAuditManager.GetAllAuditLogAsync(); 
            return View();
        }
	}
}