using AHC.CD.Business.LocationTracker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Controllers
{
    public class LocationTrackerController : Controller
    {
        ILocationTrackerManager locationTrackerManager = null;

        public LocationTrackerController(ILocationTrackerManager locationTrackerManager)
        {
            this.locationTrackerManager = locationTrackerManager;

        }
        // GET: Profile/LocationTracker
        [Authorize(Roles = "ADM,CCO,CRA")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetAllPracticeLocations(string facilityName)
        {
            try
            {
                var locations = locationTrackerManager.GetLocationsByFacilityName(facilityName);
                return JsonConvert.SerializeObject(locations);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public string GetAllProvidersByLocation(int facilityID)
        {
            try
            {
                var providers = locationTrackerManager.GetAllProvidersByLocation(facilityID);
                return JsonConvert.SerializeObject(providers);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public string GetAllProviderPracticeLocations(int profileID)
        {
            try
            {
                var locations = locationTrackerManager.GetAllProviderPracticeLocations(profileID);
                return JsonConvert.SerializeObject(locations);
            }
            catch (Exception)
            {
                throw;
            }
        }


        [HttpGet]
        public string GetAllProvidersByName(string name)
        {
            try
            {
                var profile = locationTrackerManager.GetAllProvidersByName(name);
                return JsonConvert.SerializeObject(profile);
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}