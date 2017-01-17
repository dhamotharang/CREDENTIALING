using AHC.CD.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class HospitalPrivilegeController : Controller
    {
        private IProfileManager profileManager = null;

        public HospitalPrivilegeController(IProfileManager profileManager)
	    {
            this.profileManager = profileManager;
	    }
        
        // GET: Profile/HospitalPrivilege
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AddHospitalPrivilegeAsync(int profileId, AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeInformation hospitalPriviledge)
        {
            string returnMessage = "";

            if (ModelState.IsValid)
            {
                var result = await profileManager.AddHospitalPrivilegeAsync(profileId, hospitalPriviledge);
                returnMessage = result.ToString();
            }
            else
            {
                returnMessage = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }

            return Json(returnMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateHospitalPrivilegeAsync(int profileId, AHC.CD.Entities.MasterProfile.HospitalPrivilege.HospitalPrivilegeInformation hospitalPriviledge)
        {
            string returnMessage = "";

            if (ModelState.IsValid)
            {
                await profileManager.UpdateHospitalPrivilegeAsync(profileId, hospitalPriviledge);
                returnMessage = "true";
            }
            else
            {
                returnMessage = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }

            return Json(returnMessage, JsonRequestBehavior.AllowGet);
        }
    }
}