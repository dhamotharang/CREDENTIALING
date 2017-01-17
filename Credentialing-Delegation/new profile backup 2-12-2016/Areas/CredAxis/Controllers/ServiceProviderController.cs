using Newtonsoft.Json;
using PortalTemplate.Areas.CredAxis.Models.ProviderviewModel;
using PortalTemplate.Areas.CredAxis.Services;
using PortalTemplate.Areas.CredAxis.Services.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.CredAxis.Controllers
{
    public class ServiceProviderController : Controller
    {
        //IProviderService ProviderService = null;
        //public ProviderServiceController()
        //{
        //    ProviderService = new ProviderService();
        //}

        //[HttpPost]
        //public PartialViewResult SearchProvider(ProviderSearch searchParams)
        //{


        //    return PartialView("~/Areas/CredAxis/Views/SearchProvider/_ProviderSearchResult.cshtml", ProviderService.GetAllProviders(searchParams));
        //}
        //public ActionResult GetFilteredSearchProvider(int index, string sortingType, string sortBy, ProviderSearch SearchObject)
        //{

        //    return PartialView("~/Areas/CredAxis/Views/SearchProvider/_ProviderSearchResult.cshtml", ProviderService.GetAllProviders(SearchObject));
        //}

        [HttpPost]
        public PartialViewResult SearchProvider(ProviderSearch searchParams)
        {
            IProviderService ProviderService = new ProviderService();
            ViewBag.SearchResultData = ProviderService.GetAllProviders(searchParams);
            return PartialView("~/Areas/CredAxis/Views/SearchProvider/_ProviderSearchResult.cshtml", ViewBag);
        }
    }
}