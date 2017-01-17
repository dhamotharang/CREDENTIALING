using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.Facility
{
    public class FacilityServiceQuestionController : Controller
    {
        //
        // GET: /Portal/FacilityServiceQuestion/
        public ActionResult Index()
        {
            ViewBag.Title = "Facility Service Question";

            return PartialView("~/Areas/Portal/Views/FacilityServiceQuestion/Index.cshtml");

        }

	}
}