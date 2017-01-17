using AHC.CD.Business;
using AHC.CD.Entities.MasterProfile.WorkHistory;
//using AHC.CD.Entities.MasterProfile.ServiceInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class ProfileController : Controller
    {
        private IProfileManager profileManager = null;

        public ProfileController(IProfileManager profileManager)
        {
            this.profileManager = profileManager;
        }
        
        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }
        //VIEW: Profile
        public ActionResult ViewProfile()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddProfessionalAffiliation(AHC.CD.Entities.MasterProfile.ProfessionalAffiliation.ProfessionalAffiliationInfo professionalAffiliation)
        {
            string returnMessage = "";

            if (ModelState.IsValid)
            {
                returnMessage = "true";
            }
            else
            {
                returnMessage = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }

            return Json(returnMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateProfessionalAffiliation(AHC.CD.Entities.MasterProfile.ProfessionalAffiliation.ProfessionalAffiliationInfo professionalAffiliation)
        {
            string returnMessage = "";

            if (ModelState.IsValid)
            {
                returnMessage = "true";
            }
            else
            {
                returnMessage = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }

            return Json(returnMessage, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveWorkGapInfo(WorkGap WorkGap)
        {
            string response = "";

            if (ModelState.IsValid)
            {
                // talk to buss. layer here

                response = "success";
            }
            else
            {
                response = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage)); 
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }

        
    }
}