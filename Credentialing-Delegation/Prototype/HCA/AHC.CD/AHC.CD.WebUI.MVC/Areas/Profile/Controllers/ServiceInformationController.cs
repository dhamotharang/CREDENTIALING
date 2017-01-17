using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ServiceInformationController : Controller
    {
        // GET: Profile/ServiceInformation
        public ActionResult Index()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult AddMilitaryServiceInformation(int profileId, AHC.CD.Entities.MasterProfile.ServiceInformation.MilitaryServiceInformation militaryServiceInformation)
        //{
        //    string returnMessage = "";

        //    if (ModelState.IsValid)
        //    {
        //        returnMessage = "true";
        //    }
        //    else
        //    {
        //        returnMessage = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
        //    }

        //    return Json(returnMessage, JsonRequestBehavior.AllowGet);
        //}


        //[HttpPost]
        //public ActionResult UpdateMilitaryServiceInformation(int profileId, AHC.CD.Entities.MasterProfile.ServiceInformation.MilitaryServiceInformation militaryServiceInformation)
        //{
        //    string returnMessage = "";

        //    if (ModelState.IsValid)
        //    {
        //        returnMessage = "true";
        //    }
        //    else
        //    {
        //        returnMessage = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
        //    } 

        //    return Json(returnMessage, JsonRequestBehavior.AllowGet);
        //}



        //[HttpPost]
        //public ActionResult AddPublicHealthService(int profileId, AHC.CD.Entities.MasterProfile.ServiceInformation.PublicHealthService publicHealthService)
        //{
        //    string returnMessage = "";

        //    if (ModelState.IsValid)
        //    {
        //        returnMessage = "true";
        //    }
        //    else
        //    {
        //        returnMessage = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
        //    }

        //    return Json(returnMessage, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost]
        //public ActionResult UpdatePublicHealthService(int profileId, AHC.CD.Entities.MasterProfile.ServiceInformation.PublicHealthService publicHealthService)
        //{
        //    string returnMessage = "";

        //    if (ModelState.IsValid)
        //    {
        //        returnMessage = "true";
        //    }
        //    else
        //    {
        //        returnMessage = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
        //    }

        //    return Json(returnMessage, JsonRequestBehavior.AllowGet);
        //}

    }
}