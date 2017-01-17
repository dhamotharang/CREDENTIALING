using AHC.CD.Business.LocationTracker;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AHC.CD.WebUI.MVC.Areas.MobileSite.Controllers
{
    public class LocationTrackerMobileController : Controller
    {
        ILocationTrackerManager locationTrackerManager = null;

        public LocationTrackerMobileController(ILocationTrackerManager locationTrackerManager)
        {
            this.locationTrackerManager = locationTrackerManager;
            
        }
        // GET: Profile/LocationTracker
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string GetAllPracticeLocations(string facilityName)
        {
            var locations = locationTrackerManager.GetLocationsByFacilityName(facilityName);
            return JsonConvert.SerializeObject(locations);
        }

        [HttpGet]
        public string GetAllProvidersByLocation(int facilityID)
        {
            var providers = locationTrackerManager.GetAllProvidersByLocation(facilityID);
            return JsonConvert.SerializeObject(providers);
        }

        [HttpGet]
        public string GetAllProviderPracticeLocations(int profileID)
        {
            var locations = locationTrackerManager.GetAllProviderPracticeLocations(profileID);
            return JsonConvert.SerializeObject(locations);
        }

        [HttpGet]
        public string GetAllProviders()
        {
            var providers = locationTrackerManager.GetAllProviders();
            return JsonConvert.SerializeObject(providers);
        }

        [HttpGet]
        public string GetAllProvidersByName(string name)
        {
            var profile = locationTrackerManager.GetAllProvidersByName(name);
            return JsonConvert.SerializeObject(profile);
        }
    }
}