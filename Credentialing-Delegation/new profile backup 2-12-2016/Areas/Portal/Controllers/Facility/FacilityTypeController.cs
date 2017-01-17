using PortalTemplate.Areas.Portal.Models.Facility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.Facility
{
    public class FacilityTypeController : Controller
    {
        //
        // GET: /Portal/FacilityType/
        public ActionResult Index()
        {
            ViewBag.Title = "Facility Type";

            return PartialView("~/Areas/Portal/Views/FacilityType/Index.cshtml");

        }

        public JsonResult AddEditProviderType(FacilityTypeViewModel facilitytype)
        {

            if (ModelState.IsValid)
            {
              //  var AddEditProviderType = _providerType.AddEditProviderType(facilitytype);

               // return Json(new { Result = AddEditProviderType, status = "done" });
            }


            return Json(new { status = "false" });
        }
        
	}
}