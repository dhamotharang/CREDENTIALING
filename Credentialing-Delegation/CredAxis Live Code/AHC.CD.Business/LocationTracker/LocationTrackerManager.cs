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
                var locations = locationRepo.Get(l => (((l.FacilityName.Contains(facilityName)) || (l.Building != null && l.Building.Contains(facilityName))
                    || (l.City != null && l.City.Contains(facilityName))
                    || (l.State != null && l.State.Contains(facilityName))
                    || (l.Street != null && l.Street.Contains(facilityName))
                    || (l.Country != null && l.Country.Contains(facilityName))
                    || (l.ZipCode != null && l.ZipCode.Contains(facilityName))
                    || (l.County != null && l.County.Contains(facilityName)))
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
                var providers = profileRepository.Get(p => p.PracticeLocationDetails.Count > 0 && p.Status=="Active", includeProperties).ToList();

                foreach (var item in providers)
                {
                    var IsTrue = item.PracticeLocationDetails.Any(f => f.FacilityId == facilityID && f.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString());

                    if (IsTrue)
                    {
                        var providerTitle = "";
                        if (item.PersonalDetail != null && item.PersonalDetail.ProviderTitles.Count > 0)
                        {
                            item.PersonalDetail.ProviderTitles = item.PersonalDetail.ProviderTitles.Where(s => s.Status == StatusType.Active.ToString()).ToList();
                            providerTitle = String.Join(", ", item.PersonalDetail.ProviderTitles.Select(s => s.ProviderType.Title));
                        }
                        item.PersonalDetail.ProviderTitles.ElementAt(0).ProviderType.Title = providerTitle;
                        profiles.Add(item);
                    }
                        
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
                string[] ProviderName = name.Split(' ');
                var profiles = new List<Profile>();
               // int flag = 0;
                string firstname;
                string middlename;
                string lastname;
                if (ProviderName.Length == 1)
                {
                    firstname = ProviderName[0];
                    profiles = profileRepository.Get(p => p.PersonalDetail != null && (p.PersonalDetail.FirstName.Contains(firstname) || p.PersonalDetail.MiddleName.Contains(firstname) || p.PersonalDetail.LastName.Contains(firstname)) && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties).ToList();

                }
                else if(ProviderName.Length == 2)
                {
                    firstname = ProviderName[0];
                    lastname = ProviderName[1];
                    profiles = profileRepository.Get(p => p.PersonalDetail != null && (p.PersonalDetail.FirstName.Contains(firstname) && p.PersonalDetail.LastName.Contains(lastname)) && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties).ToList();

                }
                else if (ProviderName.Length > 2) {
                    firstname = ProviderName[0];
                    middlename = ProviderName[1];
                    lastname = ProviderName[2];
                    profiles = profileRepository.Get(p => p.PersonalDetail != null && (p.PersonalDetail.FirstName.Contains(firstname) && p.PersonalDetail.MiddleName.Contains(middlename) && p.PersonalDetail.LastName.Contains(lastname)) && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties).ToList();

                }
                //foreach (var data in ProviderName)
                //{
                //    if (flag == 0)
                //    {
                //        profiles = profileRepository.Get(p => p.PersonalDetail != null && (p.PersonalDetail.FirstName.Contains(data) || p.PersonalDetail.LastName.Contains(data) || (p.PersonalDetail.MiddleName != null && p.PersonalDetail.MiddleName.Contains(data))) && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties).ToList();
                //        flag = 1;
                //    }
                //    else
                //    {
                //        var temporaryProfiles1 = new List<Profile>();
                //        var temporaryProfiles = profileRepository.Get(p => p.PersonalDetail != null && (p.PersonalDetail.FirstName.Contains(data) || p.PersonalDetail.LastName.Contains(data) || (p.PersonalDetail.MiddleName != null && p.PersonalDetail.MiddleName.Contains(data))) && p.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), includeProperties).ToList();
                //        foreach (var data1 in temporaryProfiles)
                //        {
                //            int flagForProviderName = 0;
                //            foreach (var data2 in profiles)
                //            {
                //                if (data1.ProfileID == data2.ProfileID)
                //                {
                //                    flagForProviderName = 1;
                //                }
                //            }
                //            if (flagForProviderName == 0)
                //            {
                //                temporaryProfiles1.Add(data1);
                //            }

                //        }
                //        if (temporaryProfiles1.Count > 0)
                //        {
                //            foreach (var data3 in temporaryProfiles1)
                //            {
                //                profiles.Add(data3);
                //            }
                //        }

                //    }
                //}
                var providerTitle = "";
                foreach (var profile in profiles)
                {
                    if (profile.PersonalDetail != null && profile.PersonalDetail.ProviderTitles.Count > 0)
                    {
                        profile.PersonalDetail.ProviderTitles = profile.PersonalDetail.ProviderTitles.Where(s => s.Status == StatusType.Active.ToString()).ToList();
                       providerTitle = String.Join(", ", profile.PersonalDetail.ProviderTitles.Select(s => s.ProviderType.Title));
                    }
                    profile.PersonalDetail.ProviderTitles.ElementAt(0).ProviderType.Title = providerTitle;
                }
                

                return profiles;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
