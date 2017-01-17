using Newtonsoft.Json;
using PortalTemplate.Areas.CMS.Helper;
using PortalTemplate.Areas.UM.IServices.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.UM.Services.Authorization
{
    public class AuthorizationService:IAuthorizationService
    {
        private readonly string baseURL;
        private readonly string UMbaseURL;
        private readonly ServiceUtility serviceUtility;
        public AuthorizationService()
        {
            this.baseURL = ConfigurationManager.AppSettings["CMSServiceWebAPIURL"].ToString();
            this.UMbaseURL = ConfigurationManager.AppSettings["UMService"].ToString();
            this.serviceUtility = new ServiceUtility();
        }
        public async Task SaveAuthorization(Models.ViewModels.Authorization.AuthorizationViewModel Authorization)        
        {
            string result = await PostDataToService("api/Authorization/SaveAuthorization", Authorization);
            //return memberDetail;
        }


        /// <summary>
        /// For getting the data from web api's
        /// </summary>
        /// <param name="URL">Extension URL for the given Abstraction apart from the IP</param>
        /// <returns>Json Object</returns>
        private static async Task<string> GetDataFromService(string URL)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(1, 0, 0));
                client.BaseAddress = new Uri("http://192.61.99.89:8049/");//ConfigurationManager.AppSettings["MemberAPIURL"].ToString());
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
        private static async Task<string> PostDataToService(string URL, object data)
        {
            string result = null;
            using (var client = new HttpClient())
            {
                client.Timeout.Add(new TimeSpan(1, 0, 0));

                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["UMService"].ToString());
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
        public int? CalculatePlainLanguageValues(Models.ViewModels.Authorization.AuthorizationViewModel auth, int index)
        {
              if (auth.CPTCodes[index].RequestedUnits == null)
            {
                auth.CPTCodes[index].TotalUnits = null;
            }
            else if (auth.CPTCodes[index].NumberPer == null || auth.CPTCodes[index].NumberPer == 0)
            {
                auth.CPTCodes[index].TotalUnits = auth.CPTCodes[index].RequestedUnits;
            }
            else
            {
                if (auth.CPTCodes[index].Range1 != null  && auth.CPTCodes[index].Range2 != null)
                {
                    if (((auth.CPTCodes[index].Range1.ToString().ToLower().Equals("day")) || (auth.CPTCodes[index].Range1.ToString().ToLower().Equals("days"))) && ((auth.CPTCodes[index].Range2.ToString().ToLower().Equals("day")) || (auth.CPTCodes[index].Range2.ToString().ToLower().Equals("days"))))
                    {
                        auth.CPTCodes[index].TotalUnits = auth.CPTCodes[index].NumberPer * auth.CPTCodes[index].RequestedUnits;
                    }
                    else if (((auth.CPTCodes[index].Range1.ToString().ToLower().Equals("day")) || (auth.CPTCodes[index].Range1.ToString().ToLower().Equals("days"))) && ((auth.CPTCodes[index].Range2.ToString().ToLower().Equals("week")) || (auth.CPTCodes[index].Range2.ToString().ToLower().Equals("weeks"))))
                    {
                        auth.CPTCodes[index].TotalUnits = auth.CPTCodes[index].NumberPer * (auth.CPTCodes[index].RequestedUnits * 7);
                    }
                    else if (((auth.CPTCodes[index].Range1.ToString().ToLower().Equals("day")) || (auth.CPTCodes[index].Range1.ToString().ToLower().Equals("days"))) && ((auth.CPTCodes[index].Range2.ToString().ToLower().Equals("month")) || (auth.CPTCodes[index].Range2.ToString().ToLower().Equals("months"))))
                    {
                        auth.CPTCodes[index].TotalUnits = auth.CPTCodes[index].NumberPer * (auth.CPTCodes[index].RequestedUnits * 30);
                    }
                    else if (((auth.CPTCodes[index].Range1.ToString().ToLower().Equals("week")) || (auth.CPTCodes[index].Range1.ToString().ToLower().Equals("weeks"))) && ((auth.CPTCodes[index].Range2.ToString().ToLower().Equals("day")) || (auth.CPTCodes[index].Range2.ToString().ToLower().Equals("days"))))
                    {
                        auth.CPTCodes[index].TotalUnits = (auth.CPTCodes[index].NumberPer) * (auth.CPTCodes[index].RequestedUnits);
                    }
                    else if (((auth.CPTCodes[index].Range1.ToString().ToLower().Equals("week")) || (auth.CPTCodes[index].Range1.ToString().ToLower().Equals("weeks"))) && ((auth.CPTCodes[index].Range2.ToString().ToLower().Equals("week")) || (auth.CPTCodes[index].Range2.ToString().ToLower().Equals("weeks"))))
                    {
                        auth.CPTCodes[index].TotalUnits = (auth.CPTCodes[index].NumberPer) * (auth.CPTCodes[index].RequestedUnits);
                    }
                    else if (((auth.CPTCodes[index].Range1.ToString().ToLower().Equals("week")) || (auth.CPTCodes[index].Range1.ToString().ToLower().Equals("weeks"))) && ((auth.CPTCodes[index].Range2.ToString().ToLower().Equals("month")) || (auth.CPTCodes[index].Range2.ToString().ToLower().Equals("months"))))
                    {
                        auth.CPTCodes[index].TotalUnits = (Convert.ToInt32((auth.CPTCodes[index].NumberPer * 30) / 7)) * (auth.CPTCodes[index].RequestedUnits);
                    }
                    else if (((auth.CPTCodes[index].Range1.ToString().ToLower().Equals("month")) || (auth.CPTCodes[index].Range1.ToString().ToLower().Equals("months"))) && ((auth.CPTCodes[index].Range2.ToString().ToLower().Equals("day")) || (auth.CPTCodes[index].Range2.ToString().ToLower().Equals("days"))))
                    {
                        auth.CPTCodes[index].TotalUnits = (auth.CPTCodes[index].NumberPer) * (auth.CPTCodes[index].RequestedUnits);
                    }
                    else if (((auth.CPTCodes[index].Range1.ToString().ToLower().Equals("month")) || (auth.CPTCodes[index].Range1.ToString().ToLower().Equals("months"))) && ((auth.CPTCodes[index].Range2.ToString().ToLower().Equals("week")) || (auth.CPTCodes[index].Range2.ToString().ToLower().Equals("weeks"))))
                    {
                        auth.CPTCodes[index].TotalUnits = (auth.CPTCodes[index].NumberPer) * (auth.CPTCodes[index].RequestedUnits);
                    }
                    else if (((auth.CPTCodes[index].Range1.ToString().ToLower().Equals("month")) || (auth.CPTCodes[index].Range1.ToString().ToLower().Equals("months"))) && ((auth.CPTCodes[index].Range2.ToString().ToLower().Equals("month")) || (auth.CPTCodes[index].Range2.ToString().ToLower().Equals("months"))))
                    {
                        auth.CPTCodes[index].TotalUnits = (auth.CPTCodes[index].NumberPer) * (auth.CPTCodes[index].RequestedUnits);
                    }
                }
               
              
            }
            return (auth.CPTCodes[index].TotalUnits);
        }


        public int GetPOSID(string POSName)
        {
            int posId=0;
            //List<Models.MasterDataEntities.MasterDataPlaceOfServiceViewModel> placeOfServices = new List<Models.MasterDataEntities.MasterDataPlaceOfServiceViewModel>();
            //Task<string> placeOfServicesList = Task.Run(async () =>
            //{
            //    string msg = await serviceUtility.GetDataFromService(baseURL, "/api/UM/GetAllPlaceOfServices?codeType="+2);
            //    return msg;
            //});
            //if (placeOfServicesList.Result != null)
            //{
            //    placeOfServices = JsonConvert.DeserializeObject<List<Models.MasterDataEntities.MasterDataPlaceOfServiceViewModel>>(placeOfServicesList.Result);
            //}
            //foreach (var md in placeOfServices)
            //{
            //    if (md.Name == POSName)
            //    {
            //        posId = md.PlaceOfServiceID;
            //    }
            //}
            switch (POSName)
            {
                case "11(a)- OFFICE":
                    return 1;
                case "12- PATIENT HOME":
                    return 2;
                case "21- IP HOSPITAL":
                    return 3;
                case "22- OP HOSPITAL":
                    return 4;
                case "24- ASC":
                    return 5;
                case "31- SNF":
                    return 6;
                case "62- CORF":
                    return 7;
                default:
                    return 0;
            }
        }
    }
}