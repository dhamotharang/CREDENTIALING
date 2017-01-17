using AHC.CD.Business;
using AHC.CD.Business.Profiles;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.MasterProfile;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace AHC.CD.WebApi.Controllers
{
    public class ProviderServiceController : ApiController
    {
        private readonly IProfileServiceManager profiles =null ;
        private readonly IProfileManager profileManager = null;

        public ProviderServiceController(IProfileServiceManager profiles, IProfileManager profilemanager)
        {
            this.profiles = profiles;
            this.profileManager = profilemanager;
        }
        [HttpGet]
        public List<ProviderDTO> Index()
        {
            List<ProviderDTO> providers = profiles.GetAllProviders();
            return providers;

        }

        [HttpGet]
        public ProviderServiceDTO Index2(string NPINumber)
        {
            ProviderServiceDTO practiceLoactionDetails = profiles.GetAllProvidersDataByNPI(NPINumber);
            return practiceLoactionDetails;

        }
       [HttpGet]
        public async Task<object> GetProviderEducationByNPI(string NPI)
        {
            
             int profileId = profiles.GetProfileIDByNPI(NPI);
            return await profileManager.GetEducationHistoriesProfileDataAsync(profileId);
        }
        [HttpGet]
        public async Task<object> GetProviderDemographicsByNPI(string NPI)
        {
            int profileId = profiles.GetProfileIDByNPI(NPI);
            return await profileManager.GetDemographicsProfileDataAsync(profileId);
        }
        [HttpGet]
        public async Task<object> GetProviderLicensesByNPI(string NPI)
        {
            int profileId = profiles.GetProfileIDByNPI(NPI);
            return await profileManager.GetIdentificationAndLicensesProfileDataAsync(profileId);
        }
        [HttpGet]
        public async Task<object> GetProviderSpecialtyByNPI(string NPI)
        {
            int profileId = profiles.GetProfileIDByNPI(NPI);
            var temp = await profileManager.GetBoardSpecialtiesProfileDataAsync(profileId);
            return temp;
        }
        [HttpGet]
        public async Task<object> GetProviderPracticeLocationByNPI(string NPI)
        {
            int profileId = profiles.GetProfileIDByNPI(NPI);
            return await profileManager.GetPracticeLocationsProfileDataAsync(profileId);
        }
        [HttpGet]
        public async Task<object> GetProviderHospitalPrivilegesByNPI(string NPI)
        {
            int profileId = profiles.GetProfileIDByNPI(NPI);
            return await profileManager.GetHospitalPrivilegesProfileDataAsync(profileId);
        }
        [HttpGet]
        public async Task<object> GetProviderLiabilityByNPI(string NPI)
        {
            int profileId = profiles.GetProfileIDByNPI(NPI);
            return await profileManager.GetProfessionalLiabilitiesProfileDataAsync(profileId);
        }
        [HttpGet]
        public async Task<object> GetProviderWorkHistoryByNPI(string NPI)
        {
            int profileId = profiles.GetProfileIDByNPI(NPI);
            return await profileManager.GetWorkHistoriesProfileDataAsync(profileId);
        }
        [HttpGet]
        public async Task<object> GetProviderReferenceByNPI(string NPI)
        {
            int profileId = profiles.GetProfileIDByNPI(NPI);
            return await profileManager.GetProfessionalReferencesProfileDataAsync(profileId);
        }
        [HttpGet]
        public async Task<object> GetProviderAffiliationByNPI(string NPI)
        {
            int profileId = profiles.GetProfileIDByNPI(NPI);
            return await profileManager.GetProfessionalAffiliationsProfileDataAsync(profileId);
        }
        [HttpGet]
        public async Task<object> GetProviderDisclosureQuestionsByNPI(string NPI)
        {
            int profileId = profiles.GetProfileIDByNPI(NPI);
            return await profileManager.GetDisclosureQuestionsProfileDataAsync(profileId);
        }
        [HttpGet]
        public async Task<object> GetProviderContractByNPI(string NPI)
        {
            int profileId = profiles.GetProfileIDByNPI(NPI);
            return await profileManager.GetContractInfoProfileDataAsync(profileId);
        }
        [HttpGet]
        public async Task<object> GetProviderBriefProfileByNPI(string NPI)
        {
            //int profileId = profiles.GetProfileIDByNPI(NPI);
            //return Ok(await profiles.GetProviderBriefProfileByNPI(NPI));
            return await profiles.GetProviderBriefProfileByNPI(NPI);
        }
    }
}
