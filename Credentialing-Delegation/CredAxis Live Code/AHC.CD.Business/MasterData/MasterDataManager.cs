using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Entities.Location;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Accessibility;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Service;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using AHC.CD.Entities.MasterProfile.CredentialingRequest;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Entities.MasterProfile.ProfileReviewSection;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.MasterData
{
    internal class MasterDataManager : IMasterDataManager
    {
        private readonly IUnitOfWork uow = null;
        private readonly IRepositoryManager repositoryManager = null;
        private readonly IProfileRepository profileRepository = null;

        public MasterDataManager(IRepositoryManager repositoryManager, IUnitOfWork uow)
        {
            this.profileRepository = uow.GetProfileRepository();
            this.repositoryManager = repositoryManager;
            this.uow = uow;
        }

        public async Task<IEnumerable<Group>> GetAllGroupsAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<Group>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(Group obj1, Group obj2) { return obj1.Name.CompareTo(obj2.Name); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<IEnumerable<OrganizationGroup>> GetAllOrganizationGroupsAsync()
        //{
        //    try
        //    {
        //        var data = await repositoryManager.GetAsync<OrganizationGroup>(s => s.Status.Equals(StatusType.Active.ToString()));
        //        var sortData = data.ToList();
        //        sortData.Sort(delegate(OrganizationGroup obj1, OrganizationGroup obj2) { return obj1.GroupName.CompareTo(obj2.GroupName); });

        //        return sortData;
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public async Task<IEnumerable<Entities.MasterData.Tables.StateLicenseStatus>> GetAllLicenseStatusAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<StateLicenseStatus>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(StateLicenseStatus obj1, StateLicenseStatus obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<LOB>> GetAllLOBsOfPlanContractByPlanIDAsync(int planID)
        {
            try
            {
                var data = await repositoryManager.GetAsync<PlanContract>(s => s.Status != StatusType.Inactive.ToString(), "PlanLOB.LOB");
                List<LOB> LOBData = new List<LOB>();
                foreach (var item in data)
                {
                    if (item.PlanLOB.PlanID == planID && !LOBData.Any(l => l.LOBID == item.PlanLOB.LOBID))
                    {
                        LOBData.Add(item.PlanLOB.LOB);
                    }
                }
                LOBData.Sort(delegate(LOB obj1, LOB obj2) { return obj1.LOBName.CompareTo(obj2.LOBName); });
                return LOBData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        // new method for getting lobs according to plan in SPA page
        public async Task<IEnumerable<LOB>> GetAllLOBsOfPlanByPlanIDAsync(int planID)
        {
            try
            {
                var data = await repositoryManager.GetAsync<Plan>(s => s.PlanID == planID && s.Status != StatusType.Inactive.ToString(), "PlanLOBs.LOB");
                List<LOB> LOBData = new List<LOB>();
                foreach (var item in data)
                {
                    LOBData = (from planlob in item.PlanLOBs
                               where planlob.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString()
                               select planlob.LOB).ToList();
                }
                LOBData.Sort(delegate(LOB obj1, LOB obj2) { return obj1.LOBName.CompareTo(obj2.LOBName); });
                return LOBData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.StaffCategory>> GetAllStaffCategoryAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<StaffCategory>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(StaffCategory obj1, StaffCategory obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.SpecialtyBoard>> GetAllspecialtyBoardAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<SpecialtyBoard>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(SpecialtyBoard obj1, SpecialtyBoard obj2) { return obj1.Name.CompareTo(obj2.Name); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.Specialty>> GetAllSpecialtyAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<Specialty>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(Specialty obj1, Specialty obj2) { return obj1.Name.CompareTo(obj2.Name); });
                sortData.Sort(delegate(Specialty obj3, Specialty obj4) { return obj3.Name.CompareTo(obj4.Name); });
                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.School>> GetAllSchoolAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<School>(s => s.Status.Equals(StatusType.Active.ToString()), "SchoolContactInfoes");
                //var data = await repositoryManager.GetAsync<School>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(School obj1, School obj2) { return obj1.Name.CompareTo(obj2.Name); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.QualificationDegree>> GetAllQualificationDegreeAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<QualificationDegree>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(QualificationDegree obj1, QualificationDegree obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.ProviderType>> GetAllProviderTypeAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<ProviderType>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(ProviderType obj1, ProviderType obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<FacilityPracticeType>> GetAllLocationPracticeTypeAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<FacilityPracticeType>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(FacilityPracticeType obj1, FacilityPracticeType obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Account.Service.FacilityServiceQuestion>> GetAllPracticeServiceQuestionAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<FacilityServiceQuestion>(s => s.Status.Equals(StatusType.Active.ToString()));

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<PracticeOpenStatusQuestion>> GetAllPracticeOpenStatusQuestionAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<PracticeOpenStatusQuestion>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Account.Accessibility.FacilityAccessibilityQuestion>> GetAllPracticeAccessibilityQuestionAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<FacilityAccessibilityQuestion>(s => s.Status.Equals(StatusType.Active.ToString()));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.MilitaryRank>> GetAllMilitaryRankAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<MilitaryRank>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(MilitaryRank obj1, MilitaryRank obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MilitaryRankBusinessModel>> GetAllMilitaryRanks()
        {
            try
            {
                var data = await repositoryManager.GetAsync<MilitaryRank>(s => s.Status.Equals(StatusType.Active.ToString()), "MilitaryBranches");

                var sortData = ConstructMilitaryRank(data);
                sortData.Sort(delegate(MilitaryRankBusinessModel obj1, MilitaryRankBusinessModel obj2) { return obj1.MilitaryRankTitle.CompareTo(obj2.MilitaryRankTitle); });

                return sortData;

            }
            catch (Exception)
            {

                throw;
            }
        }


        private List<MilitaryRankBusinessModel> ConstructMilitaryRank(IEnumerable<MilitaryRank> militaryRanks)
        {
            List<MilitaryRankBusinessModel> ranks = new List<MilitaryRankBusinessModel>();
            MilitaryRankBusinessModel military = null;
            foreach (var item in militaryRanks)
            {

                foreach (var branch in item.MilitaryBranches)
                {
                    military = new MilitaryRankBusinessModel();

                    military.MilitaryRankID = item.MilitaryRankID;
                    military.MilitaryRankTitle = item.Title;
                    military.MilitaryBranchID = branch.MilitaryBranchID;
                    military.MilitaryBranchTitle = branch.Title;
                    military.StatusType = item.StatusType;
                    military.Status = item.Status;
                    military.LastModifiedDate = item.LastModifiedDate;
                    ranks.Add(military);
                }

            }

            return ranks;
        }



        public async Task<IEnumerable<Entities.MasterData.Tables.MilitaryPresentDuty>> GetAllMilitaryPresentDutyAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<MilitaryPresentDuty>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(MilitaryPresentDuty obj1, MilitaryPresentDuty obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.MilitaryDischarge>> GetAllMilitaryDischargeAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<MilitaryDischarge>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(MilitaryDischarge obj1, MilitaryDischarge obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.MilitaryBranch>> GetAllMilitaryBranchAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<MilitaryBranch>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(MilitaryBranch obj1, MilitaryBranch obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.InsuranceCarrier>> GetAllInsuranceCarrierAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<InsuranceCarrier>(s => s.Status.Equals(StatusType.Active.ToString()), "InsuranceCarrierAddresses");
                var sortData = data.ToList();
                sortData.Sort(delegate(InsuranceCarrier obj1, InsuranceCarrier obj2) { return obj1.Name.CompareTo(obj2.Name); });
                Parallel.ForEach(sortData, (s) =>
                {
                    s.InsuranceCarrierAddresses.ToList().Sort(delegate(InsuranceCarrierAddress obj1, InsuranceCarrierAddress obj2) { return obj1.City.CompareTo(obj2.City); });
                });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.InsuranceCarrierAddress>> GetAllInsuranceCarrierAddressesAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<InsuranceCarrierAddress>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(InsuranceCarrierAddress obj1, InsuranceCarrierAddress obj2) { return obj1.City.CompareTo(obj2.City); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.HospitalContactPerson>> GetAllHospitalContactPersonAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<HospitalContactPerson>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(HospitalContactPerson obj1, HospitalContactPerson obj2) { return obj1.ContactPersonName.CompareTo(obj2.ContactPersonName); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.HospitalContactInfo>> GetAllHospitalContactInfoAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<HospitalContactInfo>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(HospitalContactInfo obj1, HospitalContactInfo obj2) { return obj1.City.CompareTo(obj2.City); });
                Parallel.ForEach(sortData, (s) =>
                {
                    s.HospitalContactPersons.ToList().Sort(delegate(HospitalContactPerson obj1, HospitalContactPerson obj2) { return obj1.ContactPersonName.CompareTo(obj2.ContactPersonName); });
                });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.Hospital>> GetAllHospitalAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<Hospital>(s => s.Status.Equals(StatusType.Active.ToString()), "HospitalContactInfoes, HospitalContactInfoes.HospitalContactPersons");
                var data = await repositoryManager.GetAsync<Hospital>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(Hospital obj1, Hospital obj2) { return obj1.HospitalName.CompareTo(obj2.HospitalName); });
                Parallel.ForEach(sortData, (s) =>
                {
                    s.HospitalContactInfoes.ToList().Sort(delegate(HospitalContactInfo obj1, HospitalContactInfo obj2) { return obj1.City.CompareTo(obj2.City); });
                    Parallel.ForEach(s.HospitalContactInfoes, (x) => { x.HospitalContactPersons.ToList().Sort(delegate(HospitalContactPerson obj1, HospitalContactPerson obj2) { return obj1.ContactPersonName.CompareTo(obj2.ContactPersonName); }); });
                });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.DEASchedule>> GetAllDEAScheduleAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<DEASchedule>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(DEASchedule obj1, DEASchedule obj2) { return obj1.ScheduleTitle.CompareTo(obj2.ScheduleTitle); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.Certification>> GetAllCertificationAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<Certification>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(Certification obj1, Certification obj2) { return obj1.Name.CompareTo(obj2.Name); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.AdmittingPrivilege>> GetAllAdmittingPrivilegeAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<AdmittingPrivilege>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(AdmittingPrivilege obj1, AdmittingPrivilege obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Tables.ProfileDisclosureQuestion>> GetAllProfileDisclosureQuestionAsync()
        {
            try
            {
                return await repositoryManager.GetAsync<ProfileDisclosureQuestion>(s => s.Status.Equals(StatusType.Active.ToString()), "Questions, Questions.QuestionCategory, Questions.QuestionTheme");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<VisaType>> GetAllVisaTypeAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<VisaType>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(VisaType obj1, VisaType obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<InsuaranceCompanyName>> GetAllInsuaranceCompanyNameAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<InsuaranceCompanyName>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(InsuaranceCompanyName obj1, InsuaranceCompanyName obj2) { return obj1.CompanyName.CompareTo(obj2.CompanyName); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<VisaStatus>> GetAllVisaStatusAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<VisaStatus>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(VisaStatus obj1, VisaStatus obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<Question>(s => s.Status.Equals(StatusType.Active.ToString()));
                var questions = data.OrderBy(q => q.QuestionCategoryId).ToList();
                //var sortData = data.ToList();
                //sortData.Sort(delegate(Question obj1, Question obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return questions;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<QuestionCategory>> GetAllQuestionCategoriesAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<QuestionCategory>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                //sortData.Sort(delegate(Question obj1, Question obj2) { return obj1.Title.CompareTo(obj2.Title); });

                return sortData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<LocationDetail>> GetLocationsByCityAsync(string city)
        {
            try
            {
                return await repositoryManager.GetAsync<LocationDetail>(ld => ld.City.Contains(city));
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async Task<IEnumerable<Specialty>> GetSpecializationsBySpecialityAsync(string speciality)
        //{
        //    try
        //    {
        //         return await repositoryManager.GetAsync<Specialty>(sp=>sp.Name.Contains(speciality));

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

        public async Task<IEnumerable<string>> GetAllStatesAsync()
        {
            try
            {
                //var data = await repositoryManager.GetAllAsync<LocationDetail>();
                //var states = data.GroupBy(u => u.State).Select(s => s.First().State).ToList();
                //states.Sort(delegate(string obj1, string obj2) { return obj1.CompareTo(obj2); });
                //return states;

                var USStates = from country in await repositoryManager.GetAllAsync<Country>()
                               from state in country.States
                               where country.Code == "US"
                               select state.Name;

                return USStates.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public async task<ienumerable<string>> getalltaxonomyasync()
        // {
        //     try
        //     {

        //     }
        // }

        public async Task<IEnumerable<LocationDetail>> GetCitiesAsync(string citySearch)
        {
            try
            {

                var locationDetails = from country in await repositoryManager.GetAllAsync<Country>()
                                      from state in country.States
                                      from city in state.Cities
                                      where city.Name.ToLower().StartsWith(citySearch.ToLower())
                                      select new LocationDetail()
                                      {
                                          City = city.Name,
                                          Country = country.Name,
                                          State = state.Name,
                                          StateCode = state.Code,
                                          CountryCode = country.Code
                                      };

                return locationDetails.ToList();


                //var countries = await repositoryManager.GetAllAsync<Country>();
                //var locationDetail = new List<LocationDetail>();

                //foreach (var country in countries)
                //{
                //    foreach (var state in country.States)
                //    {
                //        foreach (var city in state.Cities)
                //        {
                //            if(city.Name.ToLower().StartsWith(citySearch.ToLower()))
                //            {
                //                locationDetail.Add(new LocationDetail() { City = city.Name, Country = country.Name, State = state.Name, StateCode = state.Code, CountryCode = country.Code });
                //            }
                //        }
                //    }
                //}


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<LocationDetail>> GetCitiesAllAsync()
        {
            var locationDetails = from country in await repositoryManager.GetAllAsync<Country>()
                                  from state in country.States
                                  from city in state.Cities

                                  select new LocationDetail
                                  {
                                      City = city.Name,
                                      Country = country.Name,
                                      State = state.Name,
                                      StateCode = state.Code,
                                      CountryCode = country.Code
                                  };

            return locationDetails.ToList();
        }

        public async Task<IEnumerable<OrganizationGroup>> GetAllOrganizationGroupAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<OrganizationGroup>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(OrganizationGroup obj1, OrganizationGroup obj2) { return obj1.GroupName.CompareTo(obj2.GroupName); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<FacilityAccessibilityQuestion>> GetAllAccessibilityQuestionsAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<FacilityAccessibilityQuestion>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                //sortData.Sort(delegate(FacilityAccessibilityQuestion obj1, FacilityAccessibilityQuestion obj2) { return obj1.GroupName.CompareTo(obj2.GroupName); });

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<FacilityPracticeType>> GetAllPracticeTypesAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<FacilityPracticeType>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<FacilityServiceQuestion>> GetAllServiceQuestionsAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<FacilityServiceQuestion>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<PracticeOpenStatusQuestion>> GetAllOpenPracticeStatusQuestionsAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<PracticeOpenStatusQuestion>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<OrganizationGroup>> GetAllOrganizationGroupsAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<OrganizationGroup>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task InitData()
        {
            try
            {
                await repositoryManager.InitMasterData();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Account.Staff.Employee>> GetAllBusinessContactPersonAsync()
        {
            try
            {
                // var data = await repositoryManager.GetAsync<Employee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.Departments.Any(d => d.Department.Name.Equals("Operation")));
                var data = await repositoryManager.GetAsync<Employee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.Departments.Any(d => d.Department.Name.Equals("Operation")));
                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MasterEmployee>> GetAllMasterBusinessContactPersonAsync()
        {
            try
            {
                // var data = await repositoryManager.GetAsync<Employee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.Departments.Any(d => d.Department.Name.Equals("Operation")));
                var data = await repositoryManager.GetAsync<MasterEmployee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.Departments.Any(d => d.Department.Name.Equals("Operation")), "Departments.Department, Designations.Designation");
                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Account.Staff.Employee>> GetAllBillingContactAsync()
        {
            try
            {
                //var data = await repositoryManager.GetAsync<Employee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.Departments.Any(d => d.Department.Name.Equals("Billing")));
                var data = await repositoryManager.GetAsync<Employee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.Departments.Any(d => d.Department.Name.Equals("Billing")));
                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MasterEmployee>> GetAllMasterBillingContactPersonAsync()
        {
            try
            {
                //var data = await repositoryManager.GetAsync<Employee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.Departments.Any(d => d.Department.Name.Equals("Billing")));
                var data = await repositoryManager.GetAsync<MasterEmployee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString() && s.Departments.Any(d => d.Department.Name.Equals("Billing")));
                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllCredentialingContactAsync()
        {
            try
            {

                //var data = await repositoryManager.GetAsync<Employee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString());
                var data = await repositoryManager.GetAsync<Employee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString());
                var sortData = data.ToList();

                List<Employee> credentialingContacts = new List<Employee>();

                foreach (var item in sortData)
                {
                    if (item.Designations.Any(x => x.DesignationID == EmployeeDesignationValue.PrimaryCredentialingContactDesignationId))
                    {

                        credentialingContacts.Add(item);
                    }
                }
                return credentialingContacts;
            }

            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MasterEmployee>> GetAllMasterCredentialingContactPersonAsync()
        {
            try
            {

                //var data = await repositoryManager.GetAsync<Employee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString());
                var data = await repositoryManager.GetAsync<MasterEmployee>(s => s.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString());
                var sortData = data.ToList();

                List<MasterEmployee> credentialingContacts = new List<MasterEmployee>();

                foreach (var item in sortData)
                {
                    if (item.Designations.Any(x => x.DesignationID == EmployeeDesignationValue.PrimaryCredentialingContactDesignationId))
                    {

                        credentialingContacts.Add(item);
                    }
                }
                return credentialingContacts;
            }

            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<PracticePaymentAndRemittance>> GetAllPaymentAndRemittance()
        {
            try
            {
                //var data = await repositoryManager.GetAsync<PracticePaymentAndRemittance>(s => s.PaymentAndRemittancePerson.Departments.Any(d => d.Department.Name.Equals("Payment and Remmittance")) && s.PaymentAndRemittancePerson.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString(), "PaymentAndRemittancePerson");
                var data = await repositoryManager.GetAsync<PracticePaymentAndRemittance>(s => s.PaymentAndRemittancePerson.Departments.Any(d => d.Department.Name.Equals("Payment and Remmittance")) && s.PaymentAndRemittancePerson.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString(), "PaymentAndRemittancePerson");

                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MasterPracticePaymentRemittancePerson>> GetAllMasterPaymentRemittancePersonAsync()
        {
            try
            {
                //var data = await repositoryManager.GetAsync<PracticePaymentAndRemittance>(s => s.PaymentAndRemittancePerson.Departments.Any(d => d.Department.Name.Equals("Payment and Remmittance")) && s.PaymentAndRemittancePerson.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString(), "PaymentAndRemittancePerson");
                var data = await repositoryManager.GetAsync<MasterPracticePaymentRemittancePerson>(s => s.PaymentAndRemittancePerson.Departments.Any(d => d.Department.Name.Equals("Payment and Remmittance")) && s.PaymentAndRemittancePerson.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString(), "PaymentAndRemittancePerson");

                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<ProviderLevel>> GetAllProviderLevelsAsync()
        {
            try
            {
                var data = await repositoryManager.GetAllAsync<ProviderLevel>();
                var sortData = data.ToList();

                return data;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IEnumerable<Country>> GetAllCountriesAsync()
        {
            try
            {

                var data = await repositoryManager.GetAllAsync<Country>();
                var sortData = data.ToList();
                return sortData;
            }

            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.Credentialing.Plan>> GetAllPlanAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<Plan>(s => s.Status.Equals(StatusType.Active.ToString()), "ContactDetails,ContactDetails.ContactDetail,Locations,PlanLOBs,PlanLOBs.LOB,PlanLOBs.SubPlans,PlanLOBs.LOBContactDetails, PlanLOBs.LOBContactDetails.ContactDetail,PlanLOBs.LOBContactDetails.ContactDetail.PhoneDetails,PlanLOBs.LOBContactDetails.ContactDetail.EmailIDs,PlanLOBs.LOBContactDetails.ContactDetail.PreferredContacts,PlanLOBs.LOBAddressDetails");

                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.Credentialing.Plan>> GetAllPlanNamesAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<Plan>(s => s.Status.Equals(StatusType.Active.ToString()));

                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Entities.Credentialing.Plan>> GetAllInactivePlansAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<Plan>(s => s.Status.Equals(StatusType.Inactive.ToString()), "ContactDetails,ContactDetails.ContactDetail,Locations,PlanLOBs,PlanLOBs.LOB,PlanLOBs.SubPlans,PlanLOBs.LOBContactDetails, PlanLOBs.LOBContactDetails.ContactDetail,PlanLOBs.LOBAddressDetails");

                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.Credentialing.LOB>> GetAllLOBAsync()
        {
            try
            {
                var data = await repositoryManager.GetAsync<LOB>(s => s.Status.Equals(StatusType.Active.ToString()));

                var sortData = data.ToList();

                return sortData;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<IEnumerable<Plan>> GetAllPlans_ForBEAsync(List<string> Groups)
        {
            try
            {

                var data = await repositoryManager.GetAllAsync<Plan>();
                var sortData = data.ToList();

                List<Plan> credentialingPlans = new List<Plan>();

                //foreach (var item in sortData)
                //{

                //    foreach (var g in Groups)
                //    {
                //        if (item.PlanBEs.Any(x => x.Group.GroupName == g))
                //        {

                //            credentialingPlans.Add(item);
                //        }
                //    }
                //}
                return credentialingPlans;
            }

            catch (Exception)
            {

                throw;
            }
        }




        public async Task<IEnumerable<ProfileVerificationParameter>> GetAllProfileVerificationParameter()
        {
            try
            {
                var data = await repositoryManager.GetAsync<ProfileVerificationParameter>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(ProfileVerificationParameter obj1, ProfileVerificationParameter obj2) { return obj1.Title.CompareTo(obj2.Title); });
                return sortData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<VerificationLink>> GetAllVerificationLinks()
        {
            try
            {
                var data = await repositoryManager.GetAsync<VerificationLink>(s => s.Status.Equals(StatusType.Active.ToString()));
                var sortData = data.ToList();
                sortData.Sort(delegate(VerificationLink obj1, VerificationLink obj2) { return obj1.Name.CompareTo(obj2.Name); });
                return sortData;
            }
            catch (Exception)
            {
                throw;
            }
        }




        public async Task<List<int?>> GetAllProfileIDsFromCredentialingInfoIDsAsync(int[] ProviderIDArray)
        {
            IEnumerable<AHC.CD.Entities.Credentialing.Loading.CredentialingInfo> data = await repositoryManager.GetAsync<AHC.CD.Entities.Credentialing.Loading.CredentialingInfo>(s => s.Status.Equals(StatusType.Active.ToString()));
            List<int?> profileIDs = new List<int?>();
            foreach (var item in data.ToList())
            {
                foreach (int credInfoID in ProviderIDArray)
                {
                    if (item.CredentialingInfoID == credInfoID)
                    {
                        profileIDs.Add(item.ProfileID);
                    }
                }
            }
            return profileIDs;
        }

        public async Task<IEnumerable<EmailTemplate>> GetAllEmailTemplatesAsync()
        {
            IEnumerable<EmailTemplate> emailTemplates = null;
            try
            {
                emailTemplates = await repositoryManager.GetAsync<EmailTemplate>(e => e.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return emailTemplates;
        }


        public async Task<IEnumerable<Entities.MasterProfile.CredentialingRequest.CredentialingRequest>> GetAllCredentialingRequestAsync()
        {
            IEnumerable<CredentialingRequest> credentialingRequests = null;
            try
            {
                credentialingRequests = await repositoryManager.GetAsync<CredentialingRequest>(e => e.Status != Entities.MasterData.Enums.StatusType.Inactive.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return credentialingRequests;
        }


        public async Task<IEnumerable<ProfileSection>> GetAllProfileSectionsAsync()
        {
            IEnumerable<ProfileSection> profileSections = null;
            try
            {
                profileSections = await repositoryManager.GetAsync<ProfileSection>(e => e.Status == Entities.MasterData.Enums.StatusType.Active.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return profileSections;
        }

        public async Task<IEnumerable<Entities.MasterProfile.ProfileReviewSection.ProfileReviewSection>> GetAllNotApplicableSectoinAsync(int profileId)
        {
            IEnumerable<ProfileReviewSection> profileReviewSections = null;
            try
            {
                profileReviewSections = await repositoryManager.GetAsync<ProfileReviewSection>(e => e.ProfileID == profileId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return profileReviewSections;
        }


        public async Task<IEnumerable<ProfileSubSection>> GetAllProfileSubSectionsAsync()
        {
            IEnumerable<ProfileSubSection> profileSubSections = null;
            try
            {
                profileSubSections = await repositoryManager.GetAllAsync<ProfileSubSection>();
            }
            catch (Exception)
            {
                throw;
            }
            return profileSubSections;
        }

        public async Task<IEnumerable<VerificationLink>> GetAllVerificationLinksAsync()
        {
            IEnumerable<VerificationLink> VerificationLinks = null;
            try
            {
                //VerificationLinks = await repositoryManager.GetAllAsync<VerificationLink>();
                VerificationLinks = await repositoryManager.GetAsync<VerificationLink>(v => v.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString());
            }
            catch (Exception)
            {
                throw;
            }
            return VerificationLinks;
        }



        public async Task<int> InactivateVerificationLink(int verificationLinkID)
        {
            try
            {
                var VerificationLinkRepo = uow.GetGenericRepository<VerificationLink>();
                //VerificationLink inactiveverificationlink = await VerificationLinkRepo.FindAsync(c => c.VerificationLinkID == verificationLinkID);
                var result = VerificationLinkRepo.Find(c => c.VerificationLinkID == verificationLinkID);
                result.StatusType = Entities.MasterData.Enums.StatusType.Inactive;
                VerificationLinkRepo.Update(result);
                VerificationLinkRepo.Save();
                return result.VerificationLinkID;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> InactivateNotesTemplte(int notesTemplateID)
        {
            try
            {
                var NotesTemplateRepo = uow.GetGenericRepository<NotesTemplate>();
                //VerificationLink inactiveverificationlink = await VerificationLinkRepo.FindAsync(c => c.VerificationLinkID == verificationLinkID);
                var result = await NotesTemplateRepo.FindAsync(c => c.NotesTemplateID == notesTemplateID);
                result.StatusType = Entities.MasterData.Enums.StatusType.Inactive;
                NotesTemplateRepo.Update(result);
                NotesTemplateRepo.Save();
                return result.NotesTemplateID;
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<IEnumerable<DecredentialingReason>> GetAllDecredentialingReasonsAsync()
        {
            IEnumerable<DecredentialingReason> DecredentialingReasons = null;
            try
            {
                DecredentialingReasons = await repositoryManager.GetAsync<DecredentialingReason>(r => r.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString());
            }
            catch (Exception)
            {
                throw;
            }
            return DecredentialingReasons;
        }

        public async Task<int> InactivateDecredentialingReason(int decredentialingReasonID)
        {
            try
            {
                var DecredentialingReasonRepo = uow.GetGenericRepository<DecredentialingReason>();
                var result = DecredentialingReasonRepo.Find(c => c.DecredentialingReasonId == decredentialingReasonID);
                result.StatusType = Entities.MasterData.Enums.StatusType.Inactive;
                DecredentialingReasonRepo.Update(result);
                DecredentialingReasonRepo.Save();
                return result.DecredentialingReasonId;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<NotesTemplate> GetNotesTemplateByCode(string Code)
        {
            var uo = uow.GetNotesTemplateRepository();
            return uo.GetNotesTemplateByCode(Code);

        }
        public List<NotesTemplate> GetAllNotesTemplates()
        {
            var uo = uow.GetNotesTemplateRepository();
            return uo.GetAllNotesTemplates();
        }

        public async Task<IEnumerable<Facility>> GetAllMasterFacilityInformationAsync()
        {
            IEnumerable<Facility> FacilityInformations = null;
            try
            {
                FacilityInformations = await repositoryManager.GetAsync<Facility>(f => f.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString());
                foreach (var info in FacilityInformations)
                {
                    if (info.Building == null)
                    {
                        info.Building = "";
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return FacilityInformations;
        }

        public async Task<IEnumerable<Profile>> GetAllMidLevelPractitionersAsync()
        {
            IEnumerable<Profile> MidLevelPractitioners = null;
            try
            {
                MidLevelPractitioners = await repositoryManager.GetAsync<Profile>(f => f.PersonalDetail.ProviderLevelID == 2);
            }
            catch (Exception)
            {
                throw;
            }
            return MidLevelPractitioners;
        }

        public async Task AddFacilityAsync(int organizationId, Facility facility)
        {
            try
            {
                var OrganizationRepository = uow.GetGenericRepository<Organization>();
                var organization = OrganizationRepository.Find(organizationId);
                if (organization.Facilities == null)
                {
                    organization.Facilities = new List<Facility>();
                }
                // setting the facility active    
                facility.Status = StatusType.Active.ToString();
                organization.Facilities.Add(facility);
                await OrganizationRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateFacilityAsync(int organizationId, Facility facility)
        {
            try
            {
                var OrganizationRepository = uow.GetGenericRepository<Organization>();
                var organization = await OrganizationRepository.FindAsync(organizationId);
                var dbFacility = organization.Facilities.FirstOrDefault(f => f.FacilityID == facility.FacilityID);
                if (dbFacility == null)
                    throw new Exception("Invalid Facility");

                facility.Status = StatusType.Active.ToString();
                dbFacility.Building = facility.Building;
                dbFacility.City = facility.City;
                dbFacility.Code = facility.Code;
                dbFacility.Country = facility.Country;
                dbFacility.CountryCodeFax = facility.CountryCodeFax;
                dbFacility.CountryCodeTelephone = facility.CountryCodeTelephone;
                dbFacility.County = facility.County;
                dbFacility.EmailAddress = facility.EmailAddress;
                dbFacility.Fax = facility.Fax;
                dbFacility.FaxNumber = facility.FaxNumber;
                dbFacility.FacilityDetail = facility.FacilityDetail;
                dbFacility.Name = facility.Name;
                dbFacility.FacilityName = facility.FacilityName;
                dbFacility.State = facility.State;
                dbFacility.Status = facility.Status;
                dbFacility.Street = facility.Street;
                dbFacility.Telephone = facility.Telephone;
                dbFacility.ZipCode = facility.ZipCode;
                OrganizationRepository.Update(organization);
                OrganizationRepository.Save();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
