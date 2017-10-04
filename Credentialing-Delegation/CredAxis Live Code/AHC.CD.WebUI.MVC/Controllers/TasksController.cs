using AHC.CD.Business.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using AHC.CD.WebUI.MVC.Models.TaskTracker;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class TasksController : Controller
    {
        private ITaskManager taskManager = null;
        public TasksController(ITaskManager taskManager)
        {
            this.taskManager = taskManager;
        }
        public async Task<ActionResult> Index()
        {
            ViewBag.Tasks = await GetAllTasksByUserId();
            var roles = await GetCurrentUserRoles();
            ViewBag.IsAdmin = roles.Any(x => x.Equals("adm", System.StringComparison.InvariantCultureIgnoreCase));
            ViewBag.LoggedInUserName = User.Identity.Name;
            return View();
        }

        public string AddNewTask(TaskTrackerViewModel taskTracker)
        {
            return JsonConvert.SerializeObject(taskTracker);
        }

        public async Task<string> GetAllTasksByUserId()
        {
            var res = await taskManager.GetAllTasksByUserIdAsync(GetCurrentUserId());
            return JsonConvert.SerializeObject(res, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
        }

        public async Task<string> GetAllMasterDataForTaskTrackerAsync()
        {
            var res = await taskManager.GetAllMasterDataForTaskTrackerAsync();
            return JsonConvert.SerializeObject(res);
        }

        public async Task<string> GetTaskInfoById(int taskId)
        {
            var res = await taskManager.GetTaskInfoById(taskId);
            return JsonConvert.SerializeObject(res);
        }

        public async Task<string> GetAllHistoriesForATask(int taskId)
        {
            var res = await taskManager.GetAllHistoriesForATask(taskId);
            return JsonConvert.SerializeObject(res);
        }

        #region Private Methods

        public async Task<ActionResult> GetDashBoardNotificationsAsync()
        {
            var res = await taskManager.GetDashBoardNotificationsAsync(GetCurrentUserId());
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> LoadMoreNotificationsAsync(int skipRecords)
        {
            var res = await taskManager.GetDashBoardNotificationsAsync(GetCurrentUserId(), skipRecords);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        private async Task<IEnumerable<string>> GetCurrentUserRoles() => await GetUserManager().GetRolesAsync(GetCurrentUserId());

        private string GetCurrentUserId() => User.Identity.GetUserId();

        private ApplicationUserManager GetUserManager() => HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

        #endregion
    }
}