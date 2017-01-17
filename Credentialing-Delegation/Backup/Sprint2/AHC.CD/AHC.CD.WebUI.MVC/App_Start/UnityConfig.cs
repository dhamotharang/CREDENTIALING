using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc5;
using Microsoft.Practices.Unity.Configuration;


namespace AHC.CD.WebUI.MVC
{
    /// <summary>
    /// Author: Venkat
    /// Date:   20/10/2014
    /// Unity IoC for Dependency Injection
    /// Need to put all dependancy types in config file
    /// </summary>
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();

            // Loading from Config File

            container.LoadConfiguration();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}