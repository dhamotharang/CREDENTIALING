using AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class IdentificationAndLicenseController : Controller
    {
        [HttpPost]
        public ActionResult AddStateLicense(AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.StateLicenseViewModel stateLicense)
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
        public ActionResult UpdateStateLicense(AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.StateLicenseViewModel stateLicense)
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
        public ActionResult AddFederalDEALicense(AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.FederalDEAInformationViewModel federalDEAInformation)
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
        public ActionResult UpdateFederalDEALicense(AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.FederalDEAInformationViewModel federalDEAInformation)
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
        public ActionResult AddCDSCInformation(AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.CDSCInformationViewModel cDSCInformation)
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
        public ActionResult UpdateCDSCInformation(AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.CDSCInformationViewModel cDSCInformation)
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
        public ActionResult AddMedicareInformation(AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicareAndMedicaidViewModel medicareInformation)
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
        public ActionResult UpdateMedicareInformation(AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicareAndMedicaidViewModel medicareInformation)
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
        public ActionResult AddMedicaidInformation(AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicareAndMedicaidViewModel medicaidInformation)
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
        public ActionResult UpdateMedicaidInformation(AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.MedicareAndMedicaidViewModel medicaidInformation)
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
        public ActionResult UpdateOtherIdentificationNumber(AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses.OtherIdentificationNumberViewModel otherIdentificationNumber)
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

        public JsonResult GetScheduleData()
        {
            DEAScheduleInfoViewModel DEAScheduleInfoViewModel = new DEAScheduleInfoViewModel();

            DEAScheduleViewModel DEAScheduleViewModel = new DEAScheduleViewModel();

            ICollection<DEAScheduleTypeViewModel> DEAScheduleTypeViewModel = new List<DEAScheduleTypeViewModel>();


            DEAScheduleInfoViewModel.DEASchedule = DEAScheduleViewModel;

            DEAScheduleInfoViewModel.DEAScheduleTypes = DEAScheduleTypeViewModel;

            return Json(DEAScheduleInfoViewModel,JsonRequestBehavior.AllowGet);
        }

    }
}