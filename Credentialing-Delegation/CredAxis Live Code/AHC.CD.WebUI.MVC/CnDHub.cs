using System;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using AHC.CD.Business.Notification;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Threading.Tasks;
using AHC.CD.WebUI.MVC.Models;
using AHC.CD.WebUI.MVC;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using AHC.CD.WebUI.MVC;
using Microsoft.Practices.Unity;
using System.Threading;
using System.Security.Principal;
namespace PGChat
{
    public class CnDHub : Hub
    {
        public CnDHub()
        {
            manager = UnityConfig.container.Value.Resolve<IRealTimeNotificationManager>();
        }
        private IRealTimeNotificationManager manager ;

        public void Logout(string userName)
        {
            Clients.All.logoutCall(userName);
        }
        //public void NotificationMessage()
        //{
        //    Clients.All.Notification();
        //}
        //public override Task OnConnected()
        //{

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

        public void NotifyPendingRequest()
        {
            
            //var pendingRequestData = manager.GetAllPendingRequest(sqlDependency_OnChangeOfUpdatesTracker);

            //var context = GlobalHost.ConnectionManager.GetHubContext<CnDHub>();
            //context.Clients.All.NotifyPendingRequestData(pendingRequestData);

            var data = manager.GetAllPendingRequestCount(sqlDependency_OnChangeOfUpdatesTracker);

            var context = GlobalHost.ConnectionManager.GetHubContext<CnDHub>();
            context.Clients.All.NotifyPendingRequestData();
        }

        public void StoreCDUserIDIntoSession(string userAuthID)
        {
            dynamic userIDs = new System.Dynamic.ExpandoObject();

            var userID = manager.GetCDUserID(userAuthID);

            userIDs.CDUserID = userID;
            userIDs.UserAuthID = userAuthID;

            var context = GlobalHost.ConnectionManager.GetHubContext<CnDHub>();
            context.Clients.All.NotifyCDUserID(userIDs);
        }

        public void NotifyTasksExpiriesCount()
        {

            var data = manager.GetTaskExpiriesCount(sqlDependency_OnChangeOfTaskExpiries);

            var context = GlobalHost.ConnectionManager.GetHubContext<CnDHub>();
            context.Clients.All.NotifyClientTasksExpiriesCount(data);
        }        

        #region Private Methods

        private void sqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                NotifyPendingRequestCount();
            }
        }

        private void sqlDependency_OnChangeOfUpdatesTracker(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                NotifyPendingRequest();
            }
        }

        private void sqlDependency_OnChangeOfTaskExpiries(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                NotifyTasksExpiriesCount();
            }
        }
        
       
        #endregion

    }

    
}