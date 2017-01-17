using System.Web.Mvc;
using PortalTemplate.Areas.Auditing.Validation.Managers;
using PratianUI;
namespace PortalTemplate.Areas.Auditing
{
    public class AuditingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Auditing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Auditing_default",
                "Auditing/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
            ModelValidatorProviders.Providers.Add(new ExtendedDataAnnotationsModelValidatorProvider());
        }
    }
}