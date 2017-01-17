using System.Globalization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AHC.CD.WebUI.MVC;
using AHC.CD.WebUI.MVC.Models;
using System.Collections.Generic;
using AHC.CD.Business.Users;
using AHC.CD.Business.MasterData;

namespace AHC.CD.WebUI.MVC.Controllers
{
    [Authorize(Roles="SuperAdmin")]
    public class InitDataController : Controller
    {
        private IUserManager UserManager;
        private IMasterDataManager MasterDataManager;
        
        public InitDataController(IUserManager userManager, IMasterDataManager masterDataManager)
        {
            this.UserManager = userManager;
            this.MasterDataManager = masterDataManager;
        }

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

        public async Task<string> Index()
        {
            await SampleUser();
            await MasterData();

            return "Init Success";
        }

        // GET: InitData
        public async Task<string> CreateSampleUsers()
        {
            return await SampleUser();
        }

        private async Task<string> SampleUser()
        {
            try
            {
                const string password = "Password@123456";

                var rolesString = new string[] { "ADM", "CCO", "CRA", "TL", "PRO", "CCM", "MGT", "HR" };

                foreach (var roleName in rolesString)
                {
                    //Create Role Admin if it does not exist
                    var role = await AuthRoleManager.FindByNameAsync(roleName);
                    if (role == null)
                    {
                        role = new IdentityRole(roleName);
                        var roleresult = await AuthRoleManager.CreateAsync(role);
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

                for (int i = 0; i < users.Count; i++)
                {
                    var user = await AuthUserManager.FindByNameAsync(users[i].UserName);
                    if (user == null)
                    {
                        var result = await AuthUserManager.CreateAsync(users[i], password);

                        dbUsers.Add(new AHC.CD.Entities.User() { AuthenicateUserId = users[i].Id });

                        if (i >= 0 && i <= 2)
                        {
                            await AddUserToRole(users[i], rolesString[i]);
                        }
                        else if (i >= 3 && i <= 7)
                        {
                            await AddUserToRole(users[i], rolesString[3]);
                        }
                        else if (i >= 8 && i <= 12)
                        {
                            await AddUserToRole(users[i], rolesString[4]);
                        }
                        else
                        {
                            await AddUserToRole(users[i], rolesString[i - 8]);
                        }
                    }
                }

                //new AHC.CD.Data.EFRepository.UserInitializer().Seed(dbUsers);
                await UserManager.AddSampleUsers(dbUsers);

                return "Sample Users Added";
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

        public async Task<string> CreateMigrationUsers()
        {
            try
            {
                var users = await UserManager.GetMigratedUsers();
                var authDetails = new List<string>();
                foreach (var user in users)
                {
                    //Get the NPI number
                    var email = "migratedprovider" + user.UserID + "@access.com";

                    var applicationUser = new ApplicationUser { UserName = email, Email = email };
                    var result = await AuthUserManager.CreateAsync(applicationUser, "Password@123");
                    if (result.Succeeded)
                    {
                        AuthUserManager.AddToRole(applicationUser.Id, "PRO");
                        user.AuthenicateUserId = applicationUser.Id;
                        authDetails.Add(email.ToString());
                    }
                }

                await UserManager.UpdateMigratedUsers();

                return String.Join(",", authDetails);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> InitMasterData()
        {
            return await MasterData();
        }

        private async Task<string> MasterData()
        {
            try
            {
                string info = "Master Data Initialized";

                await MasterDataManager.InitData();

                return info;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}