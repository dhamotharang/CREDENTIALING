using AHC.CD.Business;
using AHC.CD.Entities.ProviderInfo;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class MasterProfileController : Controller
    {
        private IProfileManager profileManager = null;

        public MasterProfileController(IProfileManager profileManager)
	    {
            this.profileManager = profileManager;
	    }
        
        // GET: Profile/MasterProfile
        public ActionResult Index(int? profileId = 1)
        {
            ViewBag.ProfileId = profileId;
            return View();
        }

        public async Task<JsonResult> Get(int profileId)
        {
            var data = await profileManager.GetProviderByIdAsync(profileId);
            //return Json(JsonConvert.SerializeObject(data, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "MM/dd/yyyy" }), JsonRequestBehavior.AllowGet);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
            
    }
}