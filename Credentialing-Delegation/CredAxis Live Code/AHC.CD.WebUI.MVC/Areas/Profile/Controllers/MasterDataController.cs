using AHC.CD.Business;
using AHC.CD.Business.Account;
using AHC.CD.Business.DTO;
using AHC.CD.Business.MasterData;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Exceptions;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using Newtonsoft.Json;
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

        IOrganizationManager organizationManager = null;
        private IProfileManager profileManager = null;

        public MasterDataController(IMasterDataManager IMasterDataManager, IOrganizationManager organizationManager, IProfileManager profileManager)
        {
            this.masterDataManager = IMasterDataManager;
            this.organizationManager = organizationManager;
            this.profileManager = profileManager;
        }

        public async Task<List<int?>> GetAllProfileIDsFromCredentialingInfoIDs(int[] ProviderIDArray)
        {
            List<int?> data = await masterDataManager.GetAllProfileIDsFromCredentialingInfoIDsAsync(ProviderIDArray);
            return data;
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllLicenseStatus()
        {
            var data = await masterDataManager.GetAllLicenseStatusAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllStaffCategories()
        {
            var data = await masterDataManager.GetAllStaffCategoryAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllspecialtyBoards()
        {
            var data = await masterDataManager.GetAllspecialtyBoardAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllSpecialities()
        {
            var data = await masterDataManager.GetAllSpecialtyAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllSchools()
        {
            var data = await masterDataManager.GetAllSchoolAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllQualificationDegrees()
        {
            var data = await masterDataManager.GetAllQualificationDegreeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllProviderTypes()
        {
            var data = await masterDataManager.GetAllProviderTypeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllProfileDisclosureQuestions()
        {
            var data = await masterDataManager.GetAllProfileDisclosureQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllLoacationPracticeTypes()
        {
            var data = await masterDataManager.GetAllLocationPracticeTypeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllPracticeServiceQuestions()
        {
            var data = await masterDataManager.GetAllPracticeServiceQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllPracticeOpenStatusQuestions()
        {
            var data = await masterDataManager.GetAllPracticeOpenStatusQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllPracticeAccessibilityQuestions()
        {
            var data = await masterDataManager.GetAllPracticeAccessibilityQuestionAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllMilitaryRanks()
        {
            var data = await masterDataManager.GetAllMilitaryRankAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllMilitaryPresentDuties()
        {
            var data = await masterDataManager.GetAllMilitaryPresentDutyAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllMilitaryDischarges()
        {
            var data = await masterDataManager.GetAllMilitaryDischargeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllMilitaryBranches()
        {
            var data = await masterDataManager.GetAllMilitaryBranchAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllInsuranceCarriers()
        {
            var data = await masterDataManager.GetAllInsuranceCarrierAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllInsuranceCarrierAddresses()
        {
            var data = await masterDataManager.GetAllInsuranceCarrierAddressesAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllHospitalContactPersons()
        {
            var data = await masterDataManager.GetAllHospitalContactPersonAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllHospitalContactInfoes()
        {
            var data = await masterDataManager.GetAllHospitalContactInfoAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllHospitals()
        {
            var data = await masterDataManager.GetAllHospitalAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllDEASchedules()
        {
            var data = await masterDataManager.GetAllDEAScheduleAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllCertificates()
        {
            var data = await masterDataManager.GetAllCertificationAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllAdmittingPrivileges()
        {
            var data = await masterDataManager.GetAllAdmittingPrivilegeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllVisaTypes()
        {
            var data = await masterDataManager.GetAllVisaTypeAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllInsuaranceCompanyNames()
        {
            var data = await masterDataManager.GetAllInsuaranceCompanyNameAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllVisaStatus()
        {
            var data = await masterDataManager.GetAllVisaStatusAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllQuestions()
        {
            var data = await masterDataManager.GetAllQuestionsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllQuestionCategories()
        {
            var data = await masterDataManager.GetAllQuestionCategoriesAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllProviderLevels()
        {
            var data = await masterDataManager.GetAllProviderLevelsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #region organization
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<JsonResult> GetAllOrganizationGroupAsync()
        {
            var data = await masterDataManager.GetAllOrganizationGroupAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<string> GetAllOrganizations()
        {
            var result = await organizationManager.GetAllOrganizationWithLocationDetailAsync();
            return JsonConvert.SerializeObject(result);
            //return Json(await organizationManager.GetAllOrganizationWithLocationDetailAsync(), JsonRequestBehavior.AllowGet);

        }

        #endregion

        #region Midlevels 
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData", VaryByParam= "organizationId")]
        public async Task<ActionResult> GetMidlevels(int organizationId)
        {
            return Json(await organizationManager.GetAllMidLevelsByOrgId(organizationId), JsonRequestBehavior.AllowGet);
        }

        #endregion
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllAccessibilityQuestions()
        {
            var data = await masterDataManager.GetAllAccessibilityQuestionsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllPracticeTypes()
        {
            var data = await masterDataManager.GetAllPracticeTypesAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllServiceQuestions()
        {
            var data = await masterDataManager.GetAllServiceQuestionsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllOpenPracticeStatusQuestions()
        {
            var data = await masterDataManager.GetAllOpenPracticeStatusQuestionsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllPaymentAndRemittance()
        {
            var data = await masterDataManager.GetAllPaymentAndRemittance();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllBusinessContactPerson()
        {
            var data = await masterDataManager.GetAllBusinessContactPersonAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllOrganizationGroups()
        {
            var data = await masterDataManager.GetAllOrganizationGroupsAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllBillingContact()
        {
            var data = await masterDataManager.GetAllBillingContactAsync();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        //public JsonResult GetInitialPracticeDays()
        //{
        //    var data = "["+
        //        "{ DayName: 'Sunday', DayOfWeek: 0, DailyHours: [{ StartTime: '00:00', EndTime: '00:00' }] },"+
        //        "{ DayName: 'Monday', DayOfWeek: 1, DailyHours: [{ StartTime: '00:00', EndTime: '00:00' }] },"+
        //        "{ DayName: 'Tuesday', DayOfWeek: 2, DailyHour': [{ StartTime: '00:00', EndTime: '00:00' }] },"+
        //        "{ DayName: 'Wednesday', DayOfWeek: 3, DailyHours: [{ StartTime: '00:00', EndTime: '00:00' }] },"+
        //        "{ DayName: 'Thursday', DayOfWeek: 4, DailyHours: [{ StartTime: '00:00', EndTime: '00:00' }] },"+
        //        "{ DayName: 'Friday', DayOfWeek: 5, DailyHours: [{ StartTime: '00:00', EndTime: '00:00' }] },"+
        //        "{ DayName: 'Saturday', DayOfWeek: 6, DailyHours: [{ StartTime: '00:00', EndTime: '00:00' }] }"+
        //    "]";

        //    //return Json(new JsonResult { }, JsonRequestBehavior.AllowGet);
        //}
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<string> GetAllFacilities()
        {
            IEnumerable<Organization> organizations =await organizationManager.GetAllOrganizationWithLocationDetailAsync();

            List<Facility> faclities=new List<Facility>();


            foreach (var org in organizations)
	        {
		        if(org.OrganizationID==OrganizationAccountId.DefaultOrganizationAccountID){
                   
                    foreach (var facility in org.Facilities)
	                {
		                faclities.Add(facility);
	                }
                }
	        }

            return JsonConvert.SerializeObject(faclities);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData", VaryByParam = "practitionerLevel;profileID")]
        public async Task<string> GetPractitionersByProviderLevel(string practitionerLevel, int profileID)
        {
            var data = await profileManager.GetAllProfileByProviderLevel(practitionerLevel, profileID);
            return JsonConvert.SerializeObject(data);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData", VaryByParam = "profileID")]
        public async Task<ActionResult> GetAllProviderLevelByProfileId(int profileID)
        {
            var data = await profileManager.GetAllProviderLevelByProfileId(profileID);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllCredentialingContact()
        {
            var data = await masterDataManager.GetAllCredentialingContactAsync();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllProfileSections()
        {
            var data = await masterDataManager.GetAllProfileSectionsAsync();    

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData", VaryByParam = "profileID")]
        public async Task<ActionResult> GetAllNotApplicableSectoin(int profileId)    
        {
            var data = await masterDataManager.GetAllNotApplicableSectoinAsync(profileId);

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #region PLAN
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllPlan()
        {
            var data = await masterDataManager.GetAllPlanAsync();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> GetAllPlans_ForBE(List<string> Groups)
        {
            var data = await masterDataManager.GetAllPlans_ForBEAsync(Groups);

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        #endregion

        #region LOB
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllLOB()
        {
            var data = await masterDataManager.GetAllLOBAsync();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region PSV
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllProfileVerificationParameter()
        {
            var data = await masterDataManager.GetAllProfileVerificationParameter();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Verification Links
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllVerificationLinks()
        {
            var data = await masterDataManager.GetAllVerificationLinks();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Credentialing Request
        //[OutputCache(Location = System.Web.UI.OutputCacheLocation.Server, CacheProfile = "MasterData")]
        public async Task<ActionResult> GetAllCredentialingRequest()    
        {
            var data = await masterDataManager.GetAllCredentialingRequestAsync();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}