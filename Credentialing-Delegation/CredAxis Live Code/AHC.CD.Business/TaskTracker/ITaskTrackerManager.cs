using AHC.CD.Business.BusinessModels.TaskTracker;
using AHC.CD.Entities.TaskTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.TaskTracker
{
    public interface ITaskTrackerManager
    {
        AHC.CD.Entities.TaskTracker.TaskTracker AddTask(TaskTrackerBusinessModel taskTracker, string ActionPerformedBy);
        AHC.CD.Entities.TaskTracker.TaskTracker UpdateTask(TaskTrackerBusinessModel taskTracker, string ActionPerformedBy);
        void InactiveTask(int trackerId, string ActionPerformedBy);
        void ReactiveTask(int trackerId, string ActionPerformedBy, string authid);
        Task<IEnumerable<object>> GetAllProviders();
        Task<IEnumerable<object>> GetAllInsuranceCompanies();
        Task<IEnumerable<AHC.CD.Entities.TaskTracker.TaskTracker>> GetAllTasksByUserId(string userAuthId);
        Task<IEnumerable<AHC.CD.Entities.TaskTracker.TaskTracker>> GetAllTasksByProfileId(int profileid);
        Task<bool> SetReminder(List<TaskReminder> reminders, string userAuthID);
        Task<List<TaskReminder>> GetReminders(string userAuthID);
        Task<bool> DismissReminder(int taskID, string userAuthID);
        Task<bool> DismissAllReminder(int[] taskIDs, string userAuthID);
        Task<bool> RescheduleReminder(int taskID, DateTime scheduledDateTime, string userAuthID);

        //AHC.CD.Entities.TaskTracker.TaskTracker GetTaskInfo(int TaskID);

        //string GetUserEmail(string AssignedtoID);

    }
}
