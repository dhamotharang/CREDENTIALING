using AHC.CD.WebUI.MVC.App_Start;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System;
using AHC.CD.WebUI.MVC.CustomHelpers;
using AHC.JobSchedulingService;
using System.Web;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.AspNet.SignalR;
using PGChat;

namespace AHC.CD.WebUI.MVC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private string ConnString = ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString;

        protected void Application_Start()
        {

            //ViewEngines.Engines.RemoveAt(0); // Remove ASPX View Engine
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            UnityConfig.RegisterComponents();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.Configure();

            ModelBinders.Binders.Add(typeof(DateTime), new DateTimeBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new NullableDateTimeBinder());

            ChangeNotificationService.Instance.StartService();
            LicenseExpiryNotificationService.Instance.StartService();
            //SqlDependency.Start(ConnString);
            //RegisterNotification();

        }
        //protected void Application_End()
        //{
        //    SqlDependency.Stop(ConnString);
        //}
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
           
        }

//        private void RegisterNotification()
//        {

//            string connectionString = ConfigurationManager.ConnectionStrings["EFEntityContext"].ConnectionString;

//            string commandText = @"SELECT  dbo.[UserDashboardNotifications].[UserDashboardNotificationID]
//                                          ,dbo.[UserDashboardNotifications].[Action]
//                                          ,dbo.[UserDashboardNotifications].[ActionPerformedByUser]
//                                          ,dbo.[UserDashboardNotifications].[ActionPerformed]
//                                          ,dbo.[UserDashboardNotifications].[Description]
//                                          ,dbo.[UserDashboardNotifications].[RedirectURL]
//                                          ,dbo.[UserDashboardNotifications].[AcknowledgementStatus]
//                                          ,dbo.[UserDashboardNotifications].[Status]
//                                          ,dbo.[UserDashboardNotifications].[LastModifiedDate]
//                                          ,dbo.[UserDashboardNotifications].[CDUser_CDUserID]
//                                      FROM [dbo].[UserDashboardNotifications]";

//            SqlDependency.Start(connectionString);
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {

//                using (SqlCommand command = new SqlCommand(commandText, connection))
//                {
//                    connection.Open();
//                    var sqlDependency = new SqlDependency(command);


//                    sqlDependency.OnChange += new OnChangeEventHandler(sqlDependency_OnChange);
//                    using (SqlDataReader reader = command.ExecuteReader())
//                    {
//                    }
//                }
//            }
//        }
//        private void sqlDependency_OnChange(object sender, SqlNotificationEventArgs e)
//        {

//            if (e.Type == SqlNotificationType.Change)
//            {
//                CnDHub Notify = new CnDHub();
//                Notify.NotificationMessage();
//            }
//            RegisterNotification();
//        }

    }
}