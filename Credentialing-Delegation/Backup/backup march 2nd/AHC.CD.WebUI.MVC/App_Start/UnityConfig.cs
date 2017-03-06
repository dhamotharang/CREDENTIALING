using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Mvc5;

namespace AHC.CD.WebUI.MVC
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            container.LoadConfiguration();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}