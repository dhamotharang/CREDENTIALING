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
        [Authorize(Roles = "ADM,CCO")]
        // GET: Profile/SearchProvider
        public ActionResult SearchProvider()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SearchProviderJson(SearchProviderViewModel search)
        {
            //ProviderSearchRequestDTO searchRequest = null;
            //string status = "true";
            List<ProviderSearchResultDTO> searchResults = null;

            searchResults = searchManager.SearchProviderProfileForViewEdit(search.NPINumber, search.FirstName, search.LastName, search.ProviderRelationship, search.IPAGroupName, search.ProviderLevel, search.ProviderType);
           
            return Json(new { searchResults }, JsonRequestBehavior.AllowGet);
        }
    }
}