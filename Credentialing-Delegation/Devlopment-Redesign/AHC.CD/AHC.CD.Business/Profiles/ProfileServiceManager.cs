using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    internal class ProfileServiceManager : IProfileServiceManager
    {
        IProfileRepository profileRepository = null;
        IUnitOfWork uow = null;

        public ProfileServiceManager(IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.uow = uow;
        }
        public List<ProviderDTO> GetAllProviders()
        {
            List<Profile> profiles = profileRepository.GetAll("SpecialtyDetails.Specialty.ProviderTypes,PersonalDetail.ProviderTitles.ProviderType").ToList();
            List<ProviderDTO> providers = new List<ProviderDTO>();
            foreach (var profile in profiles)
            {
                try
                {
                    var provider = new ProviderDTO();
                    provider.FirstName = profile.PersonalDetail.FirstName;
                    provider.LastName = profile.PersonalDetail.LastName;
                    provider.MiddleName = profile.PersonalDetail.MiddleName;
                    provider.ContactName = profile.PersonalDetail.FirstName + " " + (string.IsNullOrWhiteSpace(profile.PersonalDetail.MiddleName) ? null : (profile.PersonalDetail.MiddleName + " ")) + profile.PersonalDetail.LastName;
                    provider.FaxNumber = (profile.ContactDetail == null) ? null : ((profile.ContactDetail.PhoneDetails == null) ? null : profile.ContactDetail.PhoneDetails.Where(m => m.PhoneType.ToLower() == "fax").Select(m => m.PhoneNumber == null ? null : m.PhoneNumber).FirstOrDefault());
                    provider.NPI = profile.OtherIdentificationNumber == null ? null : profile.OtherIdentificationNumber.NPINumber;
                    provider.PhoneNumber = (profile.ContactDetail == null) ? null : ((profile.ContactDetail.PhoneDetails == null) ? null : profile.ContactDetail.PhoneDetails.Where(m => (m.PhoneType.ToLower() == "mobile") || (m.PhoneType.ToLower() == "home")).Select(m => m.PhoneNumber == null ? null : m.PhoneNumber).FirstOrDefault());
                    provider.Speciality = (profile.SpecialtyDetails == null) ? null : ((string.Join(",", profile.SpecialtyDetails.Select(m => m.Specialty.Name == null ? null : m.Specialty.Name).ToList())));
                    provider.SSN = profile.PersonalIdentification == null ? null : profile.PersonalIdentification.SSN;
                    provider.Type = string.Join(",", profile.PersonalDetail.ProviderTitles == null ? null : profile.PersonalDetail.ProviderTitles.Select(m => m.ProviderType.Title).ToList());

                    providers.Add(provider);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return providers;
        }
    }
}
