using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebApiForMobileApp
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
