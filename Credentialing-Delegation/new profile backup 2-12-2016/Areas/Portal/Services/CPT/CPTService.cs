using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using PortalTemplate.Areas.Portal.IServices.CPT;
using System.Configuration;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PortalTemplate.Areas.Portal.Models.UMModel;

namespace PortalTemplate.Areas.Portal.Services.CPT
{
    public class CPTService : ICPTService
    {
        private static async Task<string> GetDataFromService(string URL)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(1, 0, 0));
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ICDServiceWebAPIURL"].ToString());
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                try
                {
                    HttpResponseMessage response = await client.GetAsync(URL);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return result;
            }
        }

        public List<CPTViewModel> GetAllCPTCodes()
        {
            Task<string> cptList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/CptCodes/GetAllCPTCodes");
                return msg;
            });

            List<CPTViewModel> CPTViewModels = JsonConvert.DeserializeObject<List<CPTViewModel>>(cptList.Result);
            return CPTViewModels;
        }


        public List<CPTViewModel> GetCPTCodeByDescription(string descString, int limit)
        {
            Task<string> cptList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/CptCodes/GetCPTCodesByDescription?descString=" + descString);
                return msg;
            });
            CPTServiceDTO data = JsonConvert.DeserializeObject<CPTServiceDTO>(cptList.Result);
            List<CPTViewModel> CPTViewModels = new List<CPTViewModel>();
            CPTViewModels = data.CPTCodeList;
            return CPTViewModels.Take<CPTViewModel>(limit).ToList();
        }


        public List<CPTViewModel> GetAllCPTCodesByCodeString(string codeString,int limit)
        {
            Task<string> cptList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/CptCodes/GetCPTCodesByCode?codeString=" + codeString);
                return msg;
            });

            CPTServiceDTO data = JsonConvert.DeserializeObject<CPTServiceDTO>(cptList.Result);
            List<CPTViewModel> CPTViewModels = new List<CPTViewModel>();
            CPTViewModels = data.CPTCodeList;
            return CPTViewModels.Take<CPTViewModel>(limit).ToList();
        }
        public List<CPTViewModel> GetAllCPTCodesByRange(string FromCode, string ToCode)
        {
            Task<string> cptList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/CptCodes/GetAllCPTCodeswithrange?lowercptcode=" + FromCode + "&uppercptcode=" + ToCode);
                return msg;
            });

            CPTServiceDTO data = JsonConvert.DeserializeObject<CPTServiceDTO>(cptList.Result);
            List<CPTViewModel> CPTViewModels = new List<CPTViewModel>();
            CPTViewModels = data.CPTCodeList;
            return CPTViewModels;
        }
    }
}