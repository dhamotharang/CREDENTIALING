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
using AHC.CD.Entities.TaskTracker;
using PGChat;
using AutoMapper;
using System.IO;

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
        [Authorize(Roles = "CCO,ADM,CRA")]
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
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPost]
        public string UpdateTask(TaskTrackerViewModel taskTracker)
        {
            //string status = "false";
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
                taskTrackerManager.InactiveTask(taskTrackerID, User.Identity.Name);
                status = "true";
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
                taskTrackerManager.ReactiveTask(taskTrackerID, User.Identity.Name, authID);
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
                    if (user.CDRoles.Any(s => s.CDRole.Code == "CCO" || s.CDRole.Code == "CRA"))
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
        public string GetCDUsers()
        {
            List<object> cdUsers = new List<object>();
            try
            {
                var users = userManager.GetAllCDUsers1();
                foreach (var user in users)
                {
                    user.CDRoles = null;
                    var user1 = new { CDUserID = user.CDUserID, AuthenicateUserId = user.AuthenicateUserId };
                    cdUsers.Add(user1);

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
                var craRole = roles.Find(r => r.Name == "CRA");
                foreach (var user in users)
                {
                    if (user.Roles.Any(s => s.RoleId == ccoRole.Id || s.RoleId == craRole.Id))
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

        public async Task<string> SetReminder(List<TaskReminderViewModel> reminders)
        {
            try
            {
                foreach (var reminder in reminders)
                {
                    if (reminder.ScheduledDateTime == null)
                    {
                        reminder.ScheduledDateTime = reminder.LastModifiedDate.ToString();
                    }
                }
                Mapper.CreateMap<TaskReminderViewModel, TaskReminder>().ForMember(d => d.ScheduledDateTime, opt => opt.MapFrom(src => Convert.ToDateTime(src.ScheduledDateTime)));
                var taskReminders = Mapper.Map<List<TaskReminderViewModel>, List<TaskReminder>>(reminders);
                var status = await taskTrackerManager.SetReminder(taskReminders, GetUserAuthId());

                return status.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ActionResult> GetReminders()
        {
            try
            {
                var reminders = await taskTrackerManager.GetReminders(GetUserAuthId());
                string responseView = RenderPartialToString(this.ControllerContext, "~/Views/Prototypes/Reminders/_reminderNotification.cshtml", null);
                return Json(new { reminders = reminders, responseView = responseView }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DismissReminder(int taskID)
        {
            try
            {
                var status = await taskTrackerManager.DismissReminder(taskID, GetUserAuthId());

                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DismissAllReminder(int[] taskIDs)
        {
            try
            {
                var status = await taskTrackerManager.DismissAllReminder(taskIDs, GetUserAuthId());

                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPost]
        public async Task<bool> RescheduleReminder(int taskID, double scheduledDateTime)
        {
            try
            {
                //DateTime _scheduledDateTime = Convert.ToDateTime(scheduledDateTime);
                //DateTime _scheduledDateTime = DateTime.Now.AddMilliseconds(scheduledDateTime);
                var status = await taskTrackerManager.RescheduleReminder(taskID, scheduledDateTime, GetUserAuthId());

                return status;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #region Private Methods

        private string GetUserAuthId()
        {
            var currentUser = User.Identity.Name;
            var appUser = new ApplicationUser() { UserName = currentUser };
            ApplicationUser user = AuthUserManager.FindByNameSync(appUser);
            return user.Id;
        }

        private string RenderPartialToString(ControllerContext controllerContext, String viewURL, Object model)
        {
            if (model != null)
            {
                controllerContext.Controller.ViewData.Model = model;
            }
            using (var sw = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewURL);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        #endregion
    }
}