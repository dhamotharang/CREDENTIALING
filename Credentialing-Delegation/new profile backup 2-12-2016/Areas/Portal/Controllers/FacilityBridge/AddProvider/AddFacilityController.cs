using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Portal.Controllers.ProviderBridge
{
    public class AddFacilityController : Controller
    {
        //
        // GET: /Portal/AddProvider/
        public ActionResult Index()
        {
            
            return PartialView("~/Areas/Portal/Views/FacilityBridge/_FacilityBody.cshtml");
        }
	}
}