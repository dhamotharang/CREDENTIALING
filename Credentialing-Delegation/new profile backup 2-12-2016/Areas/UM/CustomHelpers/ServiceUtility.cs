using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.CustomHelpers
{
    public class ServiceUtility
    {
        public async Task<string> GetDataFromService(string baseURL, string URL)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(1, 0, 0));
                client.BaseAddress = new Uri(baseURL);
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
        public  async Task<string> PostDataToService(string baseURL,string URL, object data)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(1, 0, 0));

                client.BaseAddress = new Uri(baseURL);
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                // PostMethod
                try
                {
                    var data1 = new JavaScriptSerializer().Serialize(data);

                    var response = await client.PostAsJsonAsync(URL, data);

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
    }
}