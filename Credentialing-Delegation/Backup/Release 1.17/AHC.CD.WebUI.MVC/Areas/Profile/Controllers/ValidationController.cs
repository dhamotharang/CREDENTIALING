using AHC.CD.Business.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ValidationController : Controller
    {
        private IProfileDataDuplicateManager ProfileDataDuplicateManager { get; set; }

        public ValidationController(IProfileDataDuplicateManager profileDataDuplicateManager)
        {
            this.ProfileDataDuplicateManager = profileDataDuplicateManager;
        }

        public JsonResult IsContactNumberDoesNotExists(string Number, string CountryCode, string PhoneDetailID)
        {
            return Json(ProfileDataDuplicateManager.IsContactNumberDoesNotExists(String.Join("-", CountryCode, Number), Convert.ToInt32(PhoneDetailID)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsEmailAddressDoesNotExists(string EmailAddress, string EmailDetailID)
        {
            return Json( ProfileDataDuplicateManager.IsEmailAddressDoesNotExists(EmailAddress, Convert.ToInt32(EmailDetailID)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNPINumberDoesNotExists(string NPINumber, string profileId = null)
        {
            return Json( ProfileDataDuplicateManager.IsNPINumberDoesNotExists(NPINumber, Convert.ToInt32(profileId)), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCAQHNumberDoesNotExists(string CAQHNumber, int profileId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsCAQHNumberDoesNotExists(CAQHNumber, profileId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNPIUsernameDoesNotExists(string NPIUserName, int profileId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsNPIUsernameDoesNotExists(NPIUserName, profileId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCAQHUsernameDoesNotExists(string CAQHUserName, int profileId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsCAQHUsernameDoesNotExists(CAQHUserName, profileId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsIndividualTaxIDDoesNotExists(string IndividualTaxId, string ContractInfoID = null)
        {
            return Json(ProfileDataDuplicateManager.IsIndividualTaxIDDoesNotExists(IndividualTaxId, Convert.ToInt32(ContractInfoID)), JsonRequestBehavior.AllowGet);
        }



        public JsonResult IsDLNumberDoesNotExists(string licenseNumber, int profileId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsDLNumberDoesNotExists(licenseNumber, profileId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsSSNumberDoesNotExists(string ssNumber, int profileId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsSSNumberDoesNotExists(ssNumber, profileId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsVisaNumberDoesNotExists(string number, int profileId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsVisaNumberDoesNotExists(number, profileId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsGreenCardNumberDoesNotExists(string number, int profileId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsGreenCardNumberDoesNotExists(number, profileId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNationalIDNumberDoesNotExists(string number, int profileId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsNationalIDNumberDoesNotExists(number, profileId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsStateLicenseNumberDoesNotExists(string number, int stateLicenseInformationId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsStateLicenseNumberDoesNotExists(number, stateLicenseInformationId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsFederalDEANumberDoesNotExists(string number, int federalDEAInformationId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsFederalDEANumberDoesNotExists(number, federalDEAInformationId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCDSCLicenseNumberDoesNotExists(string number, int cdscInformationId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsCDSCLicenseNumberDoesNotExists(number, cdscInformationId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsMedicareLicenseNumberDoesNotExists(string number, int medicareInformationId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsMedicareLicenseNumberDoesNotExists(number, medicareInformationId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsMedicaidLicenseNumberDoesNotExists(string number, int medicaidInformationId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsMedicaidLicenseNumberDoesNotExists(number, medicaidInformationId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsUPINNumberDoesNotExists(string number, int profileId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsUPINNumberDoesNotExists(number, profileId), JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsUSMLENumberDoesNotExists(string number, int profileId = 0)
        {
            return Json( ProfileDataDuplicateManager.IsUSMLENumberDoesNotExists(number, profileId), JsonRequestBehavior.AllowGet);
        }
    }
}