using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.LocationTracker
{
    public interface ILocationTrackerManager
    {
        List<Facility> GetLocationsByFacilityName(string facilityName);
        List<Profile> GetAllProvidersByLocation(int facilityID);
        List<PracticeLocationDetail> GetAllProviderPracticeLocations(int profileId);
        List<Profile> GetAllProviders();
        List<Profile> GetAllProvidersByName(string name);
    }
}
