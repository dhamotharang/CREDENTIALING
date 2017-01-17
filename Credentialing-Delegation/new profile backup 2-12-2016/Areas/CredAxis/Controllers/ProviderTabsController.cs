using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalTemplate.Areas.CredAxis.Controllers
{
    public class ProviderTabsController : Controller
    {
        //
        // GET: /CredAxis/ProviderTabs/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetSummaryPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/OtherTabs/_summary.cshtml");
        }

        public ActionResult GetDemographicsPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Demographics/_Demographic.cshtml");
        }

        public ActionResult GetLicencesPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Licenses/_LicensesIndex.cshtml");
        }

        public ActionResult GetSpecialtyPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/Specialty/_Specialty.cshtml");
        }

        public ActionResult GetEducationPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EducationHistory/_EducationHistoryIndex.cshtml");
        }

        public ActionResult GetWorkPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/WorkHistory/_WorkHistory.cshtml");
        }

        public ActionResult GetAffiliationPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalAffiliation/_ProfessionalAffiliation.cshtml");
        }

        public ActionResult GetLiabilityPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalLiability/_ProfessionalLiabilityIndex.cshtml");
        }

        public ActionResult GetEmploymentPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/EmploymentInformation/_EmploymentInfoIndex.cshtml");
        }

        public ActionResult GetProfessionalPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/ProfessionalReference/_ProfessionalreferenceIndex.cshtml");
        }
      
        public ActionResult GetDisclousureQuestionPartial()
        {
            return PartialView("~/Areas/CredAxis/Views/ProviderProfile/Tabs/DisclosureQuestions/_DisclosureQuestions.cshtml");
        }

	}
}