using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
//using System.Web.Http;
//using Microsoft.Owin.Cors;
[assembly: OwinStartup(typeof(AHC.CD.WebApiForMobileApp.Startup))]

namespace AHC.CD.WebApiForMobileApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //HttpConfiguration config = new HttpConfiguration();
            ConfigureAuth(app);
            //WebApiConfig.Register(config);
           // app.UseCors(Microsoft.Owin.Cors.CorsOptions.Equals();
           // app.UseWebApi(config);
        }
    }
}
