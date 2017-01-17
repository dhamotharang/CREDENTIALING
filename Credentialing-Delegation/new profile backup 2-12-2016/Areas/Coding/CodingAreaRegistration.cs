using PortalTemplate.Areas.Coding.Validation.Managers;
using System.Web.Mvc;
using PratianUI;
namespace PortalTemplate.Areas.Coding
{
    public class CodingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Coding";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Coding_default",
                "Coding/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            ModelValidatorProviders.Providers.Add(new ExtendedDataAnnotationsModelValidatorProvider());
        }
    }
}