using AHC.CD.Business.DocumentWriter;
using AHC.CD.Business.Users;
using AHC.CD.ErrorLogging;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models;
using AHC.CD.WebUI.MVC.Models;
using AHC.MailService;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using System.Globalization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using AHC.CD.Entities.MasterProfile.Contract;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ContractInformation;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Initiation.Models;

namespace AHC.CD.WebUI.MVC.Areas.Initiation.Controllers
{
    public class ProviderController : Controller
    {
        private IUserManager UserManager { get; set; }
        private IErrorLogger ErrorLogger { get; set; }
        private IEmailSender EmailSender { get; set; }

        private ApplicationUserManager _authUserManager;
        public ApplicationUserManager AuthUserManager
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

        private ApplicationRoleManager _authRoleManager;
        public ApplicationRoleManager AuthRoleManager
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

        public ProviderController(IUserManager userManager, IErrorLogger errorLogger, IEmailSender mailService)
        {
            this.UserManager = userManager;
            this.ErrorLogger = errorLogger;
            this.EmailSender = mailService;
        }
        
        // GET: Initiation/Provider
        public ActionResult Index()
        {
            //var authUserId = await CreateAuthUser("sachingarg@pratian.com", "Password@123456");
            //SendEmail(authUserId, "sachingarg@pratian.com");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(ProfileViewModel profile)
        {
            string status = "true";
            int profileId = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    //Create authentication user with the email address and the password as NPI
                    var authUserId = await CreateAuthUser(profile.ContactDetail.EmailIDs[0].EmailAddress, profile.OtherIdentificationNumber.NPINumber);


                    //Create CD user with the given information
                    DocumentDTO contractDocument = null;
                    DocumentDTO cvDocument = null;

                    if (profile.ContractInfoes[0].ContractFile != null)
                    {
                        contractDocument = CreateDocument(profile.ContractInfoes[0].ContractFile);
                    }

                    if (profile.CVInformation.CVFile != null)
                    {
                        cvDocument = CreateDocument(profile.CVInformation.CVFile);
                    }

                    var profileData = AutoMapper.Mapper.Map<ProfileViewModel, AHC.CD.Entities.MasterProfile.Profile>(profile);
                    
                    profileData.StatusType = StatusType.Active;

                    profileId = await UserManager.InitiateProviderAsync(authUserId, profileData, cvDocument, contractDocument);

                    //end Activation Link with username and password
                    //SendEmail(authUserId, profile.ContactDetail.EmailIDs[0].EmailAddress);
                }
                else
                {
                    status = String.Join(", ", ModelState.Keys.SelectMany(key => this.ModelState[key].Errors.Select(x => key + ": " + x.ErrorMessage)));
                }
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
                status = ExceptionMessage.PROFILE_ADD_UPDATE_EXCEPTION;
            }

            return Json(new { status = status, profileId = profileId }, JsonRequestBehavior.AllowGet);
        }

        #region Private Methods

        private async Task<string> CreateAuthUser(string userName, string password)
        {
            try
            {
                //Create Role Admin if it does not exist
                var role = await AuthRoleManager.FindByNameAsync("PRO");
                if (role == null)
                {
                    role = new IdentityRole("PRO");
                    var roleresult = await AuthRoleManager.CreateAsync(role);
                }

                var appUser = new ApplicationUser() { UserName = userName, Email = userName, LockoutEnabled = true };
                var user = await AuthUserManager.FindByNameAsync(appUser.UserName);
                if (user == null)
                {
                    var result = await AuthUserManager.CreateAsync(appUser, password);
                    await AddUserToRole(appUser, "PRO");
                    return appUser.Id;
                }

                return user.Id;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private async Task AddUserToRole(ApplicationUser user, string roleName)
        {
            // Add user admin to Role Admin if not already added
            var rolesForUser = await AuthUserManager.GetRolesAsync(user.Id);
            if (!rolesForUser.Contains(roleName))
            {
                var result = await AuthUserManager.AddToRoleAsync(user.Id, roleName);
            }
        }

        private DocumentDTO CreateDocument(HttpPostedFileBase file, bool isRemoved = false)
        {
            DocumentDTO document = new DocumentDTO() { IsRemoved = isRemoved };

            if (file != null)
            {
                document.FileName = file.FileName;
                document.InputStream = file.InputStream;
            }

            return document;
        }

        private void SendEmail(string authUserId, string emailAddress)
        {
            var hosturl = System.Web.HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + "/Account/ConfirmAccount?token=" + authUserId;

            var confirmationLink = string.Format("<a href=\"{0}\">Clink to confirm your registration</a>\n\nLogin with username as {1} and password as your NPI number", hosturl, emailAddress);

            // Send Confirmation Email.
            EMailModel confirmEmailData = new EMailModel();
            confirmEmailData.Subject = "Welcome Email";
            confirmEmailData.To = emailAddress;
            confirmEmailData.From = "venkat@pratian.com";
            confirmEmailData.Body = confirmationLink;
            EmailSender.SendMail(confirmEmailData);
        }

        #endregion
    }
}