using AHC.CD.Business.Profiles;
using AHC.CD.ErrorLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfileRoleMappingController : Controller
    {
        private IProfileDelegationManager delegationManager=null;
        private IErrorLogger ErrorLogger { get; set; }

        public ProfileRoleMappingController(IProfileDelegationManager delegationManager, IErrorLogger errorLogger)
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

        //
        // GET: /Profile/ProfileRoleMapping/
        public ActionResult Index(int id)
        {
            ViewBag.ProfileId = id;
            ViewBag.ProfileDisplayData = JsonConvert.SerializeObject(delegationManager.GetProfileData(Convert.ToInt32(id)));
            ViewBag.ExistingRoles = JsonConvert.SerializeObject(delegationManager.GetRolesForUser(Convert.ToInt32(id)));
            return View();
        }

        public async Task<JsonResult> GetAllRolesAsync()
        {
            var data = await delegationManager.GetAllRoles();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> AddRole(int profileID, int roleID)
        {
            string status = "true";
            AHC.CD.Entities.CDUserRole userRole = null;
            try
            {
                var currentUser = HttpContext.User.Identity.Name;
                var appUser = new ApplicationUser() { UserName = currentUser };
                var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
                string userId = user.Id;
                userRole = delegationManager.AddRole(profileID, roleID);
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
                status = ExceptionMessage.UNABLE_TO_ADD_ROLE_PROFILE;
            }

            return Json(new { status = status, userRole = userRole }, JsonRequestBehavior.AllowGet);
        }
	}
}