using AHC.CD.Business.Users;
using AHC.CD.ErrorLogging;
using AHC.MailService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using System.Globalization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System.Threading.Tasks;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.WebUI.MVC.Areas.Initiation.Models;
using AHC.CD.Resources.Messages;
using AHC.CD.Entities.UserInfo;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Exceptions;

namespace AHC.CD.WebUI.MVC.Areas.Initiation.Controllers
{
    public class InitiateUserController : Controller
    {
        protected IUserManager UserManager { get; set; }
        protected IErrorLogger ErrorLogger { get; set; }
        protected IEmailSender EmailSender { get; set; }

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

        protected ApplicationRoleManager _authRoleManager;
        protected ApplicationRoleManager AuthRoleManager
        {
            get
            {
                return _authRoleManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }
            private set
            {
                _authRoleManager = value;
            }
        }

        public InitiateUserController(IUserManager userManager, IErrorLogger errorLogger, IEmailSender mailService)
        {
            this.UserManager = userManager;
            this.ErrorLogger = errorLogger;
            this.EmailSender = mailService;
        }

       

        //[Authorize(Roles = "ADM")]
        // GET: Initiation/User
        public ActionResult Index()
        {            

            return View();
        }

        [HttpPost]
        //[Authorize(Roles = "ADM")]
        // GET: Initiation/User
        public async Task<ActionResult> Index(UserViewModel newUser)
        {
            var status = "true";
            var profileUserId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    var appUser = new ApplicationUser() { UserName = newUser.Email, Email = newUser.Email, LockoutEnabled = true, FullName = newUser.Name };
                    var user = await AuthUserManager.FindByNameAsync(appUser.UserName);

                    if (user == null)
                    {
                        //Create authentication user with the email address and the password as NPI
                        var authUserId = await CreateUser(appUser, newUser.RoleCode);

                        var userData = AutoMapper.Mapper.Map<UserViewModel, ProfileUser>(newUser);

                        userData.StatusType = StatusType.Active;

                        profileUserId = await UserManager.InitiateUserAsync(authUserId, userData);
                    }
                    else
                    {
                        status = ExceptionMessage.USER_EXIST_EXCEPTION;
                    }

                }
                
            }            
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);                
            }

            return Json(new {status = status, profileUserId = profileUserId }, JsonRequestBehavior.AllowGet);
        }



        private async Task<string> CreateUser(ApplicationUser appUser, string roleCode)
        {
            
            string password = "Password@123456";

            try
            {   
                var result = await AuthUserManager.CreateAsync(appUser, password);
                await AddUserToRole(appUser, roleCode);
                return appUser.Id;
               
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError(ex);
                return ExceptionMessage.USER_ADD_EXCEPTION;
            }
            
        }

        
        protected async Task<string> CreateAuthUser(string userName, string password, string roleCode)
        {
            try
            {
                //Create Role Admin if it does not exist
                var role = await AuthRoleManager.FindByNameAsync(roleCode);
                if (role == null)
                {
                    role = new IdentityRole(roleCode);
                    var roleresult = await AuthRoleManager.CreateAsync(role);
                }

                var appUser = new ApplicationUser() { UserName = userName, Email = userName, LockoutEnabled = true };
                var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
                if (user == null)
                {
                    var result = await AuthUserManager.CreateAsync(appUser, password);
                    await AddUserToRole(appUser, roleCode);
                    return appUser.Id;
                }

                return user.Id;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        protected async Task AddUserToRole(ApplicationUser user, string roleName)
        {
            // Add user admin to Role Admin if not already added
            var rolesForUser = await AuthUserManager.GetRolesAsync(user.Id);
            if (!rolesForUser.Contains(roleName))
            {
                var result = await AuthUserManager.AddToRoleAsync(user.Id, roleName);
            }
        }

        protected void SendEmail(string authUserId, string emailAddress, string password)
        {
            var hosturl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/ConfirmAccount?token=" + authUserId;

            var confirmationLink = string.Format("<a href=\"{0}\">Clink to confirm your registration</a>\n\nLogin with username as {1} and password as {2}", hosturl, emailAddress, password);

            // Send Confirmation Email.
            EMailModel confirmEmailData = new EMailModel();
            confirmEmailData.Subject = "Welcome Email";
            confirmEmailData.To = emailAddress;
            confirmEmailData.From = "venkat@pratian.com";
            confirmEmailData.Body = confirmationLink;
            EmailSender.SendMail(confirmEmailData);
        }
    }
}