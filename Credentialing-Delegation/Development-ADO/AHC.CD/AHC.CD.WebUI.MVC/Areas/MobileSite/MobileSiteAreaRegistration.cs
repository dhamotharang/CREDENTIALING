using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.MobileSite
{
    public class MobileSiteAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                //mobile site
                return "MobileSite";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "MobileSite_default",
                "MobileSite/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}