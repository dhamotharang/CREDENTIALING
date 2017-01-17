using AHC.CachingService;
using AHC.CD.Business;
using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.ProviderInfo;
using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Credentialing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class InitCredentialingController : Controller
    {

       
        public InitCredentialingController()
        {
          
        }



        // GET: InitCredentialing
        public ActionResult Index()
        {
           
        return View();
        }

        public JsonResult GetAllProviders()
        {

            List<ProviderCredentialiingViewModel> ProvidersList = new List<ProviderCredentialiingViewModel>();

            ProviderCredentialiingViewModel ProviderCredentialiingViewModel1 = new ProviderCredentialiingViewModel();

            ProviderCredentialiingViewModel1.FirstName = "John";
            ProviderCredentialiingViewModel1.MiddleName = "Smith";
            ProviderCredentialiingViewModel1.LastName = "Lew";
            ProviderCredentialiingViewModel1.Image = "provider1.jpg";
            ProviderCredentialiingViewModel1.Title = "MO";
            ProviderCredentialiingViewModel1.NPI = "17231123418";
            ProviderCredentialiingViewModel1.Specialities.Add("PO");
            ProviderCredentialiingViewModel1.Specialities.Add("MO");

            PlanViewModel plan1 = new PlanViewModel();

            plan1.PlanID = 1;
            plan1.Title = "plan1";
            plan1.InsuranceCompany = "Freedom";
            plan1.Logo = "Freedom.jpg";

            PlanCredentialingViewModel PlanCredentialingViewModel1 = new PlanCredentialingViewModel();
            PlanCredentialingViewModel1.Status = "Initiated";
            PlanCredentialingViewModel1.Type = "Independent";
            PlanCredentialingViewModel1.Specialty = "MO";
            PlanCredentialingViewModel1.GroupName = "N/A";
            PlanCredentialingViewModel1.Location = "LocationDetails";
            PlanCredentialingViewModel1.Plan = plan1;


            ProviderCredentialiingViewModel1.relatedPlans.Add(PlanCredentialingViewModel1);




            ProviderCredentialiingViewModel ProviderCredentialiingViewModel2 = new ProviderCredentialiingViewModel();

            ProviderCredentialiingViewModel2.FirstName = "Mc.";
            ProviderCredentialiingViewModel2.MiddleName = "";
            ProviderCredentialiingViewModel2.LastName = "Angellica";
            ProviderCredentialiingViewModel2.Image = "provider2.jpg";
            ProviderCredentialiingViewModel2.Title = "PO";
            ProviderCredentialiingViewModel2.NPI = "17231126633";
            ProviderCredentialiingViewModel2.Specialities.Add("PO");
            

            PlanViewModel plan2 = new PlanViewModel();

            plan2.PlanID = 2;
            plan2.Title = "plan2";
            plan2.InsuranceCompany = "Coventry";
            plan2.Logo = "Coventry.jpg";

            PlanCredentialingViewModel PlanCredentialingViewModel2 = new PlanCredentialingViewModel();
            PlanCredentialingViewModel2.Status = "Credentialed";
            PlanCredentialingViewModel2.Type = "Group";
            PlanCredentialingViewModel2.Specialty = "MO";
            PlanCredentialingViewModel2.GroupName = "Group1";
            PlanCredentialingViewModel2.Location = "LocationDetails";
            PlanCredentialingViewModel2.Plan = plan2;


            ProviderCredentialiingViewModel2.relatedPlans.Add(PlanCredentialingViewModel2);


            ProviderCredentialiingViewModel ProviderCredentialiingViewModel3 = new ProviderCredentialiingViewModel();

            ProviderCredentialiingViewModel3.FirstName = "Sachin";
            ProviderCredentialiingViewModel3.MiddleName = "";
            ProviderCredentialiingViewModel3.LastName = "Sharma";
            ProviderCredentialiingViewModel3.Image = "provider3.jpg";
            ProviderCredentialiingViewModel3.Title = "PO";
            ProviderCredentialiingViewModel3.NPI = "37233323438";
            ProviderCredentialiingViewModel3.Specialities.Add("PO");
            ProviderCredentialiingViewModel3.Specialities.Add("MO");

            PlanViewModel plan3 = new PlanViewModel();

            plan3.PlanID = 3;
            plan3.Title = "plan3";
            plan3.InsuranceCompany = "Coventry";
            plan3.Logo = "Coventry.jpg";

            PlanCredentialingViewModel PlanCredentialingViewModel3 = new PlanCredentialingViewModel();
            PlanCredentialingViewModel3.Status = "Credentialed";
            PlanCredentialingViewModel3.Type = "Group";
            PlanCredentialingViewModel3.Specialty = "MO";
            PlanCredentialingViewModel3.GroupName = "Group1";
            PlanCredentialingViewModel3.Location = "LocationDetails";
            PlanCredentialingViewModel3.Plan = plan3;


            ProviderCredentialiingViewModel3.relatedPlans.Add(PlanCredentialingViewModel3);


            ProvidersList.Add(ProviderCredentialiingViewModel1);
            ProvidersList.Add(ProviderCredentialiingViewModel2);
            ProvidersList.Add(ProviderCredentialiingViewModel3);


            return Json(ProvidersList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Plans(int providerID)
        {
            //IEnumerable<Plan> plans = await individualCredentialingManager.GetAllNonInitiatedPlansForProviderAsync(providerID);

            List<PlanViewModel> Plans = new List<PlanViewModel>();

            PlanViewModel plan1 = new PlanViewModel();

            plan1.PlanID = 1;
            plan1.Title = "HEALTH OPTIONS";
            plan1.InsuranceCompany = "Freedom";
            plan1.Logo = "Freedom.jpg";

            PlanViewModel plan2 = new PlanViewModel();

            plan2.PlanID = 2;
            plan2.Title = "VIP SAVINGS";
            plan2.InsuranceCompany = "Coventry";
            plan2.Logo = "Coventry.jpg";

            PlanViewModel plan3 = new PlanViewModel();

            plan3.PlanID = 3;
            plan3.Title = "VIP SAVINGS COPD";
            plan3.InsuranceCompany = "Coventry";
            plan3.Logo = "Coventry.jpg";
            Plans.Add(plan1);
            Plans.Add(plan2);
            Plans.Add(plan3);

            return Json(Plans, JsonRequestBehavior.AllowGet);
        }
    }
}