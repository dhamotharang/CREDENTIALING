using AHC.CD.Business;
using AHC.CD.Business.MasterData;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class MasterDataController : Controller
    {
        // GET: Profile/MasterData

        private IMasterDataManager masterDataManager=null;

        public MasterDataController(IMasterDataManager IMasterDataManager)
        {
            this.masterDataManager = IMasterDataManager;
        }

        
        public async Task<JsonResult> GetAllLicenseStatus()
        {
            var data = await masterDataManager.GetAllLicenseStatusAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllStaffCategories()
        {
            var data = await masterDataManager.GetAllStaffCategoryAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllspecialtyBoards()
        {
            var data = await masterDataManager.GetAllspecialtyBoardAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllSpecialities()
        {
            var data = await masterDataManager.GetAllSpecialtyAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllSchools()
        {
            var data = await masterDataManager.GetAllSchoolAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllQualificationDegrees()
        {
            var data = await masterDataManager.GetAllQualificationDegreeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllProviderTypes()
        {
            var data = await masterDataManager.GetAllProviderTypeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllProfileDisclosureQuestions()
        {
            var data = await masterDataManager.GetAllProfileDisclosureQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllPracticeTypes()
        {
            var data = await masterDataManager.GetAllPracticeTypeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllPracticeServiceQuestions()
        {
            var data = await masterDataManager.GetAllPracticeServiceQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllPracticeOpenStatusQuestions()
        {
            var data = await masterDataManager.GetAllPracticeOpenStatusQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllPracticeAccessibilityQuestions()
        {
            var data = await masterDataManager.GetAllPracticeAccessibilityQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllMilitaryRanks()
        {
            var data = await masterDataManager.GetAllMilitaryRankAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllMilitaryPresentDuties()
        {
            var data = await masterDataManager.GetAllMilitaryPresentDutyAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllMilitaryDischarges()
        {
            var data = await masterDataManager.GetAllMilitaryDischargeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllMilitaryBranches()
        {
            var data = await masterDataManager.GetAllMilitaryBranchAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllInsuranceCarriers()
        {
            var data = await masterDataManager.GetAllInsuranceCarrierAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllInsuranceCarrierAddresses()
        {
            var data = await masterDataManager.GetAllInsuranceCarrierAddressesAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllHospitalContactPersons()
        {
            var data = await masterDataManager.GetAllHospitalContactPersonAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllHospitalContactInfoes()
        {
            var data = await masterDataManager.GetAllHospitalContactInfoAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllHospitals()
        {
            var data = await masterDataManager.GetAllHospitalAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllDEASchedules()
        {
            var data = await masterDataManager.GetAllDEAScheduleAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllCertificates()
        {
            var data = await masterDataManager.GetAllCertificationAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllAdmittingPrivileges()
        {
            var data = await masterDataManager.GetAllAdmittingPrivilegeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllVisaTypes()
        {
            var data = await masterDataManager.GetAllVisaTypeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetAllVisaStatus()
        {
            var data = await masterDataManager.GetAllVisaStatusAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "ADM")]
        public async Task<string> InitMasterData()
        {
            string info = "Master Data Initialized";

            new AHC.CD.Data.EFRepository.MasterDataDBInitializer().Seed();

            return info;
        }

    }
}