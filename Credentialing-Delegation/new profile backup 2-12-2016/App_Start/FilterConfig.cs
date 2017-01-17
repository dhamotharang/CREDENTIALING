using PortalTemplate.Helper.Filter;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());            
            filters.Add(new IncompatibleBrowser());
        }
    }
}
