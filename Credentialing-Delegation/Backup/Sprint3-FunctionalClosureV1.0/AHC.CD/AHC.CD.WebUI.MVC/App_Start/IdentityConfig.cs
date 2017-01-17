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

namespace AHC.CD.WebUI.MVC
{
    // Configure the application user manager used in this application. UserManager is defined in ASP.NET Identity and is used by the application.

    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store)
            : base(store)
        {
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
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };
            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;
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

        //Create User=Admin@Admin.com with password=Admin@123456 in the Admin role        
        public static void InitializeIdentityForEF(ApplicationDbContext db)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var roleManager = HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            const string password = "Password@123456";

            var rolesString = new string[] { "ADM", "CCO", "CRA", "TL", "PRO", "CCM", "MGT", "HR" };

            foreach (var roleName in rolesString)
            {
                //Create Role Admin if it does not exist
                var role = roleManager.FindByName(roleName);
                if (role == null)
                {
                    role = new IdentityRole(roleName);
                    var roleresult = roleManager.Create(role);
                }
            }
             //Roles. = rolesDb.Where(r => r.Name.Equals("Provider")).ToList()
            var users = new List<ApplicationUser>()
            {
                new ApplicationUser() { UserName = "admin@access.com", Email = "admin@access.com", LockoutEnabled = false },
                new ApplicationUser() { UserName = "cco@access.com", Email = "cco@access.com", LockoutEnabled = false },
                new ApplicationUser() { UserName = "credadmin@access.com", Email = "credadmin@access.com", LockoutEnabled = false },

                new ApplicationUser() { UserName = "teamlead1@access.com", Email = "teamlead1@access.com", LockoutEnabled = false },
                new ApplicationUser() { UserName = "teamlead2@access.com", Email = "teamlead2@access.com", LockoutEnabled = false },
                new ApplicationUser() { UserName = "teamlead3@access.com", Email = "teamlead3@access.com", LockoutEnabled = false },
                new ApplicationUser() { UserName = "teamlead4@access.com", Email = "teamlead4@access.com", LockoutEnabled = false },
                new ApplicationUser() { UserName = "teamlead5@access.com", Email = "teamlead5@access.com", LockoutEnabled = false },

                new ApplicationUser() { UserName = "provider1@access.com", Email = "provider1@access.com", LockoutEnabled = false  },
                new ApplicationUser() { UserName = "provider2@access.com", Email = "provider2@access.com", LockoutEnabled = false },
                new ApplicationUser() { UserName = "provider3@access.com", Email = "provider3@access.com", LockoutEnabled = false },
                new ApplicationUser() { UserName = "provider4@access.com", Email = "provider4@access.com", LockoutEnabled = false },
                new ApplicationUser() { UserName = "provider5@access.com", Email = "provider5@access.com", LockoutEnabled = false },
                
                new ApplicationUser() { UserName = "credcommitte@access.com", Email = "credcommitte@access.com", LockoutEnabled = false },

                new ApplicationUser() { UserName = "mgtuser@access.com", Email = "mgtuser@access.com", LockoutEnabled = false },
                new ApplicationUser() { UserName = "hr@access.com", Email = "hr@access.com", LockoutEnabled = false },
                
                
            };

            List<AHC.CD.Entities.User> dbUsers = new List<Entities.User>();

            //var profiles = new List<Profile>()

            for (int i = 0; i < users.Count; i++)
            {
                var user = userManager.FindByName(users[i].UserName);
                if (user == null)
                {
                    var result = userManager.Create(users[i], password);

                    dbUsers.Add(new AHC.CD.Entities.User() { AuthenicateUserId = users[i].Id });

                    if(i >=0 && i <= 2)
                    {
                        AddUserToRole(userManager, users[i], rolesString[i]);
                    }
                    else if (i >= 3 && i <= 7)
                    {
                        AddUserToRole(userManager, users[i], rolesString[3]);
                    }
                    else if (i >= 8 && i <= 12)
                    {
                        AddUserToRole(userManager, users[i], rolesString[4]);
                    }
                    else
                    {
                        AddUserToRole(userManager, users[i], rolesString[i-8]);
                    }
                }    
            }

            new AHC.CD.Data.EFRepository.UserInitializer().Seed(dbUsers);
        }

        private static void AddUserToRole(ApplicationUserManager userManager, ApplicationUser user, string roleName)
        {
            // Add user admin to Role Admin if not already added
            var rolesForUser = userManager.GetRoles(user.Id);
            if (!rolesForUser.Contains(roleName))
            {
                var result = userManager.AddToRole(user.Id, roleName);
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
