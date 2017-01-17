using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfessionalReferenceController : Controller
    {
        // GET: Profile/ProfessionalReference
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult AddProfessionalReference(AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference.ProfessionalReferenceViewModel professionalReference)
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
        public ActionResult UpdateProfessionalReference(AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalReference.ProfessionalReferenceViewModel professionalReference)
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
        
    }
}