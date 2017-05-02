using AHC.CD.Entities.TaskTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository.TaskTracker
{
    public interface ITaskTrackerRepository
    {
        AHC.CD.Entities.TaskTracker.TaskTracker AddTask(AHC.CD.Entities.TaskTracker.TaskTracker taskTracker);
        AHC.CD.Entities.TaskTracker.TaskTracker UpdateTask(AHC.CD.Entities.TaskTracker.TaskTracker taskTracker);
        void InactiveTask(int taskTrackerId);
        void ReactiveTask(int taskTrackerId, int taskID);
        AHC.CD.Entities.TaskTracker.TaskTracker GetTaskById(int taskTrackerId);

        Task<IEnumerable<object>> GetAllProvider();
        Task<IEnumerable<AHC.CD.Entities.TaskTracker.TaskTracker>> GetAllTasksWithUserId(string userAuthId);
        Task<IEnumerable<AHC.CD.Entities.TaskTracker.TaskTracker>> GetAllTasksByUserId(string userAuthId);
        Task<IEnumerable<AHC.CD.Entities.TaskTracker.TaskTracker>> GetAllTasksByProfileId(int profileid);
        Task<bool> SetReminder(List<TaskReminder> reminders);
    }
}
