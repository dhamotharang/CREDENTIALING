using AHC.CD.Data.Repository;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Tables;

namespace AHC.CD.Business.Account
{
    class OrganizationManager : IOrganizationManager
    {
        private IGenericRepository<Organization> OrganizationRepository { get; set; }

        public OrganizationManager(IUnitOfWork uow)
        {
            this.OrganizationRepository = uow.GetGenericRepository<Organization>();
        }

        #region Checked

        public async Task AddFacilityAsync(int organizationId, Facility facility)
        {
            try
            {
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
                //var organization = OrganizationRepository.Find(organizationId);
                //// setting the facility active
                //facility.Status = StatusType.Active.ToString();
                ////organization.Facilities.Add(facility);
                //foreach (Facility updateFacility in organization.Facilities)
                //{
                //    if (updateFacility.FacilityID == facility.FacilityID)
                //    {
                //        updateFacility.Building = facility.Building;
                //        updateFacility.City = facility.City;
                //        updateFacility.Code = facility.Code;
                //        updateFacility.Country = facility.Country;
                //        updateFacility.CountryCodeFax = facility.CountryCodeFax;
                //        updateFacility.CountryCodeTelephone = facility.CountryCodeTelephone;
                //        updateFacility.County = facility.County;
                //        updateFacility.EmailAddress = facility.EmailAddress;
                //        updateFacility.Fax = facility.Fax;
                //        updateFacility.FaxNumber = facility.FaxNumber;
                //        updateFacility.FacilityDetail = facility.FacilityDetail;
                //        updateFacility.Name = facility.Name;
                //        updateFacility.FacilityName = facility.FacilityName;
                //        updateFacility.State = facility.State;
                //        updateFacility.Status = facility.Status;
                //        updateFacility.Street = facility.Street;
                //        updateFacility.Telephone = facility.Telephone;
                //        updateFacility.ZipCode = facility.ZipCode;
                //        break;
                //    }
                //}
                ////Facility updatedFacility = null;
                ////updatedFacility = organization.Facilities.FirstOrDefault(f => f.FacilityID == facility.FacilityID);
                //OrganizationRepository.Update(organization);
                //await OrganizationRepository.SaveAsync();


                var organization = await OrganizationRepository.FindAsync(organizationId);
                var dbFacility = organization.Facilities.FirstOrDefault(f => f.FacilityID == facility.FacilityID);
                if (dbFacility == null)
                    throw new Exception("Invalid Facility");

                facility.Status = StatusType.Active.ToString();
                //dbFacility = AutoMapper.Mapper.Map<Facility, Facility>(facility, dbFacility);
                //dbFacility.FacilityDetail = AutoMapper.Mapper.Map<FacilityDetail, FacilityDetail>(facility.FacilityDetail, dbFacility.FacilityDetail);
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

        #endregion


        public async Task AddOrganizationAsync(Entities.MasterData.Account.Organization organization)
        {
            try
            {
                OrganizationRepository.Create(organization);
                await OrganizationRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Entities.MasterData.Account.Organization>> GetAllOrganizationAsync(bool onlyActiveRecords = true)
        {
            IEnumerable<Organization> organizations = null;

            switch (onlyActiveRecords)
            {
                case true:
                    organizations = await OrganizationRepository.GetAsync(s => s.Status.Equals(StatusType.Active.ToString()));
                    break;
                case false:
                    organizations = await OrganizationRepository.GetAllAsync();
                    break;
            }

            return organizations;
        }

        public async Task<IEnumerable<Entities.MasterData.Account.Organization>> GetAllOrganizationWithLocationAsync(bool onlyActiveRecords = true)
        {
            IEnumerable<Organization> organizations = null;

            switch (onlyActiveRecords)
            {
                case true:



                    organizations = await OrganizationRepository.GetAsync(s => s.Status.Equals(StatusType.Active.ToString()), "Facilities");
                    foreach (var item in organizations)
                    {
                        item.Facilities = item.Facilities.Where(s => s.Status.Equals(StatusType.Active.ToString())).ToList();
                    }

                    break;
                case false:
                    organizations = await OrganizationRepository.GetAllAsync();
                    break;
            }

            return organizations;
        }

        public async Task<IEnumerable<Organization>> GetAllOrganizationWithLocationDetailAsync(bool onlyActiveRecords = true)
        {
            IEnumerable<Organization> organizations = null;

            switch (onlyActiveRecords)
            {
                case true:
                    organizations = await OrganizationRepository.GetAsync(s => s.Status.Equals(StatusType.Active.ToString()), "Facilities,Facilities.FacilityDetail,Facilities.FacilityDetail.Language,Facilities.FacilityDetail.Language.NonEnglishLanguages,Facilities.FacilityDetail.Accessibility,Facilities.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers,Facilities.FacilityDetail.Service,Facilities.FacilityDetail.Service.FacilityServiceQuestionAnswers,Facilities.FacilityDetail.Service.PracticeType,Facilities.FacilityDetail.PracticeOfficeHour.PracticeDays.DailyHours");
                    foreach (var item in organizations)
                    {
                        item.Facilities = item.Facilities.Where(s => s.Status.Equals(StatusType.Active.ToString())).ToList();
                    }

                    break;
                case false:
                    organizations = await OrganizationRepository.GetAllAsync();
                    break;
            }

            return organizations;
        }

        public async Task<IEnumerable<PracticingGroup>> GetAllPracticingGroupsAsync(int organizationId, bool onlyActiveRecords = true)
        {

            IEnumerable<PracticingGroup> PracticingGroups = null;
            Organization organization = null;


            switch (onlyActiveRecords)
            {
                case true:
                    organization = await OrganizationRepository.FindAsync(organizationId, "PracticingGroups");

                    break;
                case false:
                    organization = await OrganizationRepository.FindAsync(organizationId);
                    break;
            }

            return PracticingGroups;

        }

        public async Task<IEnumerable<PracticingGroup>> GetGroupsAsync(int organizationId)
        {
            try
            {
                var organization = await OrganizationRepository.GetAsync(s => s.OrganizationID == organizationId, "PracticingGroups.Group");
                return organization.SelectMany(s => s.PracticingGroups.Where(t => t.Status.Equals(StatusType.Active.ToString())));
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<PracticingGroup>> GetAllMidLevelsByOrgId(int organizationId, bool onlyActiveRecords = true)
        {
            throw new NotImplementedException();
        }

        public async Task<Organization> GetOrganizationByIDWithLocationDetailAsync(int organizationID)
        {

            var organization = await OrganizationRepository.FindAsync(s => s.OrganizationID == organizationID && s.Facilities.Any(t => t.Status.Equals(StatusType.Active.ToString())) && s.Status.Equals(StatusType.Active.ToString()), "Facilities,Facilities.FacilityDetail,Facilities.FacilityDetail.Language,Facilities.FacilityDetail.Language.NonEnglishLanguages,Facilities.FacilityDetail.Accessibility,Facilities.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers,Facilities.FacilityDetail.Service,Facilities.FacilityDetail.Service.FacilityServiceQuestionAnswers,Facilities.FacilityDetail.Service.PracticeType,Facilities.FacilityDetail.PracticeOfficeHour.PracticeDays.DailyHours");
            return organization;
        }
    }
}
