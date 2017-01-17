using PortalTemplate.Areas.Billing.Validation.Managers;
using System.Web.Mvc;

namespace PortalTemplate.Areas.Billing
{
    public class BillingAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Billing";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Billing_default",
                "Billing/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
           // ModelValidatorProviders.Providers.Add(new ExtendedDataAnnotationsModelValidatorProvider());
        }
    }
}