using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.Facility
{
    public class FacilityLanguageController : Controller
    {
        //
        // GET: /Portal/FacilityLanguage/
        public ActionResult Index()
        {

            return PartialView("~/Areas/Portal/Views/FacilityLanguage/Index.cshtml");

        }

        public ActionResult SelectedLanguage()
        {

            return PartialView("~/Areas/Portal/Views/FacilityLanguage/Index.cshtml");

        }

	}
}