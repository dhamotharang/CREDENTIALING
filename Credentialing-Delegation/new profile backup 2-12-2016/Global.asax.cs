using PortalTemplate.Areas.CMS.Validation.Managers;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace PortalTemplate
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new RazorViewEngine());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // Calling Model Validator on Application Start :
            ModelValidatorProviders.Providers.Add(new ExtendedDataAnnotationsModelValidatorProvider());
        }

        // Method to Remember User's Current Culture (On Revisit / On Navigation):
        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            HttpCookie cultureCookie = HttpContext.Current.Request.Cookies.Get("cultureCookie");
            string id = "";
            if (cultureCookie != null)
            {
                id = cultureCookie.Value;
            }
            CultureInfo ci = new CultureInfo(id);
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }
    }
}
