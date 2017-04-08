using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using AHC.CD.Business.Notification;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace PGChat
{
    public class CnDHub : Hub
    {
        private IRealTimeNotificationManager manager = new RealTimeNotificationManager();
                
        public void Logout(string userName)
        {
            Clients.All.logoutCall(userName);
        }
        //public void NotificationMessage()
        //{
        //    Clients.All.Notification();
        //}
        
        public void NotificationMessage()
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<CnDHub>();
            context.Clients.All.Notification();
        }


        public void NotifyPendingRequestCount()
        {
            var data = manager.GetAllPendingRequestCount(sqlDependency_OnChange);

            var context = GlobalHost.ConnectionManager.GetHubContext<CnDHub>();
            context.Clients.All.NotifyCount(data);
        }

        private void sqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                NotifyPendingRequestCount();
            }
        }

        public void NotifyPendingRequest()
        {
            
            //var pendingRequestData = manager.GetAllPendingRequest(sqlDependency_OnChangeOfUpdatesTracker);

            //var context = GlobalHost.ConnectionManager.GetHubContext<CnDHub>();
            //context.Clients.All.NotifyPendingRequestData(pendingRequestData);

            var data = manager.GetAllPendingRequestCount(sqlDependency_OnChangeOfUpdatesTracker);

            var context = GlobalHost.ConnectionManager.GetHubContext<CnDHub>();
            context.Clients.All.NotifyPendingRequestData();
        }

        private void sqlDependency_OnChangeOfUpdatesTracker(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                NotifyPendingRequest();
            }
        }
    }

    
}