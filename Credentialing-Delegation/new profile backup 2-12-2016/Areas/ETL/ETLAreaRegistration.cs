using System.Web.Mvc;

namespace PortalTemplate.Areas.ETL
{
    public class ETLAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "ETL";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "ETL_default",
                "ETL/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}