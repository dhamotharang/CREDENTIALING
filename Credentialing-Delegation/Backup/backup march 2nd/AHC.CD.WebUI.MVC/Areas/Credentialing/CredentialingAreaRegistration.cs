using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing
{
    public class CredentialingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Credentialing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Credentialing_default",
                "Credentialing/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}