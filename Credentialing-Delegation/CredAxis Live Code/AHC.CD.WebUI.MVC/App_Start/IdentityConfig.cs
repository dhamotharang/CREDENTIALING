using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using AHC.CD.WebUI.MVC.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Data.Entity;
using System.Web;
using Microsoft.Owin.Security;
using System.Security.Claims;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Business;
using System.Configuration;

namespace AHC.CD.WebUI.MVC
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private int UsedPasswordLimit = int.Parse(ConfigurationManager.AppSettings["UsedPasswordLimit"]);
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
        }

        public override async Task<IdentityResult> ResetPasswordAsync(string UserID, string UsedToken, string NewPassword)
        {

            var Result = await base.ResetPasswordAsync(UserID, UsedToken, NewPassword);

            if (Result.Succeeded)
            {
                var appuser = await FindByIdAsync(UserID);
                var PasswordHash = PasswordHasher.HashPassword(NewPassword);
                await AddToPasswordHistoryAsync(PasswordHash, NewPassword, appuser, "Reset");
            }

            return Result;
        }

        public override async Task<IdentityResult> ChangePasswordAsync(string UserID, string CurrentPassword, string NewPassword)
        {

            if (await IsMinimumPasswwordAge(UserID, CurrentPassword, NewPassword))
            {
                return await Task.FromResult(IdentityResult.Failed("You have to wait for at least one day to change your password "));
            }
            if (await IsPreviousPassword(UserID, CurrentPassword, NewPassword))
            {
                return await Task.FromResult(IdentityResult.Failed("You cannot reuse previous password"));
            }

            var Result = await base.ChangePasswordAsync(UserID, CurrentPassword, NewPassword);

            if (Result.Succeeded)
            {
                var appuser = await FindByIdAsync(UserID);
                var PasswordHash = PasswordHasher.HashPassword(NewPassword);
                await AddToPasswordHistoryAsync(PasswordHash, NewPassword, appuser, "Change");
            }
            return Result;
        }
        public override async Task<IdentityResult> CreateAsync(ApplicationUser appuser, string Password)
        {
            var PasswordHash = PasswordHasher.HashPassword(Password);
            var Result = await base.CreateAsync(appuser, Password);
            await AddToUsedPasswordAsync(appuser, appuser.PasswordHash);
            return Result;
        }
        public Task AddToUsedPasswordAsync(ApplicationUser appuser, string userpassword)
        {
            appuser.UserUsedPassword.Add(new UsedPassword() { UserID = appuser.Id, HashPassword = userpassword });
            return UpdateAsync(appuser);
        }
        private async Task AddToPasswordHistoryAsync(string PasswordHash, string NewPassword, ApplicationUser appuser, string Type)
        {
            try
            {
                if (Type == "Change")
                {
                    appuser.UserUsedPassword.Add(new UsedPassword() { UserID = appuser.Id, HashPassword = PasswordHash });
                }
                else
                {
                    int flag = 0;
                    for (int i=0;i<appuser.UserUsedPassword.Count;i++)
                    {
                        var IsPasswordHashMatch = PasswordHasher.VerifyHashedPassword(appuser.UserUsedPassword.ElementAt(i).HashPassword, NewPassword);
                        if (IsPasswordHashMatch == PasswordVerificationResult.Success)
                        {
                            flag = 1;
                            appuser.UserUsedPassword.ElementAt(i).CreatedDate = DateTimeOffset.Now;
                            break;
                        }
                    }
                    if (flag==1)
                    {
                        appuser.UserUsedPassword.Add(new UsedPassword() { UserID = appuser.Id, HashPassword = PasswordHash });                   
                    }                    
                }
                await UpdateAsync(appuser);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private async Task<bool> IsPreviousPassword(string UserID, string CurrentPassword, string NewPassword)
        {
            var User = await FindByIdAsync(UserID);
            if (User.UserUsedPassword.OrderByDescending(up => up.CreatedDate).Select(up => up.HashPassword).Take(UsedPasswordLimit).Where(up => PasswordHasher.VerifyHashedPassword(up, NewPassword) != PasswordVerificationResult.Failed).Any())
            {
                return true;
            }
            return false;
        }
        private async Task<bool> IsMinimumPasswwordAge(string UserID, string CurrentPassword, string NewPassword)
        {
            var User = await FindByIdAsync(UserID);
            var tempDate = User.UserUsedPassword.OrderByDescending(up => up.CreatedDate).FirstOrDefault();
            int _minimumPasswordAge = int.Parse(ConfigurationManager.AppSettings["MinimumPasswordAge"]);
            if (tempDate != null)
            {
                if ((DateTime.Today - tempDate.CreatedDate.Date).Days < _minimumPasswordAge)
                {
                    return true;
                }
            }

            return false;
        }


        public ApplicationUser FindByNameSync(ApplicationUser user)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            return userManager.FindByName(user.UserName);
        }

        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options,
            IOwinContext context)
        {
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<ApplicationUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = int.Parse(ConfigurationManager.AppSettings["MinimumPasswordLength"]),
                RequireNonLetterOrDigit = bool.Parse(ConfigurationManager.AppSettings["RequireNonLetterOrDigit"]),
                RequireDigit = bool.Parse(ConfigurationManager.AppSettings["RequireDigit"]),
                RequireLowercase = bool.Parse(ConfigurationManager.AppSettings["RequireLowercase"]),
                RequireUppercase = bool.Parse(ConfigurationManager.AppSettings["RequireUppercase"]),
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(int.Parse(ConfigurationManager.AppSettings["LockoutDuration"]));
            manager.MaxFailedAccessAttemptsBeforeLockout = int.Parse(ConfigurationManager.AppSettings["LockoutThreshold"]);
            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug in here.
            manager.RegisterTwoFactorProvider("PhoneCode", new PhoneNumberTokenProvider<ApplicationUser>
            {
                MessageFormat = "Your security code is: {0}"
            });
            manager.RegisterTwoFactorProvider("EmailCode", new EmailTokenProvider<ApplicationUser>
            {
                Subject = "SecurityCode",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }

    // Configure the RoleManager used in the application. RoleManager is defined in the ASP.NET Identity core assembly
    public class ApplicationRoleManager : RoleManager<IdentityRole>
    {
        public ApplicationRoleManager(IRoleStore<IdentityRole, string> roleStore)
            : base(roleStore)
        {
        }

        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new RoleStore<IdentityRole>(context.Get<ApplicationDbContext>()));
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }
    }

    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your sms service here to send a text message.
            return Task.FromResult(0);
        }
    }

    // This is useful if you do not want to tear down the database each time you run the application.
    // public class ApplicationDbInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    // This example shows you how to create a new database if the Model changes
    public class ApplicationDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            InitializeIdentityForEF(context);
            base.Seed(context);
        }

        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string password = "Password@123456";

            var rolesString = new string[] { "ADM", "CCO", "CRA", "TL", "PRO", "CCM", "MGT", "HR", "SYSADM" };

            var role = roleManager.FindByName("SuperAdmin");
            if (role == null)
            {
                role = new IdentityRole("SuperAdmin");
                var roleresult = roleManager.Create(role);
            }

            var appUser = new ApplicationUser() { UserName = "superadmin@access.com", Email = "superadmin@access.com", LockoutEnabled = false };

            if (userManager.FindByName(appUser.UserName) == null)
            {
                var result = userManager.Create(appUser, password);

                // Add user admin to Role Admin if not already added
                var rolesForUser = userManager.GetRoles(appUser.Id);
                if (!rolesForUser.Contains("SuperAdmin"))
                {
                    result = userManager.AddToRole(appUser.Id, "SuperAdmin");
                }
            }
        }
    }

    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager) { }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager)UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options, IOwinContext context)
        {
            return new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(), context.Authentication);
        }
    }


}
