using AHC.CD.Business.Credentialing.CnD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class DeCredentialingController : Controller
    {
        private IApplicationManager applicationManager = null;

        public DeCredentialingController(IApplicationManager applicationManager)
        {
            this.applicationManager = applicationManager;
        }

        //
        // GET: /CredentialingDelegation/DeCredentialing/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult SPA(int ID)
        {
            ViewBag.ProfileID = ID;
            return View();
        }

        public async Task<ActionResult> GetDeCredentialingInfoAsync(int ProfileID)
        {
            //string UserAuthId = await GetUserAuthId();
            //int CDUserId = profileManager.GetCredentialingUserId(UserAuthId);
            //bool isCCO = await GetUserRole();
            var credentialingInfo = await applicationManager.GetCredentialingInfoByProfileId(ProfileID);
            return Json(credentialingInfo, JsonRequestBehavior.AllowGet);

        }

	}
}