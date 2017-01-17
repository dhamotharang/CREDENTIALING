using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.Facility
{
    public class FacilityController : Controller
    {
        //
        // GET: /Portal/Facility/
        public ActionResult Index()
        {
         //   FacilityData = FacilityService.GetFacility();

            return PartialView("~/Areas/Portal/Views/Facility/Index.cshtml");
        }

        public ActionResult GetFacilityList()
        {
            //   FacilityData = FacilityService.GetFacility();

            return PartialView("~/Areas/Portal/Views/Facility/_FacilityBody.cshtml");
        }

        public ActionResult AddEditField()
        {
            using (StreamReader r = new StreamReader(Server.MapPath("~/Areas/Portal/Resources/Facility/LanguageList.js")))
            {
                string json = r.ReadToEnd();
                var items = JsonConvert.DeserializeObject(json);
                ViewBag.LanguageData = items;

            }
            //   FacilityData = FacilityService.GetFacility();

            return PartialView("~/Areas/Portal/Views/Facility/_AddEditFacility.cshtml");
        }

	}
}