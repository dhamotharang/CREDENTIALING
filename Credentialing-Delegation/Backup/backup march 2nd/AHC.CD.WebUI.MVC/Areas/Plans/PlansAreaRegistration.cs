using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Plans
{
    public class PlansAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Plans";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Plans_default",
                "Plans/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}