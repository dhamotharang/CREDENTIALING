using AHC.CD.Business.MasterData;
using AHC.CD.Entities.Location;
using AHC.CD.Entities.MasterData.Tables;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Caching;
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

        public async Task<string> GetCountry() {

            var countries = await masterDataManager.GetAllCountriesAsync();
            return JsonConvert.SerializeObject(countries);
        }

        IEnumerable<LocationDetail> locations = null;
        public async Task<JsonResult> GetCities(string city)
        {
            // Jugad Technology Implemented
            // Need to put data in cache when the project starts for the first time.
            if (this.ControllerContext.HttpContext.Cache["LOCATIONS"] == null)
            {
                var data = await masterDataManager.GetCitiesAllAsync();
                locations = data.ToList();
                this.ControllerContext.HttpContext.Cache["LOCATIONS"] = locations;
            }
            else
            {
                locations = (IEnumerable<LocationDetail>)this.ControllerContext.HttpContext.Cache["LOCATIONS"];
            }

            locations = from loc in locations
                        where loc.City.ToLower().StartsWith(city.ToLower())
                        select loc;

            return Json(locations, JsonRequestBehavior.AllowGet);
        }

        //public async Task<string> SetAllLocation()
        //{
        //    WebClient client = new WebClient();
        //    var countries = JsonConvert.DeserializeObject<Dictionary<string, object>>(client.DownloadString(@"https://raw.githubusercontent.com/Holek/steam-friends-countries/master/data/steam_countries.json"));

        //    var countryList = new List<Country>();

        //    foreach (var country in countries)
        //    {
        //        Country countryObject = new Country();
        //        countryObject.Code = country.Key.ToString();
                
        //        var value = JsonConvert.DeserializeObject<Dictionary<string, object>>(country.Value.ToString());

        //        countryObject.Name = value["name"].ToString();
        //        countryObject.Coordinates = value.ContainsKey("coordinates") ? value["coordinates"].ToString() : null;

        //        var states = JsonConvert.DeserializeObject<Dictionary<string, object>>(value["states"].ToString());
        //        var statesList = new List<State>();
        //        foreach (var state in states)
        //        {
        //            State stateObject = new State();
        //            stateObject.Code = state.Key.ToString();

        //            var value1 = JsonConvert.DeserializeObject<Dictionary<string, object>>(state.Value.ToString());

        //            stateObject.Name = value1["name"].ToString();
        //            stateObject.Coordinates = value1.ContainsKey("coordinates") ? value1["coordinates"].ToString() : null;

        //            var cities = JsonConvert.DeserializeObject<Dictionary<string, object>>(value1["cities"].ToString());
        //            var citiesList = new List<City>();
                    
        //            foreach (var city in cities)
        //            {
        //                City cityObject = new City();
        //                cityObject.Code = city.Key.ToString();

        //                var value2 = JsonConvert.DeserializeObject<Dictionary<string, object>>(city.Value.ToString());

        //                cityObject.Name = value2["name"].ToString();
        //                cityObject.Coordinates = value2.ContainsKey("coordinates") ? value2["coordinates"].ToString() : null;

        //                citiesList.Add(cityObject);
        //            }
        //            stateObject.Cities = citiesList;
        //            statesList.Add(stateObject);
        //        }
        //        countryObject.States = statesList;
        //        countryList.Add(countryObject);
        //    }

        //    new AHC.CD.Data.EFRepository.LocationInitializer().Seed(countryList);
            
        //    return "Done";
        //}
    }
}
