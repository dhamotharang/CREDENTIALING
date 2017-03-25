using AHC.CD.Business.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.MasterData.Controllers
{
    public class OrganizationController : Controller
    {
        // GET: MasterData/Organization

        private IOrganizationManager OrganizationManager { get; set; }

        public OrganizationController(IOrganizationManager organizationManager)
        {
            this.OrganizationManager = organizationManager;
        }

        public ActionResult Index()
        {
            return View();
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetGroups(int organizationId = 1)
        {
            return Json(await OrganizationManager.GetGroupsAsync(organizationId), JsonRequestBehavior.AllowGet);
        }
    }
}