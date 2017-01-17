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
using AHC.CD.Business.Profiles;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Business.Credentialing.CnD;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Entities.ProfileLog;
using AHC.CD.Business.ProfileLog;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class MasterProfileController : Controller
    {
        private IProfileManager profileManager = null;
        private IUserManager userManager = null;
        private IErrorLogger errorLogger = null;
        private IProfileUpdateManager profileUpdateManager = null;
        //private IProfileLogTrackerManager profileLogManager = null;

        public MasterProfileController(IProfileManager profileManager, IProfileUpdateManager profileUpdateManager, IErrorLogger errorLogger, IUserManager userManager)
        {
            this.profileUpdateManager = profileUpdateManager;
            this.profileManager = profileManager;
            this.userManager = userManager;
            this.errorLogger = errorLogger;
            //this.profileLogManager = profileLogManager;
        }

        protected ApplicationUserManager _authUserManager;
        private IApplicationManager applicationManager = null;


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

        // GET: Profile/MasterProfile
        //[Authorize(Roles="PRO")]
        [CompressFilter]
        [Authorize(Roles = "PRO")]
        public async Task<ActionResult> Index()
        {
            try
            {
                var profileId = this.userManager.GetProfileId(User.Identity.GetUserId());
                if (profileId == null)
                    return View("ProfileDoesNotExist");

                ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());

                ViewBag.ProfileId = profileId;

                ViewBag.Demographics = JsonConvert.SerializeObject(await profileManager.GetDemographicsProfileDataAsync(Convert.ToInt32(profileId)), new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });
                ViewBag.ProfileUpdates = JsonConvert.SerializeObject(profileUpdateManager.GetUpdatesForProfileById(Convert.ToInt32(profileId)));

                //ViewBag.StateLicenses = JsonConvert.SerializeObject(await profileManager.GetIdentificationAndLicensesProfileDataAsync(Convert.ToInt32(profileId)));

                return View();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<ActionResult> ChangeNotificationStatus(int dashboardNotificationID)
        {
            AHC.CD.Entities.Notification.UserDashboardNotification dashboardNotification = null;
            dashboardNotification = await userManager.MarkNotificationAsRead(dashboardNotificationID);
            var lengthOfRedirectString = dashboardNotification.RedirectURL.Split('/').Length;
            if (lengthOfRedirectString == 3)
            {
                var b = dashboardNotification.RedirectURL.Split('/');
                string action = dashboardNotification.RedirectURL.Split('/')[2];
                string controller = dashboardNotification.RedirectURL.Split('/')[1];
                //Edited for CCM Notification
                return RedirectToAction(action, controller, new { area = "" });
            }
            else if (lengthOfRedirectString == 5)
            {
                var b = dashboardNotification.RedirectURL.Split('/');
                string action = b[3] + "/" + b[4];
                string controller = b[2];
                string area = b[1];
                return RedirectToAction(action, controller, new { Area = area });
            }
            else
            {
                var c = dashboardNotification.RedirectURL.Split('/');
                string action = dashboardNotification.RedirectURL.Split('/')[3];
                string controller = dashboardNotification.RedirectURL.Split('/')[2];
                string area = dashboardNotification.RedirectURL.Split('/')[1];
                return RedirectToAction(action, controller, new { Area = area });
            }


        }


        [HttpGet]
        public string GetUpdatesById(int profileId)
        {
            var profileUpdates = profileUpdateManager.GetUpdatesByIdForAllStatus(profileId);
            return JsonConvert.SerializeObject(profileUpdates);
        }


        public JsonResult getProfileUpdateDataByIdWithStatus(int[] profileUpdateTrackerIds, string[] Status)
        {
            List<AHC.CD.Business.BusinessModels.ProfileUpdates.ProfileUpdatedNewData> profilenewUpdates = new List<AHC.CD.Business.BusinessModels.ProfileUpdates.ProfileUpdatedNewData>();

            for (int i = 0; i < profileUpdateTrackerIds.Length; i++)
            {
                List<AHC.CD.Business.BusinessModels.ProfileUpdates.ProfileUpdatedData> profUpdates = profileUpdateManager.GetDataById(profileUpdateTrackerIds[i]);
                foreach (var item1 in profUpdates)
                {
                    ProfileUpdatedNewData obj = new ProfileUpdatedNewData();
                    obj.FieldName = item1.FieldName;
                    obj.NewValue = item1.NewValue;
                    obj.OldValue = item1.OldValue;
                    obj.ApprovalStatus = Status[i];
                    profilenewUpdates.Add(obj);
                }
            }

            return Json(profilenewUpdates, JsonRequestBehavior.AllowGet);
        }


        public JsonResult getProfileUpdateDataById(int[] profileUpdateTrackerIds)
        {

            List<AHC.CD.Business.BusinessModels.ProfileUpdates.ProfileUpdatedData> profileUpdates = new List<AHC.CD.Business.BusinessModels.ProfileUpdates.ProfileUpdatedData>();
            foreach (int item in profileUpdateTrackerIds)
            {
                List<AHC.CD.Business.BusinessModels.ProfileUpdates.ProfileUpdatedData> profUpdates = profileUpdateManager.GetDataById(item);
                foreach (var item1 in profUpdates)
                {
                    profileUpdates.Add(item1);
                }
            }
            return Json(profileUpdates, JsonRequestBehavior.AllowGet);
        }


        [Authorize(Roles = "ADM,CCO,CRA,TL,CCM,MGT,HR")]
        [CompressFilter]
        public async Task<ActionResult> ProviderProfile(int id)
        {
            try
            {
                ViewBag.ProfileId = id;
                var request = HttpContext.ApplicationInstance.Request;
                var appUrl = HttpRuntime.AppDomainAppVirtualPath;
                string cdm = System.Configuration.ConfigurationManager.AppSettings["CredentialingManager"];
                if(cdm!=null)
                {
                    ViewBag.cdm = cdm;
                }
                string pm = System.Configuration.ConfigurationManager.AppSettings["ProjectManager"];
                if (pm != null)
                {
                    ViewBag.pm = pm;
                }
                string isPrimeCare = System.Configuration.ConfigurationManager.AppSettings["isPrimeCare"];
                if (isPrimeCare!=null)
                {
                    ViewBag.isPrimeCare = isPrimeCare;
                }

                if (!string.IsNullOrWhiteSpace(appUrl)) appUrl += "/";


                var baseUrl = string.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, appUrl);

                ViewBag.BaseUrl = baseUrl;
                ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());

                ViewBag.Demographics = JsonConvert.SerializeObject(await profileManager.GetDemographicsProfileDataAsync(id), new JsonSerializerSettings
                {
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });
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
            return JsonConvert.SerializeObject(await profileManager.GetDemographicsProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public async Task<string> GetIdentificationAndLicensesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetIdentificationAndLicensesProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public async Task<string> GetBoardSpecialtiesProfileDataAsync(int profileId)
        {
            var temp = JsonConvert.SerializeObject(await profileManager.GetBoardSpecialtiesProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
            return temp;
        }

        public async Task<string> GetHospitalPrivilegesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetHospitalPrivilegesProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public async Task<string> GetProfessionalLiabilitiesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetProfessionalLiabilitiesProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public async Task<string> GetProfessionalReferencesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetProfessionalReferencesProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public async Task<string> GetEducationHistoriesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetEducationHistoriesProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public async Task<string> GetWorkHistoriesProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetWorkHistoriesProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public async Task<string> GetProfessionalAffiliationsProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetProfessionalAffiliationsProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public async Task<string> GetPracticeLocationsProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetPracticeLocationsProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public async Task<string> GetDisclosureQuestionsProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetDisclosureQuestionsProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
        }

        public async Task<string> GetContractInfoProfileDataAsync(int profileId)
        {
            return JsonConvert.SerializeObject(await profileManager.GetContractInfoProfileDataAsync(profileId), new JsonSerializerSettings
            {
                DateTimeZoneHandling = DateTimeZoneHandling.Local
            });
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
            catch (Exception)
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

        public async Task<bool> CheckProfileLockUnlock(int prodileId)
        {
            bool IsLocked = true;

            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            ProfileLogTracker tracker = new ProfileLogTracker();
            tracker.ProfileId = prodileId;
            tracker.LogedByFullName = user.FullName;
            tracker.LogedByUserName = user.UserName;

            //IsLocked = profileLogManager.SetLockToProfile(tracker);

            return IsLocked;
        }
    }
}