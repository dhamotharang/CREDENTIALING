using AHC.CD.Business.Profiles;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.WebUI.MVC.Models;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfileDelegationController : Controller
    {
        private IProfileDelegationManager delegationManager=null;
        private IErrorLogger ErrorLogger { get; set; }

        public ProfileDelegationController(IProfileDelegationManager delegationManager,IErrorLogger errorLogger)
        {
            this.delegationManager = delegationManager;
            this.ErrorLogger = errorLogger;
            
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

        public async Task<JsonResult> GetAllTeamLeadsAsync()
        {
            var data = await delegationManager.GetAllTeamLeadsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: Profile/ProfileDelegation
        public ActionResult Index(int id)
        {
            ViewBag.ProfileId = id;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> AssignProfile(int profileId, int profileUserId)
        {
            string status = "true";
           

            try
            {
                var currentUser = HttpContext.User.Identity.Name;
                var appUser = new ApplicationUser() { UserName = currentUser };
                var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
                string userId = user.Id;

                await delegationManager.AssignProfile(profileId, profileUserId, userId);

                
            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.TL_ASSIGN_EXCEPTION;
            }

            return Json(new { status = status}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UserRoleMapping(int profileId)
        {
            string status = "true";
            try
            {
                var currentUser = HttpContext.User.Identity.Name;
                var appUser = new ApplicationUser() { UserName = currentUser };
                var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
                string userId = user.Id;

                await delegationManager.AssignProfile(profileId, 1, userId);


            }
            catch (DatabaseValidationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.ValidationError;
            }
            catch (ApplicationException ex)
            {
                ErrorLogger.LogError(ex);
                status = ex.Message;
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                status = ExceptionMessage.TL_ASSIGN_EXCEPTION;
            }

            return Json(new { status = status }, JsonRequestBehavior.AllowGet);
        }
    }
}