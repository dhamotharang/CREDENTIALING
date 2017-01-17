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
using Newtonsoft.Json;

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
        [CompressFilter]
        public async Task<ActionResult> Index()
        {
            try
            {
                var profileId = this.userManager.GetProfileId(User.Identity.GetUserId());
                if (profileId == null)
                    return View("ProfileDoesNotExist");

                ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());
                
                ViewBag.ProfileId = profileId;

                ViewBag.Demographics = JsonConvert.SerializeObject(await profileManager.GetDemographicsProfileDataAsync(Convert.ToInt32(profileId)));

                //ViewBag.StateLicenses = JsonConvert.SerializeObject(await profileManager.GetIdentificationAndLicensesProfileDataAsync(Convert.ToInt32(profileId)));

                return View();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        [Authorize(Roles = "ADM,CCO,CRA,TL,CCM,MGT,HR")]
        [CompressFilter]
        public async Task<ActionResult> ProviderProfile(int id)
        {
            try
            {
                ViewBag.ProfileId = id;
                
                ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());

                ViewBag.Demographics = JsonConvert.SerializeObject(await profileManager.GetDemographicsProfileDataAsync(id));

                //ViewBag.StateLicenses = JsonConvert.SerializeObject(await profileManager.GetIdentificationAndLicensesProfileDataAsync(id));

                return View("Index");
            }
            catch (Exception)
            {

                throw;
            }
        }

        //[AjaxAction]
        public async Task<string> Get(int profileId)
        {
            try
            {
                var data = await this.profileManager.GetByIdAsync(profileId);

                //return Json(JsonConvert.SerializeObject(data, Formatting.None, new IsoDateTimeConverter() { DateTimeFormat = "MM/dd/yyyy" }), JsonRequestBehavior.AllowGet);
                return JsonConvert.SerializeObject(data);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return null;
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
        

        #region Get Profiles as Section Wise

        public async Task<string> GetDemographicsProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetDemographicsProfileDataAsync(profileId));
        }

        public async Task<string> GetIdentificationAndLicensesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetIdentificationAndLicensesProfileDataAsync(profileId));
        }

        public async Task<string> GetBoardSpecialtiesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetBoardSpecialtiesProfileDataAsync(profileId));
        }

        public async Task<string> GetHospitalPrivilegesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetHospitalPrivilegesProfileDataAsync(profileId));
        }

        public async Task<string> GetProfessionalLiabilitiesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetProfessionalLiabilitiesProfileDataAsync(profileId));
        }

        public async Task<string> GetProfessionalReferencesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetProfessionalReferencesProfileDataAsync(profileId));
        }

        public async Task<string> GetEducationHistoriesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetEducationHistoriesProfileDataAsync(profileId));
        }

        public async Task<string> GetWorkHistoriesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetWorkHistoriesProfileDataAsync(profileId));
        }

        public async Task<string> GetProfessionalAffiliationsProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetProfessionalAffiliationsProfileDataAsync(profileId));
        }

        public async Task<string> GetPracticeLocationsProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetPracticeLocationsProfileDataAsync(profileId));
        }

        public async Task<string> GetDisclosureQuestionsProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetDisclosureQuestionsProfileDataAsync(profileId));
        }

        public async Task<string> GetContractInfoProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetContractInfoProfileDataAsync(profileId));
        }

        #endregion

        #region Deactivate Profile
        
        [HttpPost]
        public async Task<JsonResult> DeactivateProfile(int profileID)
        {
            var status = false;
            try
            {
                await profileManager.DeactivateProfile(profileID);
                status = true;
            }
            catch(Exception)
            {
                status = false;
                throw;
            }
            return Json(status, JsonRequestBehavior.AllowGet); ;
        }

        #endregion

        #region Reactivate Profile

        [HttpPost]
        public async Task<JsonResult> ReactivateProfile(int profileID)
        {
            var status = false;
            try
            {
                await profileManager.ReactivateProfile(profileID);
                status = true;
            }
            catch (Exception)
            {
                status = false;
                throw;
            }
            return Json(status, JsonRequestBehavior.AllowGet); ;
        }

        #endregion

    }
}