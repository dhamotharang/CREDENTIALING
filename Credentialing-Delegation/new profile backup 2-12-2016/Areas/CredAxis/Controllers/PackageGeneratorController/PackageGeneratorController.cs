using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers.PackageGeneratorController
{
    public class PackageGeneratorController : Controller
    {
        //
        // GET: /CredAxis/PackegeGenerator/

        // Added By Manideep Innamuri
        // Controller for Package Generator
        public ActionResult Index()
        {
            return PartialView("~/Areas/CredAxis/Views/PackageGenerator/_PackageGenerator.cshtml");
        }
	}
}