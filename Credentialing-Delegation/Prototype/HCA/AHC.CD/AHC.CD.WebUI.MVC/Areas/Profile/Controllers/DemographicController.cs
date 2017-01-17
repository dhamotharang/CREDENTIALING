using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class DemographicController : Controller
    {
        // GET: Profile/Demographic
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UpdatePersonalDetails(PersonalDetailViewModel personalDetails)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                status = true.ToString();
            }
            else
            {
                status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }
            return Json(new { status = status, personalDetails = personalDetails }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdatePersonalIdentification(PersonalIdentificationViewModel personalIdentification)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                status = true.ToString();
            }
            else
            {
                status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }
            return Json(new { status = status, personalIdentification = personalIdentification }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddOtherLegalName(OtherLegalNameViewModel otherLegalName)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                status = true.ToString();
            }
            else
            {
                status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }
            return Json(new { status = status, otherLegalName = otherLegalName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateOtherLegalName(OtherLegalNameViewModel otherLegalName)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                status = true.ToString();
            }
            else
            {
                status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }
            return Json(new { status = status, otherLegalName = otherLegalName }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddHomeAddress(HomeAddressViewModel homeAddress)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                status = true.ToString();
            }
            else
            {
                status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }
            return Json(new { status = status, homeAddress = homeAddress }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateHomeAddress(HomeAddressViewModel homeAddress)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                status = true.ToString();
            }
            else
            {
                status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }
            return Json(new { status = status, homeAddress = homeAddress }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateContactDetails(ContactDetailViewModel contactDetails)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                status = true.ToString();
            }
            else
            {
                status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }
            return Json(new { status = status, contactDetails = contactDetails }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveBirthInformation(BirthInformationViewModel birthInformation)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                status = true.ToString();
            }
            else
            {
                status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }
            return Json(new { status = status, birthInformation = birthInformation }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveEthnicityVisaDetail(VisaDetailViewModel visaDetail)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                status = true.ToString();
            }
            else
            {
                status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }
            return Json(new { status = status, visaDetail = visaDetail }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveEthnicityLanguages(LanguageInfoViewModel languageInfo)
        {
            string status = "";
            if (ModelState.IsValid)
            {
                status = true.ToString();
            }
            else
            {
                status = String.Join(", ", ModelState.Values.SelectMany(m => m.Errors).Select(e => e.ErrorMessage));
            }
            return Json(new { status = status, languageInfo = languageInfo }, JsonRequestBehavior.AllowGet);
        }
    }
}