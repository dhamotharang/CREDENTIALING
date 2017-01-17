using AHC.CD.Business.Profiles;
using AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AHC.CD.WebUI.MVC.Models;
using System.Threading.Tasks;
using AHC.CD.Business.BusinessModels.ProfileUpdates;
using AHC.CD.Entities;
using AHC.CD.Data.Repository;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Entities.MasterData.Tables;
using Newtonsoft.Json;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Controllers
{
    public class ProfileUpdatesController : Controller
    {
        private IProfileUpdateManager profileUpdateManager = null;
        IUnitOfWork uow = null;

        public ProfileUpdatesController(IUnitOfWork uow, IProfileUpdateManager profileUpdateManager)
        {
            this.profileUpdateManager = profileUpdateManager;
            this.uow = uow;
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

        private ApplicationRoleManager _roleManager;
        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        // GET: CredentialingDelegation/ProfileUpdates
        public ActionResult Index()
        {
            ViewBag.Roles = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>().GetRoles(User.Identity.GetUserId());
            return View();
        }

        [HttpGet]
        public async Task<string> GetAllUpdates()
        {
            List<ProfileUpdatesTracker> upadtedData = null;

            bool isPRO = await GetUserRole();
            

            if (isPRO)
            {
                string UserAuthId = await GetUserAuthId();
                int ProfileID = Convert.ToInt32(GetCredentialingUserId(UserAuthId));

                upadtedData = profileUpdateManager.GetUpdatesById(ProfileID);

            }
            else
            {

                upadtedData = profileUpdateManager.GetAllUpdates();

            }

            //return Json(upadtedData, JsonRequestBehavior.AllowGet);
            return JsonConvert.SerializeObject(upadtedData);
        }

        [HttpGet]
        public async Task<string> GetAllUpdatesHistory()
        {
            List<ProfileUpdatesTracker> upadtedData = null;
            bool isPRO = await GetUserRole();
           

            if (isPRO)
            {
                string UserAuthId = await GetUserAuthId();
                int ProfileID = Convert.ToInt32(GetCredentialingUserId(UserAuthId));
                upadtedData = profileUpdateManager.GetUpdatesHistoryById(ProfileID);
            }
            else
            {
                upadtedData = profileUpdateManager.GetAllUpdatesHistory();

            }

            return JsonConvert.SerializeObject(upadtedData);
        }

        [HttpPost]
        public ActionResult GetDataById(int trackerId)
        {
            var upadtedData = profileUpdateManager.GetDataById(trackerId);

            return Json(upadtedData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SetApproval(ApprovalSubmission tracker)
        {
            var status = "true";
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            await profileUpdateManager.SetApproval(tracker, user.Id);

            return Json(status, JsonRequestBehavior.AllowGet);
        }

        public int? GetCredentialingUserId(string UserAuthId)
        {
            var userRepo = uow.GetGenericRepository<CDUser>();
            var user = userRepo.Find(u => u.AuthenicateUserId == UserAuthId);

            return user.ProfileId;
        }


        public string getHospitalContactInfoById(int[] contactInfoIds)
        {
            var hospitalContactInfos = profileUpdateManager.GetHospitalContactInfoByIds(contactInfoIds);

            return JsonConvert.SerializeObject(hospitalContactInfos);
        }
        #region Private Methods

        private async Task<string> GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

            return user.Id;
        }

        private async Task<bool> GetUserRole()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            var Role = RoleManager.Roles.FirstOrDefault(r => r.Name == "PRO");

            return user.Roles.Any(r => r.RoleId == Role.Id);
        }

        #endregion
    }
}