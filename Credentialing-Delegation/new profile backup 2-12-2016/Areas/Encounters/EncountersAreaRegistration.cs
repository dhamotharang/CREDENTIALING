using PortalTemplate.Areas.Encounters.Validation.Managers;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Encounters
{
    public class EncountersAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Encounters";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Encounters_default",
                "Encounters/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            ModelValidatorProviders.Providers.Add(new ExtendedDataAnnotationsModelValidatorProvider());
        }
    }
}