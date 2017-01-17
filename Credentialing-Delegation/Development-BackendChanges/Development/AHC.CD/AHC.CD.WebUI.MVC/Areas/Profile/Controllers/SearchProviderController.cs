using AHC.CD.Business.Search;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.SearchProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class SearchProviderController : Controller
    {

        private ISearchManager searchManager = null;

        public SearchProviderController(ISearchManager searchManager)
        {
            this.searchManager = searchManager;

        }

        // GET: Profile/SearchProvider
        public ActionResult SearchProvider()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AdvancedSearchProviderJson(string search)
        {
            //ProviderSearchRequestDTO searchRequest = null;
            //string status = "true";
            List<ProviderSearchResultDTO> searchResults = null;

           //searchResults = searchManager.SearchProviderProfileForViewEdit(search.NPINumber, search.FirstName, search.LastName, search.ProviderRelationship, search.IPAGroupName, search.ProviderLevel, search.ProviderType);
            searchResults = searchManager.SearchProviderProfileForViewEditByAnyValue(search);
           
            return Json(new { searchResults }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SearchProviderJson(SearchProviderViewModel search)
        {
            //ProviderSearchRequestDTO searchRequest = null;
            //string status = "true";
            List<ProviderSearchResultDTO> searchResults = null;

            searchResults = searchManager.SearchProviderProfileForViewEdit(search.NPINumber, search.FirstName, search.LastName, search.ProviderRelationship, search.IPAGroupName, search.ProviderLevel, search.ProviderType);
            //searchResults = searchManager.SearchProviderProfileForViewEditByAnyValue(search.NPINumber);

            return Json(new { searchResults }, JsonRequestBehavior.AllowGet);
        }
    }
}