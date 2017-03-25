using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.PlanPDF
{
    public class PlanPDFAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "PlanPDF";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "PlanPDF_default",
                "PlanPDF/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}