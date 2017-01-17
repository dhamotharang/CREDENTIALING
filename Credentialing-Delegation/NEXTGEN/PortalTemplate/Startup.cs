using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortalTemplate.Startup))]
namespace PortalTemplate
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
