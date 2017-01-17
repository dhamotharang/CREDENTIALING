using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers
{
    public class CredAxisController : Controller
    {
        //
        // GET: /CredAxis/CredAxis/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ReturnPartial(string path)
        {
            return PartialView(path);
        }
        public ActionResult GetNoDataAvailablePartial(string SectionName, string RedirectUrl, string SectionId)
        {
            ViewBag.SectionName = SectionName;
            ViewBag.RedirectUrl = RedirectUrl;
            ViewBag.SectionId = SectionId;
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/_NodataAvailable.cshtml");
        }

	}
}