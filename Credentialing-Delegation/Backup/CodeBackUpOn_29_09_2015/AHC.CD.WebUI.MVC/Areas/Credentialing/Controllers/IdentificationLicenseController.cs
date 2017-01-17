using AHC.CD.Business;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class IdentificationLicenseController : Controller
    {
        private IProfileManager profileManager = null;

        public IdentificationLicenseController(IProfileManager profileManager)
        {
            this.profileManager = profileManager;
        }
        //
        // GET: /Credentialing/IdentificationLicense/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<string> GetAllData() {
            List<AHC.CD.Entities.MasterProfile.Profile> profiles = new List<Entities.MasterProfile.Profile>();
            profiles = await profileManager.getAllProfile();
            return JsonConvert.SerializeObject(profiles);
        }
	}
}