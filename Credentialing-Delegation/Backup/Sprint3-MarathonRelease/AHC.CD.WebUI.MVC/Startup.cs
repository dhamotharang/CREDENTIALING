using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AHC.CD.WebUI.MVC.Startup))]
namespace AHC.CD.WebUI.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
