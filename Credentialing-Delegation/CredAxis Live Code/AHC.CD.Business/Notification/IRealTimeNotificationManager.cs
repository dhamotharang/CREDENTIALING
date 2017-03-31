using AHC.CD.Entities.MasterProfile.ProfileUpdateRenewal;
using AHC.CD.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Notification
{
    public interface IRealTimeNotificationManager
    {
        int GetAllPendingRequestCount(NotificationDelegate.DependencyDelegate dependencyDelegate);
        dynamic GetAllPendingRequest(NotificationDelegate.DependencyDelegate dependencyDelegate);
    }
}
