using AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal;
using AHC.CD.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository.Notification
{
    public interface IRealTimeNotificationRepository
    {
        int GetAllPendingRequestCount(NotificationDelegate.DependencyDelegate dependencyDelegate);
        dynamic GetAllPendingRequest(NotificationDelegate.DependencyDelegate dependencyDelegate);
        dynamic GetTaskExpiriesCount(NotificationDelegate.DependencyDelegate dependencyDelegate);
    }
}
