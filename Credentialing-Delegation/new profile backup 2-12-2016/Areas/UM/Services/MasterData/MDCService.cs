using Newtonsoft.Json;
using PortalTemplate.Areas.UM.CustomHelpers;
using PortalTemplate.Areas.UM.IServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.UM.Services.MasterData
{
    public class MDCService : IMDCService
    {
        private readonly string baseURL;
        
        private readonly ServiceUtility serviceUtility;

        public MDCService()
        {
            this.baseURL = ConfigurationManager.AppSettings["CMSServiceWebAPIURL"].ToString();
            this.serviceUtility = new ServiceUtility();
        }
        public List<Models.ViewModels.Authorization.MDCCodeViewModel> GetAllMDCCodesByCode(string code, int limit)
        {
            List<Models.ViewModels.Authorization.MDCCodeViewModel> MDCcodes = new List<Models.ViewModels.Authorization.MDCCodeViewModel>();
            Task<string> MDCList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/Common/GetAllMDCCodesByCode?MDCCode=" + code + "&recordsCount=" + limit);
                return msg;
            });

            if (MDCList.Result != null)
            {
                MDCcodes = JsonConvert.DeserializeObject<List<Models.ViewModels.Authorization.MDCCodeViewModel>>(MDCList.Result);
            }


            return MDCcodes;
        }

        public List<Models.ViewModels.Authorization.MDCCodeViewModel> GetAllMDCCodesByDesc(string desc, int limit)
        {
            List<Models.ViewModels.Authorization.MDCCodeViewModel> MDCcodes = new List<Models.ViewModels.Authorization.MDCCodeViewModel>();
            Task<string> MDCList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/Common/GetAllMDCCodesByCodeDescription?MDCCodeDescription=" + desc + "&recordsCount=" + limit);
                return msg;
            });

            if (MDCList.Result != null)
            {
                MDCcodes = JsonConvert.DeserializeObject<List<Models.ViewModels.Authorization.MDCCodeViewModel>>(MDCList.Result);
            }


            return MDCcodes;
        }
    }
}