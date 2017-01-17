using AHC.CD.Business.Search;
using AHC.CD.WebUI.MVC.Areas.Initiation.Models;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.SearchProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class SearchProfileController : Controller
    {
         private ISearchManager searchManager = null;

         public SearchProfileController(ISearchManager searchManager)
        {
            this.searchManager = searchManager;
        }

        // GET: Profile/SearchProfile
        public ActionResult SearchProfile()
        {   
            return View();
        }

        [HttpPost]
        public JsonResult SearchProfileJson(SearchProviderViewModel search)
        {
            //string status = "true";
            List<AHC.CD.Entities.MasterProfile.Profile> searchResults = null;
            
            searchResults = searchManager.SearchProfileForViewEdit(search.NPINumber, search.FirstName, search.LastName, search.ProviderRelationship, search.IPAGroupName, search.ProviderLevel, search.ProviderType);
           
            return Json(new { searchResults }, JsonRequestBehavior.AllowGet);
        }
	}
}