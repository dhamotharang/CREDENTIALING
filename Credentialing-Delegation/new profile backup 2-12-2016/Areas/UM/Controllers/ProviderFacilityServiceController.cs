using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalTemplate.Areas.UM.Models.ServiceModels;
using System.Web.Hosting;
using System.Web.Script.Serialization;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;

using PortalTemplate.Areas.Portal.IServices.ProviderBridge.AddProvider;
using PortalTemplate.Areas.Portal.Services.ProviderBridge.AddProvider;
using PortalTemplate.Areas.Portal.Services.Facility;
using PortalTemplate.Areas.Portal.IServices.Facility;


namespace PortalTemplate.Areas.UM.Controllers
{
    public class ProviderFacilityServiceController : Controller
    {
        //
        // GET: /UM/ProviderService/

        public JsonResult GetProviderData(string searchTerm)
        {
            if (searchTerm == null|| searchTerm == "") { searchTerm = null; }
            IProviderService ProviderService = new ProviderService();
            List<ProviderViewModal> ProviderList= ProviderService.SearchProvider(searchTerm);
            return Json(ProviderList, JsonRequestBehavior.AllowGet);
        }
        
        //-------------------------------------------For Facility Service ----------------------------//
        public JsonResult GetFacilityData(string searchTerm)
        {
            if (searchTerm == null || searchTerm == "")
            {
                searchTerm = null;
            }
            IFacilityService FacilityService = new FacilityService();
           List<FacilityViewModel> FacilityList= FacilityService.SearchFacility(searchTerm, 15);
           return Json(FacilityList, JsonRequestBehavior.AllowGet);

        }
        public ActionResult SetPartialView(AuthorizationProviderViewModel Provider, string ProviderType)
        {
            return PartialView("~/Areas/UM/Views/Authorization/HelperPartialPages/AddProviderFields.cshtml", Provider);
        }
	}
}