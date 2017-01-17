using AHC.CD.Business.PackageGeneration;
using AHC.CD.ErrorLogging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class AuditingController : Controller
    {

        private IPackageGeneratorManager packageManager = null;
        private IErrorLogger errorLogger = null;

        public AuditingController(IPackageGeneratorManager packageManager, IErrorLogger errorLogger)
        {
            this.packageManager = packageManager;
            this.errorLogger = errorLogger;            
        }

        //
        // GET: /Credentialing/Auditing/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetAllPackageGenerationTracker()
        {
            var trackers = packageManager.GetAllPackageGenerationTracker();

            return JsonConvert.SerializeObject(trackers);
        }
	}
}