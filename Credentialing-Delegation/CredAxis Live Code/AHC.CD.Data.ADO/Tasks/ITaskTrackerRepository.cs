using System.Collections.Generic;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Tasks
{
    public interface ITaskTrackerRepository
    {
        Task<object> GetAllTasksByUserIdAsync(string authId);
        Task<object> GetAllMasterDataForTaskTrackerAsync();
        Task<object> GetDashBoardNotificationsAsync(string authId, int skip = 0);
        Task<object> GetTaskInfoById(int taskId);
        Task<IEnumerable<object>> GetAllHistoriesForATask(int taskId);
    }
}
