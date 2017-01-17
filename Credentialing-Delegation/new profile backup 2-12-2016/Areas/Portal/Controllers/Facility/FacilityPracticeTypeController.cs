using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.Facility
{
    public class FacilityPracticeTypeController : Controller
    {
        //
        // GET: /Portal/FacilityPracticeType/
        public ActionResult Index()
        {
            ViewBag.Title = "Facility Practice Type";
            return PartialView("~/Areas/Portal/Views/FacilityPracticeType/Index.cshtml");

        }
	}
}