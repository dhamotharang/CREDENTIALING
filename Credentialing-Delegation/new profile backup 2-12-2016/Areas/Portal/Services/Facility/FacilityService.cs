using Newtonsoft.Json;
using PortalTemplate.Areas.Portal.IServices.Facility;
using PortalTemplate.Areas.UM.CustomHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace PortalTemplate.Areas.Portal.Services.Facility
{
    public class FacilityService : IFacilityService
    {
        private readonly ServiceUtility serviceUtil;
        private string baseURL;

        public FacilityService()
        {
            this.baseURL = ConfigurationManager.AppSettings["FacilityServiceWebAPIURL"].ToString();
            this.serviceUtil = new ServiceUtility();
        }
        public List<UM.Models.ViewModels.Authorization.FacilityViewModel> GetAllFacility()
        {
            List<UM.Models.ViewModels.Authorization.FacilityViewModel> facilities = new List<UM.Models.ViewModels.Authorization.FacilityViewModel>();
            Task<string> facilityList = Task.Run(async () =>
            {
                string msg = await serviceUtil.GetDataFromService(baseURL,"api/FacilityService/GetAllFacilities?source=0");
                return msg;
            });

            facilities = JsonConvert.DeserializeObject<List<UM.Models.ViewModels.Authorization.FacilityViewModel>>(facilityList.Result);
            return facilities;
        }


        public List<UM.Models.ViewModels.Authorization.FacilityViewModel> SearchFacility(string searchTerm, int limit)
        {
            List<UM.Models.ViewModels.Authorization.FacilityViewModel> facilities = new List<UM.Models.ViewModels.Authorization.FacilityViewModel>();
            Task<string> facilityList = Task.Run(async () =>
            {
                string msg = await serviceUtil.GetDataFromService(baseURL, "api/FacilityService/SearchFacility?source=0&searchTerm=" + searchTerm + "&limit=" + limit);
                return msg;
            });

            if (facilityList.Result != null)
            {
                facilities = JsonConvert.DeserializeObject<List<UM.Models.ViewModels.Authorization.FacilityViewModel>>(facilityList.Result);
            }
            return facilities;
        }
    }
}