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
            SqlDependency.Start(ConnString);
            //RealTimeNotificationConfig.Instance.RegisterNotification();

        }

        protected void Application_End()
        {
            SqlDependency.Stop(ConnString);
        }

        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.ServerAndNoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
           
        }
    }
}