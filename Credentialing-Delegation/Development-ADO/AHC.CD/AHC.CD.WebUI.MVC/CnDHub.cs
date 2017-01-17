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
    }
}