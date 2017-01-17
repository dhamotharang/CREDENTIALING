using System.Web.Mvc;

namespace PortalTemplate.Areas.MH
{
    public class MHAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "MH";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MH_default",
                "MH/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}