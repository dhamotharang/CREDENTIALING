using AHC.CD.Business.TaskTracker;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.WebUI.MVC.Models.TaskTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using Microsoft.AspNet.Identity.Owin;
using AHC.CD.Business.BusinessModels.TaskTracker;
using Newtonsoft.Json;
using AHC.CD.Business.Users;
using AHC.CD.Entities;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class TaskTrackerController : Controller
    {

        protected ApplicationUserManager _authUserManager;
        protected ApplicationUserManager AuthUserManager
        {
            get
            {
                return _authUserManager ?? System.Web.HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
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
                return _roleManager ?? System.Web.HttpContext.Current.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }

        private ITaskTrackerManager taskTrackerManager = null;
        private IUserManager userManager = null;

        public TaskTrackerController(ITaskTrackerManager taskTrackerManager, IUserManager userManager)
        {
            this.taskTrackerManager = taskTrackerManager;
            this.userManager = userManager;
        }

        //
        // GET: /TaskTracker/
        [Authorize(Roles = "CCO,ADM")]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string AddTask(TaskTrackerViewModel taskTracker)
        {
            string status = "false";
            TaskTrackerBusinessModel tracker = null;
            
            try
            {
                var userAuthId = GetUserAuthId();
                taskTracker.AssignedByAuthId = userAuthId;
                tracker = AutoMapper.Mapper.Map<TaskTrackerViewModel, TaskTrackerBusinessModel>(taskTracker, tracker);
                return JsonConvert.SerializeObject(taskTrackerManager.AddTask(tracker, User.Identity.Name));
            }
            catch(Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public string UpdateTask(TaskTrackerViewModel taskTracker)
        {
            string status = "false";
            TaskTrackerBusinessModel tracker = null;
            try
            {
                var userAuthId = GetUserAuthId();
                taskTracker.AssignedByAuthId = userAuthId;
                tracker = AutoMapper.Mapper.Map<TaskTrackerViewModel, TaskTrackerBusinessModel>(taskTracker);
                return JsonConvert.SerializeObject(taskTrackerManager.UpdateTask(tracker, User.Identity.Name));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string Inactivetask(int taskTrackerID)
        {
            string status = "false";
            
            try
            {
                taskTrackerManager.InactiveTask(taskTrackerID,User.Identity.Name);
                status="true";
                return status;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public string Reactivetask(int taskTrackerID)
        {
            string status = "false";
            string authID;
            try
            {
                authID = GetUserAuthId();
                taskTrackerManager.ReactiveTask(taskTrackerID, User.Identity.Name,authID);
                status = "true";
                return status;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<string> GetAllProviders()
        {
            try
            {
                var providers = await taskTrackerManager.GetAllProviders();

                return JsonConvert.SerializeObject(providers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetAllCDUsers()
        {
            List<CDUser> cdUsers = new List<CDUser>();
            try
            {
                var users = await userManager.GetAllCDUsers();
               

                foreach (var user in users)
                {
                    if (user.CDRoles.Any(s => s.CDRole.Name == "Credential Coordinator"))
                    {
                        cdUsers.Add(user);
                    }
                }

                return JsonConvert.SerializeObject(cdUsers, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetAllUsers()
        {
            List<ApplicationUser> loginUsers = new List<ApplicationUser>();

            try
            {
                var users = AuthUserManager.Users.ToList();
                var roles = RoleManager.Roles.ToList();
                var ccoRole = roles.Find(r => r.Name == "CCO");
                foreach (var user in users)
                {
                    if (user.Roles.Any(s => s.RoleId == ccoRole.Id))
                    {
                        loginUsers.Add(user);
                    }
                }
                return JsonConvert.SerializeObject(loginUsers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetAllInsuranceCompanies()
        {

            try
            {
                var InsuranceCompanies = await taskTrackerManager.GetAllInsuranceCompanies();

                return JsonConvert.SerializeObject(InsuranceCompanies);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //manideep
        public async Task<string> GetAllTasksByProfileId(int profileid)
        {
            try
            {
                var result = await taskTrackerManager.GetAllTasksByProfileId(profileid);
                return JsonConvert.SerializeObject(result, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            }
            catch (Exception)
            {
                
                throw;
            }
        }



        public async Task<string> GetAllTasksByUserId()
        {
           
            try
            {
                var tasks = await taskTrackerManager.GetAllTasksByUserId(GetUserAuthId());

                return JsonConvert.SerializeObject(tasks, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Methods

        private string GetUserAuthId()
        {
            var currentUser = HttpContext.User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            ApplicationUser user = AuthUserManager.FindByNameSync(appUser);
            return user.Id;
        }

        #endregion
    }
}