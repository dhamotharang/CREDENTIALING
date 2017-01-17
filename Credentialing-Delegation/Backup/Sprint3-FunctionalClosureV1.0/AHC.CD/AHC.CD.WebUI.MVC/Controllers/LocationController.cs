using AHC.CD.Business.MasterData;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Controllers
{
    public class LocationController : Controller
    {
        IMasterDataManager masterDataManager = null;
        public LocationController(IMasterDataManager masterDataManager)
        {
            this.masterDataManager = masterDataManager;
        }


        public async Task<JsonResult> Get(string city)
        {
            IEnumerable<LocationDetail> locations = new List<LocationDetail>();

            locations = await masterDataManager.GetLocationsByCityAsync(city);
            return Json(locations, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> GetStates()
        {
            var states = await masterDataManager.GetAllStatesAsync();
            return Json(states, JsonRequestBehavior.AllowGet);
        }
    }
}
