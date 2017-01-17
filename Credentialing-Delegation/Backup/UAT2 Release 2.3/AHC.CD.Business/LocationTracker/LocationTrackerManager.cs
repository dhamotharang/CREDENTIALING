using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterData.Account.Branch;
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

        public List<Facility> GetLocationsByFacilityName(string facilityName)
        {
            try
            {                

                var locationRepo = uow.GetGenericRepository<Facility>();
                var locations = locationRepo.Get(l => (((l.FacilityName.Contains(facilityName))  || (l.Building != null && l.Building.Contains(facilityName)) 
                    || (l.City != null && l.City.Contains(facilityName)) 
                    || (l.State != null && l.State.Contains(facilityName))
                    || (l.Street != null && l.Street.Contains(facilityName))
                    || (l.Country != null && l.Country.Contains(facilityName)))                    
                    && l.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()));
                               
                return locations.ToList();

            }
            catch (Exception)
            {
                throw;
            }            
        }

        public List<Entities.MasterProfile.Profile> GetAllProvidersByLocation(int facilityID)
        {
            var includeProperties = "PersonalDetail, PersonalDetail.ProviderTitles, PersonalDetail.ProviderTitles.ProviderType, PracticeLocationDetails, PracticeLocationDetails.Facility, PracticeLocationDetails.Facility.FacilityDetail";
            List<Profile> profiles = new List<Profile>();
            try
            {
                var providers = profileRepository.Get(p => p.PracticeLocationDetails.Count > 0, includeProperties).ToList();

                foreach (var item in providers)
                {
                    var IsTrue = item.PracticeLocationDetails.Any(f => f.FacilityId == facilityID && f.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString());

                    if (IsTrue)
                        profiles.Add(item);
                }


                return profiles;
            }
            catch (Exception)
            {
                throw;
            }
                                       
        }


        public List<PracticeLocationDetail> GetAllProviderPracticeLocations(int profileId)
        {
            var includeProperties = "PracticeLocationDetails, PracticeLocationDetails.Facility, PracticeLocationDetails.Facility.FacilityDetail";
            try
            {
                var profile = profileRepository.Find(p => p.ProfileID == profileId, includeProperties);

                return profile.PracticeLocationDetails.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }


        public List<Entities.MasterProfile.Profile> GetAllProviders()
        {
            var includeProperties = "PersonalDetail";
            try
            {
                var profiles = profileRepository.GetAll(includeProperties);

                return profiles.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            
        }


        public List<Profile> GetAllProvidersByName(string name)
        {
            var includeProperties = "PersonalDetail.ProviderTitles, PersonalDetail.ProviderTitles.ProviderType, PracticeLocationDetails, PracticeLocationDetails.Facility, PracticeLocationDetails.Facility.FacilityDetail";
            try
            {
                var profiles = profileRepository.Get(p => p.PersonalDetail != null && (p.PersonalDetail.FirstName.Contains(name) || p.PersonalDetail.LastName.Contains(name)) && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties);

                return profiles.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
