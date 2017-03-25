using AHC.CD.Data.ADO.ProviderService;
using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.ContractGrid;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.Credentialing.DTO;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    internal class ProfileServiceManager : IProfileServiceManager
    {
        IProfileRepository profileRepository = null;
        IUnitOfWork uow = null;
        IProviderRepository ProviderRepo = null;
        IContractGridRepository Contractrepo = null;

        public ProfileServiceManager(IUnitOfWork uow, IProviderRepository iProviderRepo, IContractGridRepository Contractrepo)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.uow = uow;
            this.ProviderRepo = iProviderRepo;
            this.Contractrepo = Contractrepo;
        }
        public List<ProviderDTO> GetAllProviders()
        {
            List<Profile> profiles = profileRepository.GetAll("SpecialtyDetails.Specialty.ProviderTypes,PersonalDetail.ProviderTitles.ProviderType,PracticeLocationDetails,PracticeLocationDetails.Facility,PracticeLocationDetails.BusinessOfficeManagerOrStaff").ToList();
            List<ProviderDTO> providers = new List<ProviderDTO>();
            foreach (var profile in profiles)
            {
                var ProviderPraticeLocation = profile.PracticeLocationDetails.Where(x => x.IsPrimary == "YES").FirstOrDefault();
                try
                {
                    var provider = new ProviderDTO();
                    provider.FirstName = profile.PersonalDetail.FirstName;
                    provider.LastName = profile.PersonalDetail.LastName;
                    provider.MiddleName = profile.PersonalDetail.MiddleName;
                    //provider.ContactName = profile.PracticeLocationDetails == null ? null : ProviderPraticeLocation == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff == null ? null : (ProviderPraticeLocation.BusinessOfficeManagerOrStaff.FirstName + " " + (string.IsNullOrWhiteSpace(ProviderPraticeLocation.BusinessOfficeManagerOrStaff.MiddleName) ? null : (ProviderPraticeLocation.BusinessOfficeManagerOrStaff.MiddleName + " ")) + ProviderPraticeLocation.BusinessOfficeManagerOrStaff.LastName);
                    provider.ContactName = profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.LastName;
                    provider.FaxNumber = (profile.ContactDetail == null) ? null : ((profile.ContactDetail.PhoneDetails == null) ? null : profile.ContactDetail.PhoneDetails.Where(m => m.PhoneType.ToLower() == "fax").Select(m => m.PhoneNumber == null ? null : m.PhoneNumber).FirstOrDefault());
                    provider.NPI = profile.OtherIdentificationNumber == null ? null : profile.OtherIdentificationNumber.NPINumber;
                    provider.PhoneNumber = (profile.ContactDetail == null) ? null : ((profile.ContactDetail.PhoneDetails == null) ? null : profile.ContactDetail.PhoneDetails.Where(m => (m.PhoneType.ToLower() == "mobile") || (m.PhoneType.ToLower() == "home")).Select(m => m.PhoneNumber == null ? null : m.PhoneNumber).FirstOrDefault());
                    provider.Speciality = (profile.SpecialtyDetails == null) ? null : ((string.Join(",", profile.SpecialtyDetails.Select(m => m.SpecialtyPreference == "Primary" && m.Specialty.Name == null ? null : m.Specialty.Name).ToList())));
                    provider.SSN = profile.PersonalIdentification == null ? null : profile.PersonalIdentification.SSN;
                    provider.Type = string.Join(",", profile.PersonalDetail.ProviderTitles == null ? null : profile.PersonalDetail.ProviderTitles.Select(m => m.ProviderType.Title).ToList());
                    provider.OfficeManagerEmailID = profile.PracticeLocationDetails == null ? null : ProviderPraticeLocation == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff.EmailAddress;
                    provider.OfficeManagerPhoneNumber = profile.PracticeLocationDetails == null ? null : ProviderPraticeLocation == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff.MobileNumber;
                    provider.OfficeManagerFaxNumber = profile.PracticeLocationDetails == null ? null : ProviderPraticeLocation == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff.FaxNumber;
                    provider.ProviderPracticeLocationAddress = new ProviderPracticeLocationAddressDTO();
                    if (ProviderPraticeLocation != null && ProviderPraticeLocation.Facility != null)
                    {

                        provider.ProviderPracticeLocationAddress.Building = ProviderPraticeLocation.Facility.Building == null ? null : ProviderPraticeLocation.Facility.Building;
                        provider.ProviderPracticeLocationAddress.City = ProviderPraticeLocation.Facility.City == null ? null : ProviderPraticeLocation.Facility.City;
                        provider.ProviderPracticeLocationAddress.Country = ProviderPraticeLocation.Facility.Country == null ? null : ProviderPraticeLocation.Facility.Country;
                        provider.ProviderPracticeLocationAddress.County = ProviderPraticeLocation.Facility.County == null ? null : ProviderPraticeLocation.Facility.County;
                        provider.ProviderPracticeLocationAddress.EmailAddress = ProviderPraticeLocation.Facility.EmailAddress == null ? null : ProviderPraticeLocation.Facility.EmailAddress;
                        provider.ProviderPracticeLocationAddress.FaxNumber = ProviderPraticeLocation.Facility.FaxNumber == null ? null : ProviderPraticeLocation.Facility.FaxNumber;
                        provider.ProviderPracticeLocationAddress.MobileNumber = ProviderPraticeLocation.Facility.MobileNumber == null ? null : ProviderPraticeLocation.Facility.MobileNumber;
                        provider.ProviderPracticeLocationAddress.State = ProviderPraticeLocation.Facility.State == null ? null : ProviderPraticeLocation.Facility.State;
                        provider.ProviderPracticeLocationAddress.Street = ProviderPraticeLocation.Facility.Street == null ? null : ProviderPraticeLocation.Facility.Street;
                        provider.ProviderPracticeLocationAddress.ZipCode = ProviderPraticeLocation.Facility.ZipCode == null ? null : ProviderPraticeLocation.Facility.ZipCode;
                    }
                    providers.Add(provider);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return providers;
        }
        public List<ProviderDetailsDTO> GetAllProviders1()
        {
            return ProviderRepo.GetAllProviders1();
        }
        
        public List<ProviderDTO> GetAllUltimateProviders()
        {
           
          List<int> UltimateProfileIds=profileRepository.GetProfileIDsOfUltimateProviders().Distinct().ToList();
          List<Profile> profiles = profileRepository.Get(x => UltimateProfileIds.Contains(x.ProfileID), "SpecialtyDetails.Specialty.ProviderTypes,PersonalDetail.ProviderTitles.ProviderType,PracticeLocationDetails,PracticeLocationDetails.Facility,PracticeLocationDetails.BusinessOfficeManagerOrStaff").ToList();
            List<ProviderDTO> providers = new List<ProviderDTO>();
            foreach (var profile in profiles)
            {
                var ProviderPraticeLocation = profile.PracticeLocationDetails.Where(x => x.IsPrimary == "YES" && x.Status==AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).FirstOrDefault();
                try
                {
                    var provider = new ProviderDTO();
                    provider.FirstName = profile.PersonalDetail!=null?profile.PersonalDetail.FirstName:null;
                    provider.LastName = profile.PersonalDetail!=null?profile.PersonalDetail.LastName:null;
                    provider.MiddleName = profile.PersonalDetail!=null?profile.PersonalDetail.MiddleName:null;
                    //provider.ContactName = profile.PracticeLocationDetails == null ? null : ProviderPraticeLocation == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff == null ? null : (ProviderPraticeLocation.BusinessOfficeManagerOrStaff.FirstName + " " + (string.IsNullOrWhiteSpace(ProviderPraticeLocation.BusinessOfficeManagerOrStaff.MiddleName) ? null : (ProviderPraticeLocation.BusinessOfficeManagerOrStaff.MiddleName + " ")) + ProviderPraticeLocation.BusinessOfficeManagerOrStaff.LastName);
                    provider.ContactName = profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.LastName;
                    provider.FaxNumber = (profile.ContactDetail == null) ? null : ((profile.ContactDetail.PhoneDetails == null) ? null : profile.ContactDetail.PhoneDetails.Where(m => m.PhoneType.ToLower() == "fax").Select(m => m.PhoneNumber == null ? null : m.PhoneNumber).FirstOrDefault());
                    provider.NPI = profile.OtherIdentificationNumber == null ? null : profile.OtherIdentificationNumber.NPINumber;
                    provider.PhoneNumber = (profile.ContactDetail == null) ? null : ((profile.ContactDetail.PhoneDetails == null) ? null : profile.ContactDetail.PhoneDetails.Where(m => (m.PhoneType.ToLower() == "mobile") || (m.PhoneType.ToLower() == "home")).Select(m => m.PhoneNumber == null ? null : m.PhoneNumber).FirstOrDefault());
                    provider.Speciality = (profile.SpecialtyDetails == null) ? null : ((string.Join(",", profile.SpecialtyDetails.Select(m => m.SpecialtyPreference == "Primary" && m.Specialty.Name == null ? null : m.Specialty.Name).ToList())));
                    provider.SSN = profile.PersonalIdentification == null ? null : profile.PersonalIdentification.SSN;
                    provider.Type = string.Join(",", profile.PersonalDetail.ProviderTitles == null ? null : profile.PersonalDetail.ProviderTitles.Select(m => m.ProviderType.Title).ToList());
                    provider.OfficeManagerEmailID = profile.PracticeLocationDetails == null ? null : ProviderPraticeLocation == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff.EmailAddress;
                    provider.OfficeManagerPhoneNumber = profile.PracticeLocationDetails == null ? null : ProviderPraticeLocation == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff.MobileNumber;
                    provider.OfficeManagerFaxNumber = profile.PracticeLocationDetails == null ? null : ProviderPraticeLocation == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff.FaxNumber;
                    provider.ProviderPracticeLocationAddress = new ProviderPracticeLocationAddressDTO();
                    if (ProviderPraticeLocation != null && ProviderPraticeLocation.Facility != null)
                    {

                        provider.ProviderPracticeLocationAddress.Building = ProviderPraticeLocation.Facility.Building == null ? null : ProviderPraticeLocation.Facility.Building;
                        provider.ProviderPracticeLocationAddress.City = ProviderPraticeLocation.Facility.City == null ? null : ProviderPraticeLocation.Facility.City;
                        provider.ProviderPracticeLocationAddress.Country = ProviderPraticeLocation.Facility.Country == null ? null : ProviderPraticeLocation.Facility.Country;
                        provider.ProviderPracticeLocationAddress.County = ProviderPraticeLocation.Facility.County == null ? null : ProviderPraticeLocation.Facility.County;
                        provider.ProviderPracticeLocationAddress.EmailAddress = ProviderPraticeLocation.Facility.EmailAddress == null ? null : ProviderPraticeLocation.Facility.EmailAddress;
                        provider.ProviderPracticeLocationAddress.FaxNumber = ProviderPraticeLocation.Facility.FaxNumber == null ? null : ProviderPraticeLocation.Facility.FaxNumber;
                        provider.ProviderPracticeLocationAddress.MobileNumber = ProviderPraticeLocation.Facility.MobileNumber == null ? null : ProviderPraticeLocation.Facility.MobileNumber;
                        provider.ProviderPracticeLocationAddress.State = ProviderPraticeLocation.Facility.State == null ? null : ProviderPraticeLocation.Facility.State;
                        provider.ProviderPracticeLocationAddress.Street = ProviderPraticeLocation.Facility.Street == null ? null : ProviderPraticeLocation.Facility.Street;
                        provider.ProviderPracticeLocationAddress.ZipCode = ProviderPraticeLocation.Facility.ZipCode == null ? null : ProviderPraticeLocation.Facility.ZipCode;
                    }
                    providers.Add(provider);
                }
                catch (Exception)
                {

                    throw;
                }

            }
            return providers;
        }




        public ProviderServiceDTO GetAllProvidersDataByNPI(string NPINumber)
        {
            var includeProperties = new string[]{
                    //Specialty
                    "SpecialtyDetails.Specialty",

                    //Personal Details Information
                    "PersonalDetail",

                    //OtherIdentification Number 
                    "OtherIdentificationNumber",

                    //ContactDetails
                    "ContactDetail",
                    "ContactDetail.PhoneDetails",
                    "ContactDetail.EmailIDs",


                    //PracticeLoctionDetails
                    "PracticeLocationDetails",
                    "PracticeLocationDetails.Facility",
                    "PracticeLocationDetails.BusinessOfficeManagerOrStaff"
                };
            Profile profile = profileRepository.Find(x => x.OtherIdentificationNumber.NPINumber == NPINumber, includeProperties);
            ProviderServiceDTO providerDTOForAddressDetail = new ProviderServiceDTO();
            if (profile != null)
            {
                var ProviderPraticeLocation = profile.PracticeLocationDetails.Where(x => x.IsPrimary == "YES").FirstOrDefault();
                //providerDTOForAddressDetail.ContactName = profile.PracticeLocationDetails == null ? null : ProviderPraticeLocation == null ? null : ProviderPraticeLocation.BusinessOfficeManagerOrStaff == null ? null : (ProviderPraticeLocation.BusinessOfficeManagerOrStaff.FirstName + " " + (string.IsNullOrWhiteSpace(ProviderPraticeLocation.BusinessOfficeManagerOrStaff.MiddleName) ? null : (ProviderPraticeLocation.BusinessOfficeManagerOrStaff.MiddleName + " ")) + ProviderPraticeLocation.BusinessOfficeManagerOrStaff.LastName);
                providerDTOForAddressDetail.ContactName = profile.PersonalDetail.FirstName + " " + profile.PersonalDetail.LastName;
                providerDTOForAddressDetail.PhoneNumber = (profile.ContactDetail == null) ? null : ((profile.ContactDetail.PhoneDetails == null) ? null : profile.ContactDetail.PhoneDetails.Where(m => (m.PhoneType.ToLower() == "mobile") || (m.PhoneType.ToLower() == "home")).Select(m => m.PhoneNumber == null ? null : m.PhoneNumber).FirstOrDefault());
                providerDTOForAddressDetail.FaxNumber = (profile.ContactDetail == null) ? null : ((profile.ContactDetail.PhoneDetails == null) ? null : profile.ContactDetail.PhoneDetails.Where(m => m.PhoneType.ToLower() == "fax").Select(m => m.PhoneNumber == null ? null : m.PhoneNumber).FirstOrDefault());
                providerDTOForAddressDetail.EmailID = (profile.ContactDetail == null) ? null : ((profile.ContactDetail.EmailIDs == null) ? null : profile.ContactDetail.EmailIDs.Select(m => m.EmailAddress == null ? null : m.EmailAddress).FirstOrDefault());
                providerDTOForAddressDetail.Specialties = (from specialty in profile.SpecialtyDetails == null ? null : profile.SpecialtyDetails where profile.SpecialtyDetails != null select new { Name = specialty.Specialty.Name, Taxonomy = specialty.Specialty.TaxonomyCode }).ToList<object>();
                providerDTOForAddressDetail.CurrentPraticeLocation = (from PracticeLocation in profile.PracticeLocationDetails == null ? null : profile.PracticeLocationDetails.Where(x => x.CurrentlyPracticingYesNoOption == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES) where PracticeLocation.Facility != null select new { Building = PracticeLocation.Facility.Building, Street = PracticeLocation.Facility.Street, Country = PracticeLocation.Facility.Country, State = PracticeLocation.Facility.State, County = PracticeLocation.Facility.County, City = PracticeLocation.Facility.City, ZipCode = PracticeLocation.Facility.ZipCode, FaxNumber = PracticeLocation.Facility.FaxNumber }).ToList<object>();
                providerDTOForAddressDetail.PlanNames = GetPlansByProfileId(profile.ProfileID);
                providerDTOForAddressDetail.ProviderPracticeLocationAddress = new ProviderPracticeLocationAddressDTO();
                if (ProviderPraticeLocation != null && ProviderPraticeLocation.Facility != null)
                {
                    providerDTOForAddressDetail.ProviderPracticeLocationAddress.Building = ProviderPraticeLocation.Facility.Building == null ? null : ProviderPraticeLocation.Facility.Building;
                    providerDTOForAddressDetail.ProviderPracticeLocationAddress.City = ProviderPraticeLocation.Facility.City == null ? null : ProviderPraticeLocation.Facility.City;
                    providerDTOForAddressDetail.ProviderPracticeLocationAddress.Country = ProviderPraticeLocation.Facility.Country == null ? null : ProviderPraticeLocation.Facility.Country;
                    providerDTOForAddressDetail.ProviderPracticeLocationAddress.County = ProviderPraticeLocation.Facility.County == null ? null : ProviderPraticeLocation.Facility.County;
                    providerDTOForAddressDetail.ProviderPracticeLocationAddress.EmailAddress = ProviderPraticeLocation.Facility.EmailAddress == null ? null : ProviderPraticeLocation.Facility.EmailAddress;
                    providerDTOForAddressDetail.ProviderPracticeLocationAddress.FaxNumber = ProviderPraticeLocation.Facility.FaxNumber == null ? null : ProviderPraticeLocation.Facility.FaxNumber;
                    providerDTOForAddressDetail.ProviderPracticeLocationAddress.MobileNumber = ProviderPraticeLocation.Facility.MobileNumber == null ? null : ProviderPraticeLocation.Facility.MobileNumber;
                    providerDTOForAddressDetail.ProviderPracticeLocationAddress.State = ProviderPraticeLocation.Facility.State == null ? null : ProviderPraticeLocation.Facility.State;
                    providerDTOForAddressDetail.ProviderPracticeLocationAddress.Street = ProviderPraticeLocation.Facility.Street == null ? null : ProviderPraticeLocation.Facility.Street;
                    providerDTOForAddressDetail.ProviderPracticeLocationAddress.ZipCode = ProviderPraticeLocation.Facility.ZipCode == null ? null : ProviderPraticeLocation.Facility.ZipCode;
                }
            }
            return providerDTOForAddressDetail;
        }

        private List<string> GetPlansByProfileId(int profileId)
        {
            var contractgridRepo = uow.GetGenericRepository<ContractGrid>();
            List<string> res = (from planname in contractgridRepo.Get(x => x.CredentialingInfo.ProfileID == profileId, "CredentialingInfo,CredentialingInfo.Plan") select planname.CredentialingInfo.Plan.PlanName).ToList<string>();
            return res.Distinct().ToList<string>();
        }
        public int GetProfileIDByNPI(string NPI)
        {

            return profileRepository.Get(x => x.OtherIdentificationNumber.NPINumber == NPI, "OtherIdentificationNumber").Select(x => x.ProfileID).FirstOrDefault();
           //return i;
        }
        public async Task<object> GetProviderBriefProfileByNPI(string NPI)
        {
            try
            {
                var includeProperties = new string[]
                {
                   "PersonalDetail",
                  
                   "HomeAddresses",
                   "ContactDetail","ContactDetail.PhoneDetails",
                   "SpecialtyDetails","SpecialtyDetails.Specialty",
                   "BirthInformation",
                   "PracticeLocationDetails",
                   "PracticeLocationDetails.Facility",
                   
                   "OtherIdentificationNumber",
                   "ContractInfoes","ContractInfoes.ContractGroupInfoes","ContractInfoes.ContractGroupInfoes.PracticingGroup","ContractInfoes.ContractGroupInfoes.PracticingGroup.Group" 
                };
                Profile profile = await profileRepository.FindAsync(x => x.OtherIdentificationNumber.NPINumber == NPI, includeProperties);
                if (profile != null)
                {
                    List<object> HomeAddress = new List<object>();
                    if (profile.HomeAddresses != null)
                    {
                        foreach (var address in profile.HomeAddresses.Where(x => x.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()))
                        {
                            var obj = new { AddressLine1 = address.UnitNumber, AddressLine2 = address.Street, City = address.City, Country = address.Country, State = address.State, ZipCode = address.ZipCode, County = address.County };
                            HomeAddress.Add(obj);
                        }
                    }
                    int PhoneNumberDetailsCount = 0;
                    string PhoneNumber = null;
                    if (profile.ContactDetail != null)
                    {
                        foreach (var number in profile.ContactDetail.PhoneDetails.Where(x => x.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()))
                        {
                            if (PhoneNumberDetailsCount == 0 )
                            {
                                PhoneNumber = number.PhoneNumber;
                                if(number.PhoneNumber!=null)
                                {
                                    PhoneNumberDetailsCount++;
                                }
                            }
                            else
                            {
                                PhoneNumber += number.PhoneNumber!=null?"," + number.PhoneNumber:"";
                            }
                            

                        }
                    }
                    int SpecialityDetailsCount = 0;
                    string Speciality = null;
                    foreach (var number in profile.SpecialtyDetails.Where(x=>x.Status==AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()).Where(x=>x.PreferenceType==AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary))
                    {
                        if(number.Specialty!=null)
                        {
                        if (SpecialityDetailsCount == 0  )
                        {
                            Speciality = number.Specialty.Name;
                            if(number.Specialty.Name!=null)
                            {
                                SpecialityDetailsCount++;
                            }
                        }
                        else
                        {
                            Speciality += number.Specialty.Name!=null?"," + number.Specialty.Name:"";
                        }
                        
                        }

                    }
                    int Age = 0;
                    if (profile.BirthInformation != null && profile.BirthInformation.DateOfBirth!=null)
                    {
                        string[] dob = profile.BirthInformation.DateOfBirth.Split('/');
                        string[] dob1 = dob[2].Split(' ');
                        int year = int.Parse(dob1[0]);

                        Age = DateTime.Now.Date.Year - year;
                    }
                    List<object> PracticeLocations = new List<object>();
                    foreach (var practicelocation in profile.PracticeLocationDetails.Where(x=>x.Status==AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()))
                    {
                        var obj = new { PracticeName = practicelocation.PracticeLocationCorporateName, FacilityName = practicelocation.Facility.FacilityName, Street = practicelocation.Facility.Street, Building = practicelocation.Facility.Building, City = practicelocation.Facility.City, State = practicelocation.Facility.State, Country = practicelocation.Facility.Country, County = practicelocation.Facility.County, ZipCode = practicelocation.Facility.ZipCode };
                        PracticeLocations.Add(obj);
                    }
                    List<string> IPAs = new List<string>();
                    string IPA = null;
                    int IPACount = 0;
                    foreach (var names in profile.ContractInfoes.Where(x=>x.ContractStatus==AHC.CD.Entities.MasterData.Enums.ContractStatus.Accepted.ToString()))
                    {
                        foreach (var name in names.ContractGroupInfoes.Where(x => x.ContractGroupStatus==AHC.CD.Entities.MasterData.Enums.ContractGroupStatus.Accepted.ToString()))
                        {
                           // IPAs.Add(name.PracticingGroup.Group.Name);
                            if (name.PracticingGroup != null)
                            {
                                if (name.PracticingGroup.Group != null)
                                {
                                    if (IPACount == 0)
                                    {
                                        IPA = name.PracticingGroup.Group.Name;
                                        if(name.PracticingGroup.Group.Name!=null)
                                        {
                                            IPACount++;
                                        }
                                    }
                                    else
                                    {
                                        IPA += name.PracticingGroup.Group.Name != null ? "," + name.PracticingGroup.Group.Name : "";
                                    }
                                   
                                }

                            }
                        
                        }
                    }
                   
                   
                    return new
                    {
                        FullName = profile.PersonalDetail!=null?profile.PersonalDetail.FirstName + " " + (string.IsNullOrWhiteSpace(profile.PersonalDetail.MiddleName) ? null : (profile.PersonalDetail.MiddleName + " ")) + profile.PersonalDetail.LastName:null,
                        HomeAddresses = HomeAddress,
                        PhoneNumber = PhoneNumber,
                        Specialty = Speciality,
                        Age = Age,
                        PracticeLocations = PracticeLocations,
                        NPI = profile.OtherIdentificationNumber!=null?profile.OtherIdentificationNumber.NPINumber:null,
                        Gender = profile.PersonalDetail!=null?profile.PersonalDetail.Gender:null,
                        IPA = IPA
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        
        }
        public async Task<List<ProviderDetailsDTO>> GetAllProviderDetails()
        {
            try
            {
                string IncludeProperties = "PersonalDetail,OtherIdentificationNumber,BirthInformation,PracticeLocationDetails,PracticeLocationDetails.Facility,SpecialtyDetails,SpecialtyDetails.Specialty,StateLicenses,StateLicenses.StateLicenseStatus";
                IEnumerable<Profile> profiles1 = await profileRepository.GetAsync(x=>x.StatusType==AHC.CD.Entities.MasterData.Enums.StatusType.Active,IncludeProperties);
                List<Profile> profiles = profiles1.ToList();//.Where(x => x.StatusType == AHC.CD.Entities.MasterData.Enums.StatusType.Active).ToList();
                List<ProviderDetailsDTO> ProviderDetails = new List<ProviderDetailsDTO>();

                string Format = "M/d/yyyy hh:mm:ss tt";
                if (profiles != null)
                {

                    foreach (var profile in profiles)
                    {


                        //List<string> StateLicenses = profile.StateLicenses != null ? profile.StateLicenses.Where(x => x.StatusType == AHC.CD.Entities.MasterData.Enums.StatusType.Active).Where(x => x.StateLicenseStatus.StatusType == AHC.CD.Entities.MasterData.Enums.StatusType.Active).Select(x => x.LicenseNumber).ToList() : null;
                        List<Specialty> Specialities = profile.SpecialtyDetails != null ? (from speciality in profile.SpecialtyDetails.Where(x => x.StatusType == AHC.CD.Entities.MasterData.Enums.StatusType.Active).Where(x => x.PreferenceType == AHC.CD.Entities.MasterData.Enums.PreferenceType.Primary).Select(x => x.Specialty) select speciality).ToList() : null;
                        Specialty Speciality = Specialities.FirstOrDefault();
                        ProviderDetailsDTO details = new ProviderDetailsDTO()
                        {
                            FirstName = profile.PersonalDetail != null ? profile.PersonalDetail.FirstName : null,
                            MiddleName = profile.PersonalDetail != null ? profile.PersonalDetail.MiddleName : null,
                            LastName = profile.PersonalDetail != null ? profile.PersonalDetail.LastName : null,

                            NPI = profile.OtherIdentificationNumber != null ? profile.OtherIdentificationNumber.NPINumber : null,
                            Speciality = Speciality != null ? Speciality.Name : null,
                            Taxonomy = Speciality != null ? Speciality.TaxonomyCode : null,


                           // StateLicenseNumbers = profile.StateLicenses != null ? profile.StateLicenses.Where(x => x.StatusType == AHC.CD.Entities.MasterData.Enums.StatusType.Active).Select(x => x.LicenseNumber).ToList() : null,

                            PracticeLocations = profile.PracticeLocationDetails != null ? (from practicelocation in profile.PracticeLocationDetails
                                                                                           where practicelocation.PrimaryYesNoOption == AHC.CD.Entities.MasterData.Enums.YesNoOption.YES && practicelocation.StatusType == AHC.CD.Entities.MasterData.Enums.StatusType.Active
                                                                                           select new PracticeLocationDTO() { PracticeLocationName = practicelocation.PracticeLocationCorporateName, AddressLine1 = practicelocation.Facility != null ? practicelocation.Facility.Building : null, AddressLine2 = practicelocation.Facility != null ? practicelocation.Facility.Street : null, City = practicelocation.Facility != null ? practicelocation.Facility.City : null, Country = practicelocation.Facility != null ? practicelocation.Facility.Country : null, CountryCodeTelephone = practicelocation.Facility != null ? practicelocation.Facility.CountryCodeTelephone : null, County = practicelocation.Facility != null ? practicelocation.Facility.County : null, EmailAddress = practicelocation.Facility != null ? practicelocation.Facility.EmailAddress : null, FacilityName = practicelocation.Facility != null ? practicelocation.Facility.FacilityName : null, MobileNumber = practicelocation.Facility != null ? practicelocation.Facility.MobileNumber : null, State = practicelocation.Facility != null ? practicelocation.Facility.State : null, Telephone = practicelocation.Facility != null ? practicelocation.Facility.Telephone : null, ZipCode = practicelocation.Facility != null ? practicelocation.Facility.ZipCode : null }).ToList() : null

                        };
                        if (profile.BirthInformation != null)
                        {
                            try
                            {
                                details.DOB = DateTime.ParseExact(profile.BirthInformation.DateOfBirth, Format, CultureInfo.InvariantCulture);
                            }
                            catch (Exception)
                            {

                                details.DOB = DateTime.Parse(profile.BirthInformation.DateOfBirth);


                            }
                            details.DOB = DateTime.Parse(details.DOB.ToString());
                        }

                        ProviderDetails.Add(details);
                    }
                    return ProviderDetails;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ProviderDTOForUM> NewUMService()
        {
            return ProviderRepo.NewUMService();
        }
        //Helper Methods
       
        public List<string> GetNPIByName(string Name)
        {
            return profileRepository.Get(x => x.PersonalDetail.FirstName.Contains(Name) || x.PersonalDetail.LastName.Contains(Name) || x.PersonalDetail.MiddleName.Contains(Name)).Select(x => x.OtherIdentificationNumber.NPINumber).ToList();

        }
        public object GetAllExpiriesForAProvider(int ProfileID)
        {
            return profileRepository.GetAllExpiriesForAProvider(ProfileID);
        }
        public object GetAccountInfo(int ProfileID)
        {
            return profileRepository.GetAccountInfo(ProfileID);
        }
        public object GetContractsForAprovider(int profileID)
        {
            return Contractrepo.GetContractsForAproviderGropedbyZipCode(profileID);
        }
        public object GetProfileIDByEmail(string EmailID)
        {
            return profileRepository.GetProfileIDByEmail(EmailID);
        }
        public object GetAllExpiryDates(int ProfileID)
        {
            return profileRepository.GetAllExpiryDates(ProfileID);
        }
        public int GetCountOfPlansForAProvider(int ProfileID)
        {
            return profileRepository.GetCountOfPlansForAProvider(ProfileID);
        }
        public int GetCountOfActiveContractsForaProvider(int ProfiileID)
        {
            return Contractrepo.GetCountOfActiveContractsForaProvider(ProfiileID);
        }
        public int GetCDUserIDByProfileID(int ProfileID)
        {
            return profileRepository.GetCDUserIDByProfileID(ProfileID);
        }
    }
}
