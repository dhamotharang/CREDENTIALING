using AHC.CD.WebUI.MVC.ActionFilter;
using AHC.CD.WebUI.MVC.CustomHelpers;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new PasswordExpirationCheckAttribute());
            //filters.Add(new AuditLogAttribute());
        }
    }
}
