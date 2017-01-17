using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.CredentialingDelegation
{
    public class CredentialingDelegationAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CredentialingDelegation";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CredentialingDelegation_default",
                "CredentialingDelegation/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}