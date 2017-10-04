using System.Collections.Generic;
using System.Threading.Tasks;

namespace AHC.CD.Business.Tasks
{
    public interface ITaskManager
    {
        Task<object> GetAllTasksByUserIdAsync(string authId);
        Task<object> GetAllMasterDataForTaskTrackerAsync();
        Task<object> GetDashBoardNotificationsAsync(string authId, int skipRecords = 0);
        Task<object> GetTaskInfoById(int taskId);
        Task<IEnumerable<object>> GetAllHistoriesForATask(int taskId);
    }
}
