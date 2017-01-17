using AHC.CD.Business;
using AHC.CD.Business.Users;
using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.DisclosureQuestions;
using AHC.CD.Entities.MasterProfile.EducationHistory;
using AHC.CD.Entities.MasterProfile.HospitalPrivilege;
using AHC.CD.Entities.MasterProfile.IdentificationAndLicenses;
using AHC.CD.Entities.MasterProfile.ProfessionalAffiliation;
using AHC.CD.Entities.MasterProfile.ProfessionalReference;
using AHC.CD.Entities.MasterProfile.WorkHistory;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.ErrorLogging;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.CustomHelpers;
using AHC.CD.WebUI.MVC.Areas.Profile.Attributes;
using AHC.CD.Entities.MasterData.Account;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class MasterProfileController : Controller
    {
        private IProfileManager profileManager = null;
        private IUserManager userManager = null;
        private IErrorLogger errorLogger = null;

        public MasterProfileController(IProfileManager profileManager, IErrorLogger errorLogger, IUserManager userManager)
	    {
            this.profileManager = profileManager;
            this.userManager = userManager;
            this.errorLogger = errorLogger;
	    }
        
        // GET: Profile/MasterProfile
        //[Authorize(Roles="PRO")]
        public async Task<ActionResult> Index()
        {
            try
            {
                var profileId = this.userManager.GetProfileId(User.Identity.GetUserId());
                if (profileId == null)
                    return View("ProfileDoesNotExist");

                ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());
                
                ViewBag.ProfileId = profileId;
                return View();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        //[Authorize(Roles = "ADM,CCO,CRA,TL,PRO,CCM,MGT,HR")]
        public async Task<ActionResult> ProviderProfile(int id)
        {
            try
            {
                ViewBag.ProfileId = id;
                ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());
                return View("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }

        //[AjaxAction]
        public async Task<JsonResult> Get(int profileId)
        {
            try
            {
                var data = await this.profileManager.GetByIdAsync(profileId);
                
                //return Json(JsonConvert.SerializeObject(data, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "MM/dd/yyyy" }), JsonRequestBehavior.AllowGet);
                return Json(data, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);                
            }

            return Json(null, JsonRequestBehavior.AllowGet);
        }

        //[Authorize(Roles = "PRO")]        
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "PRO")]        
        public async Task<ActionResult> Create(Entities.MasterProfile.Profile profile)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    Organization organization1 = new Organization();
                    organization1.OrganizationID = 1;


                    await this.userManager.CreateProfile(User.Identity.GetUserId(), profile);
                    return RedirectToAction("Index");
                }
                else
                    return View(profile);
            }
            catch
            {
                return View();
            }
        }

        //[Authorize(Roles = "ADM,CCO,CRA,TL,PRO,CCM,MGT,HR")]  
        public async Task<ActionResult> Providers()
        {
            return View();
        }

        //[Authorize(Roles = "ADM,CCO,CRA,TL,PRO,CCM,MGT,HR")]
        public async Task<JsonResult> GetAllProfileAsync()
        {
            try
            {
                var roles = await HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRolesAsync(User.Identity.GetUserId());
                IEnumerable<object> profiles = null;

                if (!roles.Contains("TL"))
                    profiles = await this.profileManager.GetAllProfileForOperationAsync();
                else
                    profiles = await this.userManager.GetProfileForTeamLeadOperation(User.Identity.GetUserId(), "PRO");

                return Json(profiles, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {

                throw;
            }
        }   
    }
}