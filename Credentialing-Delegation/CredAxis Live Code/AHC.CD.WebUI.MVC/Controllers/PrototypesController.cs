using AHC.CD.WebUI.MVC.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class PrototypesController : Controller
    {
        //
        // GET: /Prototypes/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CCOSummary()
        {
            return View();
        }
        public ActionResult ProviderSummary()
        {
            return View();
        }
        public ActionResult CCOAssignment()
        {
            return View();
        }
        public ActionResult CCMDashboard()
        {
            return View();
        }
        public ActionResult CCOTLAssignment()
        {
            return View();
        }
        public ActionResult CommitteeReport()
        {
            return View();
        }

        /// <summary>
        /// Provider Directory View Action Method
        /// </summary>
        /// <returns>cshtml viewpage</returns>
        public ActionResult ProviderDirectory()
        {
            return View();
        }

        /// <summary>
        /// Get Provider Json Data
        /// </summary>
        /// <returns>Json, Provider Object List</returns>
        public JsonResult GetProviders(int count = 10)
        {
            var data = PrototypeHelper.GetProviders(count);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Providers Summary Status Data
        /// </summary>
        /// <param name="profileStatus"></param>
        /// <param name="count"></param>
        /// <returns>JSON, Provider Object</returns>
        public JsonResult GetProvidersSummary(string profileStatus, int count = 10)
        {
            var data = PrototypeHelper.GetProvidersSummary(count, profileStatus);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get Contact Json Data
        /// </summary>
        /// <returns>Json, Contact Object List</returns>
        public JsonResult GetSpecialties()
        {
            string[] specialties1 = { "FAMILY MEDICINE GERIATRIC MEDICINE", "BOARD CERTIFIED IN NEUROLOGY & PSYCHIATRY", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "BOARD CERTIFIED IN INTERNAL MEDICINE", "FAMILY MEDICINE", "FELLOW OF THE AMERICAN COLLEGE OF SURGEONS", "PHYSICAL MEDICINE & REHABILITATION", "BOARD CERTIFIED IN ALLERGY, ASTHMA & IMMUNOLOGY", "SERVED AS CHIEF OF STAFF AT BROOKSVILLE REGIONAL HOSPITAL", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "PRIMARY CARE", "SPORTS MEDICINE PHYSICIAN", "FAMILY MEDICINE GERIATRIC MEDICINE", "BOARD CERTIFIED BY THE AMERICAN BOARD OF PSYCHIATRY & NEUROLOGY", "INTERNAL MEDICINE", "BOARD CERTIFIED – FAMILY MEDICINE", "BOARD CERTIFIED – FAMILY MEDICINE", "GENERAL PRACTITIONER", "INTERNAL MEDICINE", "Board Certified in Internal Medicine & Geriatric Medicine", "INTERNAL MEDICINE", "ORTHOPEDICS - SPORTS MEDICINE PHYSICIAN", "Internal Medicine", "BOARD CERTIFIED IN FAMILY MEDICINE", "GENERAL PRACTICE", "INTERNAL MEDICINE", "Radiologist", "Family Medicine Geriatric Medicine", "Board Certified in Family Medicine", "Family Medicine", "BOARD CERTIFIED IN ANESTHESIOLOGY", "HOSPITALIST", "Family Medicine", "Board Certified in Family Medicine", "Internal Medicine", "FAMILY MEDICINE", "BOARD CERTIFIED IN PULMONARY MEDICINE", "INTERNAL MEDICINE", "Family Medicine Geriatric Medicine", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "BOARD CERTIFIED IN INTERNAL MEDICINE", "BOARD CERTIFIED HOSPITALIST", "SPORTS MEDICINE", "BOARD CERTIFIED IN FAMILY PRACTICE", "MEMBER OF THE ROYAL COLLEGE OF PHYSICIANS, UK", "PODIATRIC PHYSICIAN AND SURGEON", "Internal Medicine", "INTERNAL MEDICINE and Infectious Disease", "BOARD CERTIFIED – FAMILY MEDICINE", "PHYSICAL THERAPIST", "HOSPITALIST", "Board Certified in Internal Medicine", "ORTHOPEDICS - SURGEON", "FAMILY MEDICINE", "BOARD CERTIFIED IN INTERNAL MEDICINE AND GASTROENTEROLOGY", "FELLOW OF THE AMERICAN COLLEGE OF RHEUMATOLOGY", "FAMILY MEDICINE", "SPECIALTY IN OTOLARYNGOLOGY", "BOARD CERTIFIED IN INTERNAL MEDICINE", "BOARD CERTIFIED IN CARDIOVASCULAR DISEASE", "INTERNAL MEDICINE", "ORTHOPEDICS - SURGEON", "CLINICAL DIETITIAN/NUTRITION SPECIALIST", "GENERAL AND VASCULAR SURGEON", "HOSPITALIST", "FAMILY MEDICINE", "BOARD CERTIFIED IN GENERAL SURGERY", "Internal Medicine", "PODIATRIC MEDICINE", "GENERAL MEDICINE", "BOARD CERTIFIED IN GENERAL SURGERY", "FAMILY MEDICINE", "CARDIOVASCULAR DISEASE", "BOARD CERTIFIED IN INTERNAL MEDICINE", "SPECIALTY IN INTERVENTIONAL CARDIOLOGY", "FAMILY MEDICINE", "Family Medicine", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "SPECIALTY IN INFECTIOUS DISEASES", "PULMONARY/CRITICAL CARE PHYSICIAN", "PRIMARY CARE", "Internal Medicine", "Internal Medicine", "FAMILY MEDICINE", "INTERNAL MEDICINE & INFECTIOUS DISEASE", "Internal Medicine", "HOSPITALIST", "LICENSED PEDORTHIST & ORTHOTIC FITTER", "FAMILY MEDICINE", "FAMILY PRACTICE (PRIMARILY SEES ADULTS)", "BOARD CERTIFIED IN ORTHOPEDICS", "INTERNAL MEDICINE", "Family Medicine", "BOARD CERTIFIED IN FAMILY MEDICINE", "INTERNAL MEDICINE", "INTERNAL MEDICINE", "HOSPITALIST & INTERNAL MEDICINE", "HOSPITALIST & INTERNAL MEDICINE", "BOARD CERTIFIED IN PSYCHIATRY & NEUROLOGY", "CHIROPRACTIC MEDICINE", "RHEUMATOLOGIST" };
            var speciality = specialties1.ToList().Distinct();
            return Json(speciality, JsonRequestBehavior.AllowGet);
        }
    }
}