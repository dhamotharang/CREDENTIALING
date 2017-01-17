using System;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace PGChat
{
    public class CnDHub : Hub
    {        
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
    }
}