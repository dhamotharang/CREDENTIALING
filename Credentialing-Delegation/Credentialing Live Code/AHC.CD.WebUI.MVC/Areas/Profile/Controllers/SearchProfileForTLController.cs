using AHC.CD.Business.Search;
using AHC.CD.Business.Users;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.SearchProvider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class SearchProfileForTLController : Controller
    {
        private ISearchProfileForTLManager searchManager = null;


        public SearchProfileForTLController(ISearchProfileForTLManager searchManager)
        {
            this.searchManager = searchManager;
        }

        protected ApplicationUserManager _authUserManager;
        protected ApplicationUserManager AuthUserManager
        {
            get
            {
                return _authUserManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _authUserManager = value;
            }
        }

        // GET: Profile/SearchProfileForTL
        [Authorize(Roles = "TL")]
        public ActionResult Index()
        {

            return View();
        }


        [HttpPost]
        public async Task<JsonResult> SearchTLProviders(SearchProviderViewModel search)
        {

            List<ProviderSearchResultDTO> searchResults = null;

            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            string userId = user.Id;

            searchResults = searchManager.SearchProviderProfileForViewEdit(search.NPINumber, search.FirstName, search.LastName, search.ProviderRelationship, search.IPAGroupName, search.ProviderLevel, search.ProviderType, userId);

            return Json(new { searchResults }, JsonRequestBehavior.AllowGet);
        }

    }
}