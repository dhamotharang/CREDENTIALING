using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Portal.Services.MasterData
{
    public class CMSMasterData
    {

        /// <summary>
        /// Get List of All City
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllCity()
        {
            try
            {
                Task<string> cityList = Task.Run(async () =>
                {
                    string msg = await GetDataFromService("/api/Common/GetAllCities");
                    return msg;
                });

                List<string> CityList = JsonConvert.DeserializeObject<List<string>>(cityList.Result);
                return CityList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Private Method

        private static async Task<string> GetDataFromService(string URL)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(1, 0, 0));
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CMSServiceWebAPIURL"].ToString());
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

        #endregion
    }
}