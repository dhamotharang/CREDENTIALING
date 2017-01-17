using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using AHC.CD.Business.Profiles;
using Newtonsoft.Json;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class ProfileHistoryController : Controller
    {
        //
        // GET: /Profile/ProfileHistory/
        private IProfileHistoryManager profileHistoryManager = null;

        public ProfileHistoryController(IProfileHistoryManager profileHistoryManager)
        {
            this.profileHistoryManager = profileHistoryManager;
        }

        #region Professsional Affiliation

        public async Task<string> GetAllProfessionalAffiliationHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetProfessionalAffiliationHistory(profileId));
        }

        #endregion

        #region Demographics

        public async Task<string> GetAllOtherLegalNamesHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetAllOtherLegalNamesHistory(profileId));
        }

        public async Task<string> GetAllHomeAddressesHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetAllHomeAddressesHistory(profileId));
        }

        #endregion

        #region Professional Liability

        public async Task<string> GetAllProfessionalLiabilityInfoHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetAllProfessionalLiabilityInfoHistory(profileId));
        }

        #endregion

        #region Hospital Privileges

        public async Task<string> GetAllHospitalHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetAllHospitalHistory(profileId));

        }

        #endregion

        #region Professional Reference

        public async Task<string> GetAllProfessionalReferenceHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetProfessionalReferenceHistory(profileId));
        }

        #endregion

        #region Work History

        public async Task<string> GetAllMilitaryServiceInformationHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetMilitaryServiceInformationHistory(profileId));
        }

        public async Task<string> GetAllProfessionalWorkExperienceHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetProfessionalWorkExperienceHistory(profileId));
        }

        public async Task<string> GetAllPublicHealthServiceHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetPublicHealthServiceHistory(profileId));
        }

        public async Task<string> GetAllWorkGapHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetWorkGapHistory(profileId));
        }

        #endregion

        #region Identification & Licenses

        public async Task<string> GetAllStateLicensesHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetStateLicenseHistory(profileId));
        }

        public async Task<string> GetAllFederalDEALicensesHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetFederalDEALicensesHistory(profileId));
        }

        public async Task<string> GetAllMedicareInformationHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetMedicareInformationHistory(profileId));
        }

        public async Task<string> GetAllMedicaidInformationHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetMedicaidInformationHistory(profileId));
        }

        public async Task<string> GetAllCDSCInformationHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetCDSCInformationHistory(profileId));
        }

        #endregion

        #region Practice Location

        public async Task<string> GetAllPracticeLocationDetailHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetPracticeLocationDetailHistory(profileId));
        }

        #endregion

        #region Education History

        public async Task<string> GetAllEducationDetailHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetEducationDetailHistory(profileId));
        }

        public async Task<string> GetCMECertificationHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetCMECertificationHistory(profileId));
        }

        public async Task<string> GetProgramDetailHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetProgramDetailHistory(profileId));
        }

        #endregion

        #region Contact Information

        public async Task<string> GetAllContractGroupInfoHistory(int profileId)
        {
            return JsonConvert.SerializeObject(await profileHistoryManager.GetContractGroupInfoHistory(profileId));
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }
    }
}