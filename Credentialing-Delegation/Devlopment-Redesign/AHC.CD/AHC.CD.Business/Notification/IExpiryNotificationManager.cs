using AHC.CD.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Notification
{
    public interface IExpiryNotificationManager
    {
        Task<IEnumerable<ExpiryNotificationDetail>> GetExpiries(string userId = null);
        Task SaveExpiryNotificationAsync();
        void NotifyExpiries();
        Task<ExpiryNotificationDetail> GetAllExpiryForAProvider(int profileID);
    }
}
