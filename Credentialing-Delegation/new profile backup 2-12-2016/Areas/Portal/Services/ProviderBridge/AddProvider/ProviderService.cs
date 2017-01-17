using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalTemplate.Areas.Portal.IServices.ProviderBridge.AddProvider;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PortalTemplate.Areas.UM.Models.ViewModels.Authorization;
using Newtonsoft.Json;
using PortalTemplate.Areas.Portal.Models.ProviderBridge.AddProvider;
using PortalTemplate.Areas.Portal.Models.ProviderBridge.BridgeQueue;
using System.Text;

namespace PortalTemplate.Areas.Portal.Services.ProviderBridge.AddProvider
{
    public class ProviderService : IProviderService
    {
        public ProviderService()
        {

        }

        public List<ProviderViewModal> GetAllProviders()
        {
            Task<string> providerList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/UM/GetAllProvidersByPlanName?planName=Ultimate");
                return msg;
            });

            List<ProviderViewModal> ProviderViewModal = JsonConvert.DeserializeObject<List<ProviderViewModal>>(providerList.Result);
            return ProviderViewModal;
        }

        public List<ProviderViewModal> SearchProvider(string name)
        {
            if (name == null)
            {
                return null;
            }
            Task<string> providerList = Task.Run(async () =>
            {
                string msg = await GetDataFromService("api/ProviderService/SearchProvider?name=" + name);
                return msg;
            });

            List<ProviderViewModal> ProviderViewModal = JsonConvert.DeserializeObject<List<ProviderViewModal>>(providerList.Result);
            return ProviderViewModal.Take<ProviderViewModal>(15).ToList();
        }

        #region New Provider Service Abstraction

        /// <summary>
        /// Add a New Provider to the Bridge Request Queue
        /// </summary>
        /// <param name="Provider"></param>
        /// <returns></returns>
        public AddProviderViewModel AddProvider(AddProviderViewModel Provider)
        {

            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<AddProviderViewModel>("api/ProviderBridge/AddTemporaryProvider?Provider=" + Provider + "", Provider, "PVProviderServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Edit a New Provider from the Bridge Request Queue
        /// </summary>
        /// <param name="Provider"></param>
        /// <returns></returns>
        public AddProviderViewModel EditProvider(AddProviderViewModel Provider)
        {
            try
            {
                return Task.Run(() => ServiceRepository.PostDataToService<AddProviderViewModel>("api/ProviderBridge/AddTemporaryProvider?Provider=" + Provider, null, "PVProviderServiceWebAPIURL")).Result;
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        #endregion

        #region Private Method

        private static async Task<string> GetDataFromService(string URL)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(1, 0, 0));
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ProviderServiceWebAPIURL"].ToString());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // HTTP GET
                try
                {
                    HttpResponseMessage response = await client.GetAsync(URL);
                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                        result = JsonConvert.DeserializeObject<string>(result);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return result;
            }
        }

        private async Task<string> PostDataToService(object obj,string URL)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings["PVProviderServiceWebAPIURL"].ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    StringContent content = new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                    HttpResponseMessage response =await client.PostAsync(URL, content);
                    if (response.IsSuccessStatusCode)
                    {
                        string data = await response.Content.ReadAsStringAsync();
                        var res = JsonConvert.DeserializeObject<string>(data);
                        return res;
                    }
                    else
                    {
                        return null;
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}