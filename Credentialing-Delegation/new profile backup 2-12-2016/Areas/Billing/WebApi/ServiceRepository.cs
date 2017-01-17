using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Billing.WebApi
{
    public class ServiceRepository
    {
        public static async Task<T> GetDataFromService<T>(string URL, string AppSettingsKey = "BilllingServiceWebAPIURL") where T : new()
        {
            
            T result = new T();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings[AppSettingsKey].ToString());
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

        public static async Task<T> PostDataToService<T>(string URL, object data, string AppSettingsKey = "BilllingServiceWebAPIURL") where T : new()
        {
            try
            {
                T result = new T(); ;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ConfigurationManager.AppSettings[AppSettingsKey].ToString());
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // PostMethod
                    var response = client.PostAsJsonAsync(URL, data).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsAsync<T>();
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

    }
}