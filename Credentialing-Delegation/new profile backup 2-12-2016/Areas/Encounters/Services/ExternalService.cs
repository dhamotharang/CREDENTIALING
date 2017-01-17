using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Services
{
    public class ExternalService
    {
        public static async Task<string> GetDataFromServiceAsync(string ServiceName, string URL)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(1, 0, 0));
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings[ServiceName].ToString());
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                try
                {
                    HttpResponseMessage response = await client.GetAsync(URL);

                    if (response.IsSuccessStatusCode)
                    {
                        result = await response.Content.ReadAsStringAsync();
                        //result = JsonConvert.DeserializeObject<object>(jsonAsString);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return result;
            }
        }
    }
}