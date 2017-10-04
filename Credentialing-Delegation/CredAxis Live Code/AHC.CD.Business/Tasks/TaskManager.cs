using AHC.CD.Data.ADO.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AHC.CD.Business.Tasks
{
    public class TaskManager : ITaskManager
    {
        private ITaskTrackerRepository adoRepo = null;
        public TaskManager(ITaskTrackerRepository adoRepo)
        {
            this.adoRepo = adoRepo;
        }
        public async Task<object> GetAllTasksByUserIdAsync(string authId)
        {
           return await adoRepo.GetAllTasksByUserIdAsync(authId);
        }

        public async Task<object> GetAllMasterDataForTaskTrackerAsync()
        {
            return await adoRepo.GetAllMasterDataForTaskTrackerAsync();
        }

        public async Task<object> GetDashBoardNotificationsAsync(string authId, int skipRecords = 0)
        {
            return await adoRepo.GetDashBoardNotificationsAsync(authId, skipRecords);
        }

        public async Task<object> GetTaskInfoById(int taskId)
        {
            return await adoRepo.GetTaskInfoById(taskId);
        }

        public async Task<IEnumerable<object>> GetAllHistoriesForATask(int taskId)
        {
            return await adoRepo.GetAllHistoriesForATask(taskId);
        }
    }
}
