using AHC.CD.Business.Search;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.SearchProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class SearchProviderController : Controller
    {
        private ISearchManager searchManager = null;
        public SearchProviderController(ISearchManager searchManager)
        {
            this.searchManager = searchManager;
        }
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        [Authorize(Roles = "ADM,CCO,CRA")]
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

        [HttpPost]
        public async Task<JsonResult> SearchUser(SearchProviderViewModel search)
        {
            List<SearchUserforGroupMailDTO> searchResults = null;
            searchResults = await searchManager.SearchUserforGroupMail(search.FirstName, search.IPAGroupName, search.ProviderLevel);
            return Json(new { searchResults }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> SearchUserForGroupMail()
        {
            List<SearchUserforGroupMailDTO> searchResults = null;
            List<string> UserNames = new List<string>();
            var res = UserManager.Users.ToList();
            res.ForEach(x => { var role = UserManager.GetRoles(x.Id); if (!role.Contains("PRO")) UserNames.Add(x.FullName); });
            searchResults = await searchManager.GetAllUsersForGroupMailAsync();
            searchResults.ForEach(x => { res.ForEach(y => { x.FullName = y.Id == x.FullName ? y.FullName : x.FullName; }); });
            return Json(new { searchResults }, JsonRequestBehavior.AllowGet);
        }
    }
}