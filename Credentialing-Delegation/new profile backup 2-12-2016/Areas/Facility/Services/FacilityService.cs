using Newtonsoft.Json;
using PortalTemplate.Areas.Billing.WebApi;
using PortalTemplate.Areas.Facility.Models;
using PortalTemplate.Areas.Facility.Models.MasterData;
using PortalTemplate.Areas.Facility.Services.IServices;
using PortalTemplate.Areas.MH.Common;
using PortalTemplate.Areas.MH.ServiceFacade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.Facility.Services
{
    public class FacilityService : IFacilityService
    {
        ServiceLocator serviceLocator = new ServiceLocator();
        private FacilityBridgeQueueViewModel _BridgeQueueData;

        public FacilityService()
        {
            _BridgeQueueData = new FacilityBridgeQueueViewModel();
        }
        public List<FacilityBridgeQueueViewModel> GetOpenedQueueData()
        {
            List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
            string queueData;
            try
            {
                queueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/OpenedBridgeQueue.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                }
                return BridgeQueueData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<FacilityBridgeQueueViewModel> GetAssignedQueueData()
        {
            List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
            string queueData;
            try
            {
                queueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/AssignedBridgeQueue.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                }
                return BridgeQueueData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public List<FacilityBridgeQueueViewModel> GetPendingQueueData()
        {
            List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
            string queueData;
            try
            {
                queueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/PendingBridgeQueue.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                }
                return BridgeQueueData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<FacilityBridgeQueueViewModel> GetAllFacilities()
        {
            FacilityBridgeQueueViewModel data = new FacilityBridgeQueueViewModel();
            List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
            string queueData;
            try
            {
                queueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/BridgeQueueTemp.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                }
                return BridgeQueueData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public FacilityBridgeQueueViewModel GetQueueData(string id)
        {
            FacilityBridgeQueueViewModel data = new FacilityBridgeQueueViewModel();
            List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
            string queueData;
            try
            {
                queueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/BridgeQueue.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                }
                foreach (var item in BridgeQueueData)
                {
                    if (item.FacilityID == id)
                    {
                        data = item;
                        break;
                    }
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<FacilityBridgeQueueViewModel> GetApprovedQueueData()
        {
            List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
            string queueData;
            try
            {
                queueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/ApprovedBridgeQueue.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                }
                return BridgeQueueData;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string GetAllFacilityData()
        {
            int i = 0;
            string serviceName = serviceLocator.Locate("Facility");
            try
            {
                Task<string> List = Task.Run(async () =>
                {
                    string msg = await ExternalDataServiceRepository.GetDataFromServiceAsync(serviceName, "api/FacilityService/GetAllFacilities?source="+i);
                    return msg;
                });
                List<FacilityViewModel> facilities = GetDataFromService<List<FacilityViewModel>>("api/FacilityService/GetAllFacilities?source=" + i).Result;
                //List<FacilityViewModel> facilities = JsonConvert.DeserializeObject<List<FacilityViewModel>>(List.Result);
                var facilitiesFiltered = JsonConvert.SerializeObject(facilities);
                return facilitiesFiltered;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static async Task<T> GetDataFromService<T>(string URL, string AppSettingsKey = "BilllingServiceWebAPIURL") where T : new()
        {

            T result = new T();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://192.61.99.89:8045");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                HttpResponseMessage response = await client.GetAsync(URL);
                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<T>();
                }

                return result;
            }
        }
        public int GetApprovedQueueDataCount()
        {
            List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
            string queueData;
            try
            {
                queueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/ApprovedBridgeQueue.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                }
                return BridgeQueueData.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int GetRequestedQueueDataCount()
        {
            List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
            string OpenedQueueData;
            string AssignedQueueData;
            string PendingQueueData;
            int Count = 0;
            try
            {
                OpenedQueueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/OpenedBridgeQueue.json");
                AssignedQueueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/AssignedBridgeQueue.json");
                PendingQueueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/PendingBridgeQueue.json");
                using (System.IO.TextReader reader = System.IO.File.OpenText(OpenedQueueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                    Count += BridgeQueueData.Count();
                }
                using (System.IO.TextReader reader = System.IO.File.OpenText(AssignedQueueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                    Count += BridgeQueueData.Count();
                }
                using (System.IO.TextReader reader = System.IO.File.OpenText(PendingQueueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                    Count += BridgeQueueData.Count();
                }
                return Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int GetOpenedQueueDataCount()
        {
            List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
            string queueData;
            try
            {
                queueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/OpenedBridgeQueue.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                }
                return BridgeQueueData.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int GetAssignedQueueDataCount()
        {
            List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
            string queueData;
            try
            {
                queueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/AssignedBridgeQueue.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                }
                return BridgeQueueData.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public int GetPendingQueueDataCount()
        {
            List<FacilityBridgeQueueViewModel> BridgeQueueData = new List<FacilityBridgeQueueViewModel>();
            string queueData;
            try
            {
                queueData = HostingEnvironment.MapPath("~/Areas/Facility/Resources/JSONData/BridgeQueue/PendingBridgeQueue.json");

                using (System.IO.TextReader reader = System.IO.File.OpenText(queueData))
                {
                    string text = reader.ReadToEnd();
                    JavaScriptSerializer serial = new JavaScriptSerializer();
                    BridgeQueueData = serial.Deserialize<List<FacilityBridgeQueueViewModel>>(text);
                }
                return BridgeQueueData.Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public string GetAllCounties(string searchTerm)
        {
            string serviceName = serviceLocator.Locate("CMS");
            try
            {
                Task<string> List = Task.Run(async () =>
                {
                    string msg = await ExternalDataServiceRepository.GetDataFromServiceAsync(serviceName, "api/Common/GetAllCounties");
                    return msg;
                });
                List<CountyViewModel> counties = JsonConvert.DeserializeObject<List<CountyViewModel>>(List.Result);
                counties = Filter(counties, searchTerm, "Name"); //Generic Method for Data Filteration
                var countiesFiltered = JsonConvert.SerializeObject(counties);
                return countiesFiltered;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public string GetAllCountries(string searchTerm)
        {
            string serviceName = serviceLocator.Locate("CMS");
            try
            {
                Task<string> List = Task.Run(async () =>
                    {
                        string msg = await ExternalDataServiceRepository.GetDataFromServiceAsync(serviceName, "api/Common/GetAllCountries");
                        return msg;
                    });
                List<CountryViewModel> countries = JsonConvert.DeserializeObject<List<CountryViewModel>>(List.Result);
                countries = Filter(countries, searchTerm, "Name"); //Generic Method for Data Filteration
                var countriesFiltered = JsonConvert.SerializeObject(countries);
                return countriesFiltered;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public string GetAllCities(string searchTerm)
        {
            string serviceName = serviceLocator.Locate("CMS");
            try
            {
                Task<string> List = Task.Run(async () =>
                {
                    string msg = await ExternalDataServiceRepository.GetDataFromServiceAsync(serviceName, "api/Common/GetAllCities");
                    return msg;
                });
                List<CityViewModel> cities = JsonConvert.DeserializeObject<List<CityViewModel>>(List.Result);
                cities = Filter(cities, searchTerm, "Name"); //Generic Method for Data Filteration
                var citiesFiltered = JsonConvert.SerializeObject(cities);
                return citiesFiltered;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public string GetAllStates(string searchTerm)
        {
            string serviceName = serviceLocator.Locate("CMS");
            try
            {
                Task<string> statesList = Task.Run(async () =>
                {
                  string msg = await ExternalDataServiceRepository.GetDataFromServiceAsync(serviceName, "api/Common/GetAllStates");
                  return msg;
                });
                List<StateViewModel> states = JsonConvert.DeserializeObject<List<StateViewModel>>(statesList.Result);
                states = Filter(states, searchTerm, "Name"); //Generic Method for Data Filteration
                var statesFiltered = JsonConvert.SerializeObject(states);
                return statesFiltered;
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public string GetAllOrganizations(string searchTerm)
        {
            try
            {
                if (searchTerm == null) { searchTerm = ""; }
                string dataUrl = "~/Areas/Facility/Resources/JSONData/MasterData/AllOrganizations.JSON";
                string property = "OrganizationName";
                var data = GetMasterDataFromJson<OrganizationViewModel>(searchTerm, dataUrl, property);
                //Task<InsuranceCompanyViewModel> memberList = Task.Run(async () =>
                //{
                //    InsuranceCompanyViewModel msg = await ServiceRepository.GetDataFromService<InsuranceCompanyViewModel>("api/FacilityService/GetAllFacilities?source=1");
                //    return msg;
                //});

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Common Methods
        public dynamic GetMasterDataFromJson<T>(string searchTerm, string dataUrl, string property)
        {
            string file = HttpContext.Current.Server.MapPath(dataUrl);
            string json = System.IO.File.ReadAllText(file);
            var PlansList = JsonConvert.DeserializeObject<List<T>>(json);
            var filteredPersonList = Filter(PlansList, searchTerm, property); //Generic Method for Data Filteration
            var filteredPersonListInJson = JsonConvert.SerializeObject(filteredPersonList);
            return filteredPersonListInJson;
        }
        public List<T> Filter<T>(List<T> planList, string searchTerm, string property)
        {
            List<T> plans = new List<T>();
            PropertyInfo propertyInfo = typeof(T).GetProperty(property);
            try
            {
                plans = planList.FindAll(x => x.GetType().GetProperty(property).GetValue(x).ToString().ToUpper().Contains(searchTerm.ToUpper()));
              //  plans.Select(c => c).Take(5);
            }
            catch (Exception)
            {
                throw;
            }
            return plans;
        } 
    }
}