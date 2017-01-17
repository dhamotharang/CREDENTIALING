using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.MH.ServiceFacade
{
    public class MemberServiceRepository
    {
        public static async Task<T> GetMemberDataFromServiceAsync<T>(string URL) where T : new()
        {
            T result = new T();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["CMSServiceWebAPIURL"].ToString());
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

        public static async Task<long> AddMemberData(string serviceName, string URL, object data)
        {
            long result = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings[serviceName].ToString());
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // PostMethod
                var response = client.PostAsJsonAsync(URL, data).Result;

                if (response.IsSuccessStatusCode)
                {
                    result = await response.Content.ReadAsAsync<long>();
                }
                return result;
            }
        }

        
    }
}