using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Helper
{
    public class CustomSignalRDependencyResolver : DefaultDependencyResolver
    {
        private readonly IServiceProvider _serviceProvider;

        public CustomSignalRDependencyResolver(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override object GetService(Type serviceType)
        {
            var service = _serviceProvider.GetService(serviceType);

            return service ?? base.GetService(serviceType);
        }

    }
}