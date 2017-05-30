using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UIThemes_Layout.Startup))]
namespace UIThemes_Layout
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
