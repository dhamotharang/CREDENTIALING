
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
    public class DRGService : IDRGService
    {
        private readonly string baseURL;
        private readonly ServiceUtility serviceUtility;

        public DRGService()
        {
            this.baseURL = ConfigurationManager.AppSettings["CMSServiceWebAPIURL"].ToString();
            this.serviceUtility = new ServiceUtility();
        }

        public List<Models.ViewModels.Authorization.DRGCodeViewModel> GetAllDRGCodesByCode(string code, int limit)
        {
            List<Models.ViewModels.Authorization.DRGCodeViewModel> drgcodes = new List<Models.ViewModels.Authorization.DRGCodeViewModel>();
            Task<string> drgList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/Common/GetAllDRGCodesByCode?DRGCode=" + code + "&recordsCount=" + limit);
                return msg;
            });

            if (drgList.Result != null)
            {
                drgcodes = JsonConvert.DeserializeObject<List<Models.ViewModels.Authorization.DRGCodeViewModel>>(drgList.Result);
            }


            return drgcodes;
        }

        public List<Models.ViewModels.Authorization.DRGCodeViewModel> GetAllDRGCodesByDesc(string desc, int limit)
        {
            List<Models.ViewModels.Authorization.DRGCodeViewModel> drgcodes = new List<Models.ViewModels.Authorization.DRGCodeViewModel>();
            Task<string> drgList = Task.Run(async () =>
            {
                string msg = await serviceUtility.GetDataFromService(baseURL, "api/Common/GetAllDRGCodesByCodeDescription?DRGCodeDescription=" + desc + "&recordsCount=" + limit);
                return msg;
            });

            if (drgList.Result != null)
            {
                drgcodes = JsonConvert.DeserializeObject<List<Models.ViewModels.Authorization.DRGCodeViewModel>>(drgList.Result);
            }


            return drgcodes;
        }
    }
}