using PortalTemplate.Areas.Coding.Services.IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace PortalTemplate.Areas.Coding.Services
{
    public class ProviderService : IProviderService
    {
        HttpClient client = null;
        public ProviderService()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ProviderServiceWebAPIURL"]);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
       

        public void GetProviderDataByNPI(string NPI)
        {
            throw new NotImplementedException();
        }
    }
}