using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Unity.Mvc5;
using System;

namespace AHC.CD.WebUI.MVC
{
    public static class UnityConfig
    {
        public static Lazy<UnityContainer> container = new Lazy<UnityContainer>();
        public static void RegisterComponents()
        {

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();
            container.Value.LoadConfiguration();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container.Value));
        }
    }
}