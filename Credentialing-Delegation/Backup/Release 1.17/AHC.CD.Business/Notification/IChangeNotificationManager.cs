using AHC.CD.Entities.Notification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Notification
{
    public interface IChangeNotificationManager
    {
        Task SaveNotificationDetailAsync(ChangeNotificationDetail changeNotificationDetail);
        List<ChangeNotificationDetail> GetChangeNotificationDetails(string actionPerformedUser);
        bool NotifyChanges(string actionPerformedUser);


    }
}
