using AHC.CD.Business.BusinessModels.TaskTracker;
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
        void InactiveTask(int trackerId,string ActionPerformedBy);
        Task<IEnumerable<object>> GetAllProviders();
        Task<IEnumerable<object>> GetAllInsuranceCompanies();
        Task<IEnumerable<AHC.CD.Entities.TaskTracker.TaskTracker>> GetAllTasksByUserId(string userAuthId);
    }
}
