using PortalTemplate.Areas.CredAxis.Validation.Managers;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis
{
    public class CredAxisAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "CredAxis";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "CredAxis_default",
                "CredAxis/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            ModelValidatorProviders.Providers.Add(new ExtendedDataAnnotationsModelValidatorProvider());
        }
    }
}