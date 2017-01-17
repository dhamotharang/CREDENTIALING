using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Initiation
{
    public class InitiationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Initiation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Initiation_default",
                "Initiation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}