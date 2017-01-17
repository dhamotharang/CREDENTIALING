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
        Task SaveNotificationDetailAsyncForCCO(ChangeNotificationDetail changeNotificationDetail, string ApprovalStatus, int credentialingAppointmentDetailID);
        Task SaveNotificationDetailsAsyncForCCM(List<int?> profileIds, List<int>  CCMIds,string AppointmentDate,string ActionPerformedBy);
        Task CancelMeetingNotificationAsyncForCCM(List<int> profileIds, List<int> CCMIds, string AppointmentDate, string ActionPerformedBy);
        List<ChangeNotificationDetail> GetChangeNotificationDetails(string actionPerformedUser = null);
        bool NotifyChanges();
        bool NotifyChangesForCCM();


    }
}
