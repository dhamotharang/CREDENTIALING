using AHC.CD.Business;
using AHC.CD.Business.Notification;
using AHC.CD.Business.Profiles;
using AHC.CD.ErrorLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using AHC.CD.Business.CustomFieldGeneration;
using AHC.CD.Entities.CustomField;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.CustomField;
using AHC.CD.Entities.CustomField.CustomFieldTransaction;
using AHC.CD.WebUI.MVC.Models;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class CustomFieldGenerationController : Controller
    {
        private IProfileManager profileManager = null;
        private IErrorLogger errorLogger = null;
        // Change Notifications
        private IChangeNotificationManager notificationManager;
        private IProfileUpdateManager profileUpdateManager = null;
        private ICustomFieldGenerationManager customFieldGenerationManager = null;

        public CustomFieldGenerationController(IProfileManager profileManager, IErrorLogger errorLogger, IChangeNotificationManager notificationManager, IProfileUpdateManager profileUpdateManager, ICustomFieldGenerationManager customFieldGenerationManager) // Change Notifications
        {
            this.profileManager = profileManager;
            this.errorLogger = errorLogger;
            // Change Notifications
            this.notificationManager = notificationManager;
            this.profileUpdateManager = profileUpdateManager;
            this.customFieldGenerationManager = customFieldGenerationManager;
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
        public ActionResult CustomField() 
        {
            return View();
        } 

        public async Task<ActionResult> GetCustomField()
        {
            var Status = "true";
            IEnumerable<CustomField> ListOfCustomFields = null;
            try
            {	       
		        ListOfCustomFields=await customFieldGenerationManager.getAllCustomField();
	        }
	        catch (Exception ex)
	        {
                errorLogger.LogError(ex);
                Status = ExceptionMessage.NO_CUSTOM_FIELD_EXCEPTION;
	        }
            return Json(new { status = Status, ListOfCustomFields = ListOfCustomFields }, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> CustomFieldTransaction(int ProfileID)
        {
            var Status = "true";
            CustomFieldTransaction CustomFieldTransaction = null;
            try
            {
                CustomFieldTransaction = await customFieldGenerationManager.getCustomFieldTransaction(ProfileID);
	        }
	        catch (Exception ex)
	        {
                errorLogger.LogError(ex);
                Status = ExceptionMessage.NO_CUSTOM_FIELD_EXCEPTION;
	        }
            return Json(new { status = Status, CustomFieldTransaction = CustomFieldTransaction }, JsonRequestBehavior.AllowGet);
        }
        
        [HttpPost]
        public async Task<ActionResult> AddCustomField(CustomFieldViewModel customFieldViewModel)
        {
            var Status = "true";
            CustomField CustomField = null;
            CustomField CustomFieldResult = null;
            try
            {
                CustomField = AutoMapper.Mapper.Map<CustomFieldViewModel, CustomField>(customFieldViewModel, CustomField);

                CustomFieldResult = await customFieldGenerationManager.AddCustomField(CustomField);
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                Status = ExceptionMessage.NO_CUSTOM_FIELD_EXCEPTION;
            }
            return Json(new { status = Status, CustomField = CustomFieldResult }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<ActionResult> AddCustomFieldTansaction(int ProfileID, CustomFieldTransactionViewModel CustomFieldTransactionViewModel)
        {
            var Status = "true";
            CustomFieldTransaction CustomFieldTransaction = null;
            int ID = 0;
            try
            {

                CustomFieldTransaction = AutoMapper.Mapper.Map<CustomFieldTransactionViewModel, CustomFieldTransaction>(CustomFieldTransactionViewModel, CustomFieldTransaction);
                ID = await customFieldGenerationManager.AddCustomFieldTansaction(ProfileID, CustomFieldTransaction);
            }
            catch (Exception ex)
            {
                errorLogger.LogError(ex);
                Status = ExceptionMessage.NO_CUSTOM_FIELD_EXCEPTION;
            }
            return Json(new { status = Status, ID = ID }, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> getRole()
        {
            bool role = false;
            role = await GetUserRole();
            return Json(new { role = role}, JsonRequestBehavior.AllowGet);
        }

        private async Task<bool> GetUserRole()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
            var Role = RoleManager.Roles.FirstOrDefault(r => r.Name == "CCO");

            return user.Roles.Any(r => r.RoleId == Role.Id);
        }
	}
}