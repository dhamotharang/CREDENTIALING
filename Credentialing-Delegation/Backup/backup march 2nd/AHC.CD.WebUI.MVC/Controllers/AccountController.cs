using System.Globalization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AHC.CD.WebUI.MVC;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.WebUI.MVC.ActionFilter;
using AHC.CD.Business.Notification;
using AHC.CD.Data.EFRepository;
using AHC.MailService;
using AHC.CD.WebUI.MVC.Areas.Initiation.Models;
using System.Collections.Generic;
using AHC.CD.Data.Repository;
using AHC.CD.Entities;
using AHC.CD.Business.Users;
using System.Configuration;
using System.Web.Mail;
using System.Net.Mail;
using System.Text;
using AHC.CD.WebUI.MVC.CustomHelpers;
using AHC.ActivityLogging;

namespace AHC.CD.WebUI.MVC.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        IEmailSender iEmailSender = null;
        IChangeNotificationManager notificationManager;
        //private IUserRepository userrepository;
        private ICDRoleManager cdrolemanager = null;
        public AccountController()
        {
            this.notificationManager = new ChangeNotificationManager(new EFUnitOfWork(), new EmailSender());
            this.cdrolemanager = new CDRoleManager(new EFUnitOfWork());
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager, IEmailSender iEmailSender)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            this.iEmailSender = iEmailSender;
        }
        
        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        public ActionResult ViewUsers()
        {
            return View();
        }

        public JsonResult SearchUser()
        {
            List<UsersList> userlist = new List<UsersList>();
            try
            {
                foreach (var user in UserManager.Users)
                {
                    UsersList u = new UsersList();
                    var result = UserManager.GetRoles(user.Id);
                    if (result.Count() > 1)
                    {
                        foreach (var item in result)
                        {
                            UsersList u1 = new UsersList();
                            u1.Email = user.Email;
                            u1.Name = user.FullName;
                            u1.Role = item;
                            u1.AuthenticateUserId = user.Id;
                            u1.NumberofRoles = result.Count();
                            userlist.Add(u1);
                        }
                    }
                    else
                    {
                        u.Email = user.Email;
                        u.Name = user.FullName;
                        u.Role = result.FirstOrDefault();
                        u.AuthenticateUserId = user.Id;
                        u.NumberofRoles = 1;
                        userlist.Add(u);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(userlist, JsonRequestBehavior.AllowGet);
        }

        public async Task<bool> RemoveRoleofaUser(string role, string email, string authid)
        {
            try
            {
                var user = await UserManager.FindByEmailAsync(email);

                var result = cdrolemanager.RemoveRoleofaUser(role, authid);
                if (result)
                {
                    var res = UserManager.RemoveFromRole(user.Id, role);
                    UserManager.Update(user);
                    return res.Succeeded;
                }
                else { return false; }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> AddRoleforaUser(string role, string email, string authid)
        {
            try
            {
                var user = await UserManager.FindByEmailAsync(email);
                var result = await cdrolemanager.AddNewRoletoaUser(role, authid);
                if (!UserManager.IsInRole(user.Id, role))
                {
                    var res = UserManager.AddToRole(user.Id, role);
                    return res.Succeeded;
                }
                else return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> PasswordReset(string Email)
        {

            //if (!ModelState.IsValid)
            //{
            //    return View(model);
            //}
            var user = await UserManager.FindByNameAsync(Email);
            //if (user == null)
            //{
            //    // Don't reveal that the user does not exist
            //    //return RedirectToAction("ResetPasswordConfirmation", "Account");
            //    ModelState.AddModelError("", "EmailAddress does not Exist");
            //    return View(model);
            //}
            var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
            var result = await UserManager.ResetPasswordAsync(user.Id, code, "Password@123456");
            AddErrors(result);
            return result.Succeeded;
            //return View();
        }

        public async Task<JsonResult> ChangeRole(string NewRoleCode, string Email, string authId, string OldRoleCode)
        {
            UsersList u = new UsersList();
            try
            {
                var user = UserManager.FindByEmail(Email);
                var res = await UserManager.RemoveFromRoleAsync(user.Id, OldRoleCode);
                UserManager.AddToRole(user.Id, NewRoleCode);
                cdrolemanager.ChangeRoleofaUser(NewRoleCode, authId, OldRoleCode);
                UserManager.Update(user);
                u.Name = user.FullName;
                u.Email = user.Email;

            }
            catch (Exception ex)
            {
                throw;
            }

            return Json(u, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        [SkipPasswordExpirationCheck]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (returnUrl != null && returnUrl.Contains("LogOff"))
            {
                ViewBag.ReturnUrl = null;

            }
            return View();
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SkipPasswordExpirationCheck]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doen't count login failures towards lockout only two factor authentication
            // To enable password failures to trigger lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: true);

            switch (result)
            {
                case SignInStatus.Success:
                    if (!string.IsNullOrEmpty(returnUrl) && returnUrl != "/")
                    {
                        return RedirectToLocal(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Dashboard");
                    }
                case SignInStatus.LockedOut:
                    SendLockoutMail(ConfigurationManager.AppSettings["LockoutMailID"].ToString(), model.Email, HttpContext.Request.UserHostAddress);
                    AuditLockoutLog(HttpContext, model.Email);
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    //return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid login attempt.");
                    return View(model);
            }
        }

        

       

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                ViewBag.Status = "For DEMO purposes the current " + provider + " code is: " + await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: false, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        ////
        //// POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        //        var result = await UserManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded)
        //        {
        //            var code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking this link: <a href=\"" + callbackUrl + "\">link</a>");
        //            ViewBag.Link = callbackUrl;
        //            //Session["Roles"] = UserManager.GetRoles(User.Identity.GetUserId()).ToArray();
        //            return RedirectToAction("Index", "Home");
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        [AllowAnonymous]
        public async Task<ActionResult> ConfirmAccount(string token)
        {
            if (String.IsNullOrEmpty(token))
            {
                return View("Error");
            }

            var user = await UserManager.FindByIdAsync(token);
            if (user != null)
            {
                var result = await UserManager.SetTwoFactorEnabledAsync(token, false);

                return View(result.Succeeded ? "Login" : "Error");
            }
            return View("Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                var code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>");
                ViewBag.Link = callbackUrl;
                return View("ForgotPasswordConfirmation");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        [SkipPasswordExpirationCheck]
        public ActionResult LogOff()
        {
            //notificationManager.NotifyChanges(User.Identity.Name);
            Session.Clear();
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");

        }

        public ActionResult LogOffSecondaryReson()
        {
            //notificationManager.NotifyChanges(User.Identity.Name);
            Session.Clear();
            AuthenticationManager.SignOut();
            return RedirectToAction("Login", "Account");

        }

        public ActionResult LogoutProfile(string msg)
        {
            ViewBag.msg = msg;
            Session.Clear();
            AuthenticationManager.SignOut();
            return View();
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }


        private void SendLockoutMail(string LockoutMailID,string UserName,string IPaddress)
        {
            try
            {
                System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

                mail.From = new MailAddress(ConfigurationManager.AppSettings["ProductEmailID"].ToString());
                mail.To.Add(LockoutMailID);
                mail.Subject = ConfigurationManager.AppSettings["LockoutMailSubject"].ToString();
                mail.IsBodyHtml = true;
                string htmlBody;
                htmlBody = "Hi Team,<br/><br/><br/><br/><br/><br/>We found a suspicious activities with username <b>" + UserName + "</b>  and from  IP Address <b>" + IPaddress + "</b><br/><br/>" + "and this user name is lockout for <b>" + ConfigurationManager.AppSettings["LockoutDuration"].ToString() + "</b> mins ." + "<br/><br/><br/><br/><br/><br/><br/><br/>" + ConfigurationManager.AppSettings["LockoutDisclaimerMessage"].ToString();
                mail.Body = htmlBody;
                SmtpClient smtpClient = new SmtpClient();
                Task.Factory.StartNew(() =>
                {
                    smtpClient.Send(mail);
                    mail.Dispose();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }


        private void AuditLockoutLog(HttpContextBase HttpContext,string Username)
        {
            AuditMessage auditMessage = new AuditMessage();
            auditMessage.Controller = "Account";
            auditMessage.Action = "Login";
            auditMessage.DateTime = HttpContext.Timestamp;
            auditMessage.User = Username;
            auditMessage.QueryString = HttpContext.Request.QueryString.ToString();
            auditMessage.URL = HttpContext.Request.RawUrl;
            auditMessage.Category = "Alert";
            auditMessage.IP = HttpContext.Request.UserHostAddress;

            IActivityLogger logger = ActivityLogFactory.Instance.GetActivityLogger();

            Task.Factory.StartNew(() =>
            {
                logger.Log(auditMessage);
            });
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Dashboard");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}