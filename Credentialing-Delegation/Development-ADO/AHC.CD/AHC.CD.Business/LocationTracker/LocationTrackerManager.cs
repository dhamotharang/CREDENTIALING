using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Resources.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.LocationTracker
{
    internal class LocationTrackerManager : ILocationTrackerManager
    {
        IProfileRepository profileRepository = null;        
        IUnitOfWork uow = null;

        public LocationTrackerManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();            
            this.uow = uow;
        }        

        public List<PracticeLocationDetail> GetLocationsByFacilityName(string facilityName)
        {
            try
            {
                var includeProperties = "Facility, Facility.FacilityDetail";

                var locationRepo = uow.GetGenericRepository<PracticeLocationDetail>();
                var locations = locationRepo.Get(l => l.Facility != null && l.Facility.FacilityName.Contains(facilityName), includeProperties);

                if (locations == null)
                    throw new Exception("No data available");

                return locations.ToList();

            }
            catch (ApplicationException)
            {
                throw;
            }            
        }

        public List<Entities.MasterProfile.Profile> GetAllProvidersByLocation(int facilityID)
        {
            var includeProperties = "PersonalDetail, PracticeLocationDetails, PracticeLocationDetails.Facility, PracticeLocationDetails.Facility.FacilityDetail";

            var providers = from provider in profileRepository.GetAll(includeProperties)
                            where provider.PracticeLocationDetails.Equals(facilityID)
                                        && provider.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                            select provider;            
            

            return providers.ToList();
                                       
        }


        public List<PracticeLocationDetail> GetAllProviderPracticeLocations(int profileId)
        {
            var includeProperties = "PracticeLocationDetails, PracticeLocationDetails.Facility, PracticeLocationDetails.Facility.FacilityDetail";
           
            var profile = profileRepository.Find(p => p.ProfileID == profileId, includeProperties);            
            
            return profile.PracticeLocationDetails.ToList();
        }


        public List<Entities.MasterProfile.Profile> GetAllProviders()
        {
            var includeProperties = "PersonalDetail";

            var profiles = profileRepository.GetAll(includeProperties);

            return profiles.ToList();
        }


        public List<Profile> GetAllProvidersByName(string name)
        {
            var includeProperties = "PracticeLocationDetails, PracticeLocationDetails.Facility, PracticeLocationDetails.Facility.FacilityDetail";

            var profiles = profileRepository.Get(p => p.PersonalDetail != null && (p.PersonalDetail.FirstName.Contains(name) || p.PersonalDetail.LastName.Contains(name)), includeProperties);
            
            return profiles.ToList();
        }
    }
}
