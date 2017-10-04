using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Entities;
using AHC.CD.Entities.Notification;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Tasks
{
    public class TaskTrackerRepository : ITaskTrackerRepository
    {
        private static readonly DAPPERRepository repo = null;
        static TaskTrackerRepository()
        {
            repo = new DAPPERRepository();
        }
        public async Task<object> GetAllTasksByUserIdAsync(string authId)
        {
            return await repo.QueryMultipleAsync("[dbo].[sp_GetAllTasksByUserID]", (reader) =>
            {
                var Tasks = reader.Read<object>();
                var CDUserID = reader.ReadSingle<int>();
                return new { Tasks, CDUserID };
            }, true, new DynamicParameters(new { authId }));
        }

        public async Task<object> GetAllMasterDataForTaskTrackerAsync()
        {
            try
            {
                var res = await repo.QueryMultipleAsync("[dbo].[sp_GetAllMasterDataForTaskTracker]", (reader) =>
                          {
                              var Plans = reader.Read<object>();
                              var Hospitals = reader.Read<object>();
                              var ProfileSubSections = reader.Read<object>();
                              var Users = reader.Read<object>();
                              var NotesTemplates = reader.Read<object>();
                              var Providers = reader.Read<object>();
                              return new { Plans, Hospitals, ProfileSubSections, Users, NotesTemplates, Providers };
                          }, true);
                return res;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<object> GetDashBoardNotificationsAsync(string authId, int skip = 0)
        {
            var res = await repo.QueryMultipleAsync("[dbo].[sp_GetDashBoardNotificationsByCdUserId]", (reader) =>
                      {
                          var DashboardNotifications = reader.Read<UserDashboardNotification>();
                          var User = reader.ReadSingle<CDUser>();
                          var UnreadNotificationsCount = reader.ReadSingle<int>();
                          return new { DashboardNotifications, User, UnreadNotificationsCount };
                      }, true, new DynamicParameters(new { authId, skip }));
            return res;
        }

        public async Task<object> GetTaskInfoById(int taskId)
        {
            var res = await repo.ExecuteStoredProcedureAsync<object>("[dbo].[sp_GetTaskInfoById]", new DynamicParameters(new { taskId }));
            return res.FirstOrDefault();
        }

        public async Task<IEnumerable<object>> GetAllHistoriesForATask(int taskId)
        {
            var res = await repo.ExecuteStoredProcedureAsync<object>("[dbo].[sp_GetAllHistoriesForATask]", new DynamicParameters(new { taskId }));
            return res;
        }
    }
}
