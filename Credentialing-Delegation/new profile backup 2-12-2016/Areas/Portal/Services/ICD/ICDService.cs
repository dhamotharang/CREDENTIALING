using Newtonsoft.Json;
using PortalTemplate.Areas.Portal.IServices.CPT;
using PortalTemplate.Areas.Portal.IServices.ICD;
using PortalTemplate.Areas.Portal.Models.UMModel;
using PortalTemplate.Areas.UM.Models.ViewModels.ICD;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Portal.Services.ICD
{
    public class ICDService : IICDService
    {
        public List<ICDViewModel> GetICDCodesByVersion(string version)
        {
            Task<string> icdList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/IcdCodes/GetICDCodesByVersion?version="+version);
                return msg;
            });

            List<ICDViewModel> ICDCodesViewModels = JsonConvert.DeserializeObject<List<ICDViewModel>>(icdList.Result);
            return ICDCodesViewModels;
        }

        private static async Task<string> GetDataFromService(string URL)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(1, 0, 0));
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ICDServiceWebAPIURL"].ToString());
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); 
                //client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

                // HTTP GET
                try
                {
                    HttpResponseMessage response = await client.GetAsync(URL);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                        //result = GZipStream.UncompressString(response.Content.ReadAsByteArrayAsync().Result);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return result;
            }
        }


        public List<ICDViewModel> GetICDCodeByDescription(string version, string codeString)
        {
            Task<string> cptList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/IcdCodes/GetICDCodesByCodeString?version="+version+"&CodeString="+codeString);
                //string msg = await GetDataFromService("api/IcdCodes/GetICDCodesByLimit?version=" + version + "&limit=100");
                return msg;
            });

            var icds = cptList.Result;

            //ICDServiceViewModel ICDServiceViewModels = JsonConvert.DeserializeObject<ICDServiceViewModel>(cptList.Result);
            ICDServiceViewModel ICDServiceViewModels = JsonConvert.DeserializeObject<ICDServiceViewModel>(cptList.Result); 
            List<ICDViewModel> ICDViewModels = new List<ICDViewModel>();
            foreach (var item in ICDServiceViewModels.IcdCodeList)
            {
                ICDViewModel ICDViewModel = new ICDViewModel();
                ICDViewModel.ICDCode = item.ICDCode;
                ICDViewModel.ICDCodeDescription = item.ICDCodeDescription;
                ICDViewModels.Add(ICDViewModel);
            }
            return ICDViewModels;
        }


        public List<ICDViewModel> GetICDCodesByLimit(string version, string limit)
        {
            Task<string> cptList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/IcdCodes/GetICDCodesByLimit?version=" + version + "&limit=" + limit);
                return msg;
            });

            List<ICDViewModel> ICDCodesViewModels = JsonConvert.DeserializeObject<List<ICDViewModel>>(cptList.Result);
            return ICDCodesViewModels;
        }


        public List<ICDViewModel> GetICDCodesByDescWithLimit(string version, string descString,int limit)
        {
            Task<string> cptList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/IcdCodes/GetICDCodesByDescStringwithlimit?version=" + version + "&DescString=" + descString + "&limit=" + limit);
                return msg;
            });
            var data = JsonConvert.DeserializeObject<ICDServiceDTO>(cptList.Result);
            //List<ICDViewModel> ICDCodesViewModels = JsonConvert.DeserializeObject<List<ICDViewModel>>(cptList.Result);
            List<ICDViewModel> ICDCodesViewModels = new List<ICDViewModel>();
            ICDCodesViewModels = ConvertToICDViewModel(data);
            return ICDCodesViewModels;
        }


        public List<ICDViewModel> GetAllIcdsByicdorcodeStringwithLimit(string version, string codeString, int limit)
        {
            Task<string> cptList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/IcdCodes/GetICDCodesByVersionandcodewithlimit?version="+version + "&icdcode=" + codeString + "&limit="+ limit);
                return msg;
            });
            var data = JsonConvert.DeserializeObject<ICDServiceDTO>(cptList.Result);

            //List<ICDViewModel> ICDCodesViewModels = JsonConvert.DeserializeObject<List<ICDViewModel>>(cptList.Result);
            List<ICDViewModel> ICDCodesViewModels = new List<ICDViewModel>();
            ICDCodesViewModels = ConvertToICDViewModel(data);
            return ICDCodesViewModels;
        }

        private List<ICDViewModel> ConvertToICDViewModel(ICDServiceDTO data)
        {
            List<ICDViewModel> finalList = new List<ICDViewModel>();
            foreach (var item in data.IcdCodeList)
            {
                ICDViewModel icd = new ICDViewModel ();
                icd.ICDCode = item.IcdCodeNumber;
                icd.ICDCodeDescription = item.Description;
                icd.ICDVersion = item.IcdCodeVersion;
                finalList.Add(icd);
            }
            return finalList;
        }


        public List<ICDViewModel> GetICDCodesByDescString(string version, string descString)
        {
            throw new NotImplementedException();
        }
    }
}