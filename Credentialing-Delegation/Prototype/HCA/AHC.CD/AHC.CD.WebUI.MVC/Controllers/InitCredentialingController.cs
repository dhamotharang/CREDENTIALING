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
      
     
        public JsonResult GetAllProviders(CredentialingInitiationViewModel tempObject)
        {

            List<CredentialingInitiationViewModel> ProvidersList = new List<CredentialingInitiationViewModel>();

            CredentialingInitiationViewModel CredentialingInitiationViewModel1 = new CredentialingInitiationViewModel();

            CredentialingInitiationViewModel1.FirstName = "Pariksith";
            CredentialingInitiationViewModel1.MiddleName = "";
            CredentialingInitiationViewModel1.LastName = "Singh";
            CredentialingInitiationViewModel1.Image = "Pariksith_Singh.jpg";
            CredentialingInitiationViewModel1.Title = "MD";
            CredentialingInitiationViewModel1.NPI = "1417989625";
            CredentialingInitiationViewModel1.CAQH = "10721240";
            CredentialingInitiationViewModel1.Type = "Medical Doctor (MD)";
            CredentialingInitiationViewModel1.CredType = "Credentialing";
            CredentialingInitiationViewModel1.CredDate = Convert.ToDateTime("05-03-2014");





            CredentialingInitiationViewModel1.Groups.Add("American Medical Student Association");
            CredentialingInitiationViewModel1.Specialities.Add("Internal Medicine");
            CredentialingInitiationViewModel1.Plan.Add("Freedom HMO");            

            PlanViewModel plan1 = new PlanViewModel();

            plan1.PlanID = 1;
            plan1.Title = "plan1";
            plan1.InsuranceCompany = "Freedom";
            plan1.Logo = "Freedom.jpg";

            PlanCredentialingViewModel PlanCredentialingViewModel1 = new PlanCredentialingViewModel();
            PlanCredentialingViewModel1.Status = "Initiated";
            PlanCredentialingViewModel1.Type = "Independent";
            PlanCredentialingViewModel1.Speciality = "MO";
            PlanCredentialingViewModel1.GroupName = "N/A";
            PlanCredentialingViewModel1.Location = "LocationDetails";
            PlanCredentialingViewModel1.Plan = plan1;


            //CredentialingInitiationViewModel1.relatedPlans.Add(PlanCredentialingViewModel1);




            CredentialingInitiationViewModel CredentialingInitiationViewModel2 = new CredentialingInitiationViewModel();

            CredentialingInitiationViewModel2.FirstName = "Mc.";
            CredentialingInitiationViewModel2.MiddleName = "";
            CredentialingInitiationViewModel2.LastName = "Angellica";
            CredentialingInitiationViewModel2.Image = "provider2.jpg";
            CredentialingInitiationViewModel2.Title = "PO";
            CredentialingInitiationViewModel2.NPI = "17231126633";
            CredentialingInitiationViewModel2.CAQH = "12321234";
            CredentialingInitiationViewModel2.Type = "DC";
            CredentialingInitiationViewModel2.Groups.Add("Medicare Rights Center");
            CredentialingInitiationViewModel2.CredType = "Re-Credentialing";
            CredentialingInitiationViewModel2.CredDate = Convert.ToDateTime("05-03-2013");
            CredentialingInitiationViewModel2.Specialities.Add("PO");
            CredentialingInitiationViewModel2.Plan.Add("Freedom HMO");            

            PlanViewModel plan2 = new PlanViewModel();

            plan2.PlanID = 2;
            plan2.Title = "plan2";
            plan2.InsuranceCompany = "Coventry";
            plan2.Logo = "Coventry.jpg";

            PlanCredentialingViewModel PlanCredentialingViewModel2 = new PlanCredentialingViewModel();
            PlanCredentialingViewModel2.Status = "Credentialed";
            PlanCredentialingViewModel2.Type = "Group";
            PlanCredentialingViewModel2.Speciality = "MO";
            PlanCredentialingViewModel2.GroupName = "Group1";
            PlanCredentialingViewModel2.Location = "LocationDetails";
            PlanCredentialingViewModel2.Plan = plan2;


          //  CredentialingInitiationViewModel2.relatedPlans.Add(PlanCredentialingViewModel2);


            CredentialingInitiationViewModel CredentialingInitiationViewModel3 = new CredentialingInitiationViewModel();

            CredentialingInitiationViewModel3.FirstName = "Sachin";
            CredentialingInitiationViewModel3.MiddleName = "";
            CredentialingInitiationViewModel3.LastName = "Sharma";
            CredentialingInitiationViewModel3.Image = "provider3.jpg";
            CredentialingInitiationViewModel3.Title = "PO";
            CredentialingInitiationViewModel3.NPI = "37233323438";
            CredentialingInitiationViewModel3.CAQH = "12312312";
            CredentialingInitiationViewModel3.Type = "DDS";
            CredentialingInitiationViewModel3.CredType = "Credentialing";
            CredentialingInitiationViewModel3.CredDate = Convert.ToDateTime("02-03-2014");
            CredentialingInitiationViewModel3.Specialities.Add("MO");
            CredentialingInitiationViewModel3.Groups.Add("California Nurses");
            CredentialingInitiationViewModel3.Specialities.Add("PO");
            CredentialingInitiationViewModel3.Plan.Add("Wellcare");            

            PlanViewModel plan3 = new PlanViewModel();

            plan3.PlanID = 3;
            plan3.Title = "plan3";
            plan3.InsuranceCompany = "Coventry";
            plan3.Logo = "Coventry.jpg";

            PlanCredentialingViewModel PlanCredentialingViewModel3 = new PlanCredentialingViewModel();
            PlanCredentialingViewModel3.Status = "Credentialed";
            PlanCredentialingViewModel3.Type = "Group";
            PlanCredentialingViewModel3.Speciality = "MO";
            PlanCredentialingViewModel3.GroupName = "Group1";
            PlanCredentialingViewModel3.Location = "LocationDetails";
            PlanCredentialingViewModel3.Plan = plan3;


           // CredentialingInitiationViewModel3.relatedPlans.Add(PlanCredentialingViewModel3);


           
            ProvidersList.Add(CredentialingInitiationViewModel1);
            ProvidersList.Add(CredentialingInitiationViewModel2);
            ProvidersList.Add(CredentialingInitiationViewModel3);
           
        


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