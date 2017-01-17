using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.Facility
{
    public class FacilityAccessibilityQuestionController : Controller
    {
        //
        // GET: /Portal/FacilityAccessibilityQuestion/
        public ActionResult Index()
        {
            ViewBag.Title = "Facility Accessibility Question";
            return PartialView("~/Areas/Portal/Views/FacilityAccessibilityQuestion/Index.cshtml");

        }
	}
}