using Newtonsoft.Json;
using PortalTemplate.Areas.MH.Common;
using PortalTemplate.Areas.MH.IServices;
using PortalTemplate.Areas.MH.Models.ViewModels.MasterData;
using PortalTemplate.Areas.MH.Models.ViewModels.Provider;
using PortalTemplate.Areas.MH.ServiceFacade;
using PortalTemplate.Areas.MH.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace PortalTemplate.Areas.MH.Controllers
{
    public class ExternalDataController : Controller
    {
        IProviderService _providerService;
        public ExternalDataController()
        {
            _providerService = new  ProviderService();
        }

        ServiceLocator serviceLocator = new ServiceLocator();

        public JsonResult GetAllProviderAccounts(string searchTerm)
        {
            return Json(_providerService.GetAllProviderAccounts(searchTerm), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetAllProviders(string searchTerm)
        {
            return Json(_providerService.GetAllProviders(searchTerm), JsonRequestBehavior.AllowGet);
        }

        public List<FacilityServiceViewModel> GetAllFacilities()
        {
            string serviceName = serviceLocator.Locate("Facility");
            Task<string> FacilitiesList = Task.Run(async () =>
            {
                string msg = await ExternalDataServiceRepository.GetDataFromServiceAsync(serviceName, "api/FacilityService/GetAllFacilities?source=1");
                return msg;
            });

            List<FacilityServiceViewModel> facilities = JsonConvert.DeserializeObject<List<FacilityServiceViewModel>>(FacilitiesList.Result);
            return facilities;
            //return Json(await ServiceRepository.GetFacilityDataFromService<FacilityServiceViewModel>("api/FacilityService/GetAllFacilities?source=1"));
        }
	}
}