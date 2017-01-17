using Newtonsoft.Json;
using PortalTemplate.Areas.MH.Common;
using PortalTemplate.Areas.MH.IServices;
using PortalTemplate.Areas.MH.Models.ViewModels.MasterData;
using PortalTemplate.Areas.MH.ServiceFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.MH.Services
{
    public class MasterDataService : IMasterDataService
    {
        ServiceLocator serviceLocator = new ServiceLocator();

        CommonMethods commonMethods = new CommonMethods();

        //Generic Method
        private string GenericServiceMethod<T>(string searchTerm, string serviceUrl, string property)
        {
            string serviceName = serviceLocator.Locate("CMS");
            Task<string> List = Task.Run(async () =>
            {
                string msg = await ExternalDataServiceRepository.GetDataFromServiceAsync(serviceName, serviceUrl);
                return msg;
            });
            List<T> data = JsonConvert.DeserializeObject<List<T>>(List.Result);
            data = commonMethods.Filter(data, searchTerm, property); //Generic Method for Data Filteration
            var filteredData = JsonConvert.SerializeObject(data);
            return filteredData;
        }

        public  string GetAllPlans(string searchTerm)
        {
            try
            {
                if (searchTerm == null) { searchTerm = ""; }
                string dataUrl = "~/Areas/MH/Resources/MasterData/AllPlans.JSON";
                string property = "PlanName";
                var data = commonMethods.GetMasterDataFromJson<PlanCategoryViewModel>(searchTerm, dataUrl, property);
                return data;
            }catch(Exception ex){
                throw ex;
            }
        }

        public string GetAllInsuranceCompanies(string searchTerm)
        {
            try
            {
                if (searchTerm == null) { searchTerm = ""; }
                string dataUrl = "~/Areas/MH/Resources/MasterData/AllInsuranceCompanies.JSON";
                string property = "CompanyName";
                var data = commonMethods.GetMasterDataFromJson<InsuranceCompanyViewModel>(searchTerm, dataUrl, property);
                //Task<InsuranceCompanyViewModel> memberList = Task.Run(async () =>
                //{
                //    InsuranceCompanyViewModel msg = await ServiceRepository.GetDataFromService<InsuranceCompanyViewModel>("api/FacilityService/GetAllFacilities?source=1");
                //    return msg;
                //});
              
                return data;
            }catch(Exception ex){
                throw ex;
            }
        }

        public string GetAllCities(string searchTerm)
        {
            string url = "api/Common/GetAllCityDetails";
            string searchProperty = "Name";
            string data = GenericServiceMethod<CityViewModel>(searchTerm, url, searchProperty);
            return data;
        }

        public string GetAllStates(string searchTerm)
        {
            string url = "api/Common/GetAllStates";
            string searchProperty = "Name";
            string data = GenericServiceMethod<StateViewModel>(searchTerm, url, searchProperty);
            return data;
        }

        public string GetAllCounties(string searchTerm) {
            string url = "api/Common/GetAllCounties";
            string searchProperty = "Name";
            string data = GenericServiceMethod<CountyViewModel>(searchTerm, url, searchProperty);
            return data;
        }

        public string GetAllCountries(string searchTerm)
        {
            string url = "api/Common/GetAllCountries";
            string searchProperty = "Name";
            string data = GenericServiceMethod<CountryViewModel>(searchTerm, url, searchProperty);
            return data;
        }

        public string GetAllPatientRelationships(string searchTerm)
        {
            string url = "api/Common/GetAllPatientRelation";
            string searchProperty = "PatientRealtionType";
            string data = GenericServiceMethod<PatientRelationshipViewModel>(searchTerm, url, searchProperty);
            return data;
        }

        

        public string GetAllCitiesByStateCode() { return null; }
        public string GetAllCountiesByStateCode() { return null; }
        public string GetAllSources() { return null; }
        public string GetSourceBySourceCode() { return null; }
        public string GetAllBankAccountTypes() { return null; }
        public string GetAllBanks() { return null; }
        public string GetAllDeductionsTypes() { return null; }
        public string GetAllDiseaseCategories() { return null; }
        public string GetAllDiseases() { return null; }
        public string GetAllDocumentCategories() { return null; }
        public string GetAllEducationCourses() { return null; }
        public string GetAllEducationInstitutions() { return null; }
        public string GetAllEmploymentTypes() { return null; }
        public string GetAllEmployers() { return null; }
        public string GetAllIdentificationTypes() { return null; }
        public string GetAllIncomeSources() { return null; }
        public string GetAllLanguages() { return null; }
        public string GetAllLobs() { return null; }
        public string GetLobDetailsByLobId() { return null; }
        public string GetAllInsuranceCompanies() { return null; }
        public string GetAllPlansByInsuranceCompanyCode() { return null; }
        public string GetAllPbps() { return null; }
        public string GetPlanDetailsByPbpId() { return null; }
        public string GetAllNoteCategories() { return null; }
        public string GetAllAttachmentCategory() { return null; }
        public string GetAllEthnicities() { return null; }
        public string GetAllRaces() { return null; }
        public string GetAllReligions() { return null; }
        public string GetAllPremiumTypes() { return null; }
        public string GetAllIpas() { return null; }
        public string GetAllFacities() { return null; }
        public string GetFacilityDetailsByFacilityCode() { return null; }

        //private string GetJSONData(string SourceFile)
        //{
        //    string file = HostingEnvironment.MapPath(GetResourceLink(SourceFile));
        //    string json = System.IO.File.ReadAllText(file);
        //    return json;
        //}

        //private string GetResourceLink(string SourceFileName)
        //{
        //    return "~/Areas/MH/Resources/JSONData/MasterData/" + SourceFileName;
        //}
    }
}