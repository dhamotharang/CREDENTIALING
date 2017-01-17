using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AHC.CD.Exceptions.Profiles;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    internal class PracticeLocationManager : IPracticeLocationManager
    {
        private IProfileRepository ProfileRepository { get; set; }
        private IPracticeLocationRepository PracticeLocationRepository { get; set; }
        private IGenericRepository<Department> departmentRepository;
        private IGenericRepository<Organization> organizationRepository;
        private IGenericRepository<Facility> facilityRepository;
        private IGenericRepository<MidLevelPractitioner> midLevelRepository;
        private IGenericRepository<SupervisingProvider> supervisingProviderRepository;
        private IGenericRepository<Employee> employeeRepository;
        private IGenericRepository<PracticeColleague> partnerRepository;

        public PracticeLocationManager(IUnitOfWork uow, IRepositoryManager repositoryManager)
        {
            this.ProfileRepository = uow.GetProfileRepository();
            this.PracticeLocationRepository = uow.GetPracticeLocationRepository();
            this.departmentRepository = uow.GetGenericRepository<Department>();
            this.organizationRepository = uow.GetGenericRepository<Organization>();
            this.facilityRepository = uow.GetGenericRepository<Facility>();
            this.midLevelRepository = uow.GetGenericRepository<MidLevelPractitioner>();
            this.supervisingProviderRepository = uow.GetGenericRepository<SupervisingProvider>();
            this.employeeRepository = uow.GetGenericRepository<Employee>();
            this.partnerRepository = uow.GetGenericRepository<PracticeColleague>();
        }

        public async Task AddPracticeLocationAsync(int profileId, Entities.MasterProfile.PracticeLocation.PracticeLocationDetail practiceLocationDetail)
        {
            try
            {
                
                ProfileRepository.AddPracticeLocation(profileId, practiceLocationDetail);
                await ProfileRepository.SaveAsync();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdatePracticeLocationAsync(Entities.MasterProfile.PracticeLocation.PracticeLocationDetail practiceLocationDetail)
        {
            try
            {
                PracticeLocationRepository.UpdatePracticeLocation(practiceLocationDetail);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdatePracticeBusinessManagerAsync(int practiceLocationDetailId, int businessOfficeManagerId)
        {
            try
            {
                PracticeLocationRepository.UpdatePracticeBusinessManager(practiceLocationDetailId, businessOfficeManagerId);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public Task UpdatePracticeOfficeHourAsync(int practiceLocationDetailId, Entities.MasterProfile.PracticeLocation.PracticeOfficeHour practiceOfficeHour)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateOpenPracticeStatusAsync(int practiceLocationDetailId, Entities.MasterProfile.PracticeLocation.OpenPracticeStatus openPracticeStatus)
        {
            try
            {
                PracticeLocationRepository.UpdateOpenPracticeStatus(practiceLocationDetailId, openPracticeStatus);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdatePracticeBillingContactAsync(int practiceLocationDetailId, int billingContactPersonId)
        {
            try
            {
                PracticeLocationRepository.UpdatePracticeBillingContact(practiceLocationDetailId, billingContactPersonId);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdatePaymentAndRemittanceAsync(int practiceLocationDetailId, Entities.MasterProfile.PracticeLocation.PracticePaymentAndRemittance practicePaymentAndRemittance)
        {
            try
            {
                // Getting the practice location details by ID
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Find(practiceLocationDetailId);

                // Getting the business department 
                var department = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.PAYMENT).FirstOrDefault();

                // Creating association class with in employee & department 
                EmployeeDepartment employeeDepartment = new EmployeeDepartment();

                // Assigning department to association class
                employeeDepartment.DepartmentID = department.DepartmentID;


                if (practicePaymentAndRemittance.PaymentAndRemittancePerson.Departments == null)
                    practicePaymentAndRemittance.PaymentAndRemittancePerson.Departments = new List<EmployeeDepartment>();

                // Adding the association employee-department class to employee
                practicePaymentAndRemittance.PaymentAndRemittancePerson.Departments.Add(employeeDepartment);

                EmployeeDesignation employeeDesignation = new EmployeeDesignation();
                employeeDesignation.DesignationID = EmployeeDesignationValue.OfficeManagerDesignationId;

                if (practicePaymentAndRemittance.PaymentAndRemittancePerson.Designations == null)
                    practicePaymentAndRemittance.PaymentAndRemittancePerson.Designations = new List<EmployeeDesignation>();

                // Adding the association employee-department class to employee
                practicePaymentAndRemittance.PaymentAndRemittancePerson.Designations.Add(employeeDesignation);


                // Saving the business manager as a master
                //employeeRepository.Create(businessOfficeManager);

                //await employeeRepository.SaveAsync();

                // Getting organization by ID
                Organization organization = organizationRepository.Find(practiceLocationDetail.OrganizationId);

                //Employee organizationEmployee = new Employee();
                //organizationEmployee.EmployeeID = businessOfficeManager.EmployeeID;

                if (organization.Employees == null)
                    organization.Employees = new List<Employee>();

                organization.Employees.Add(practicePaymentAndRemittance.PaymentAndRemittancePerson);

                // Persisting the organization with employees
                await organizationRepository.SaveAsync();



                // Assigning business manager to practice location 
                practiceLocationDetail.BusinessOfficeManagerOrStaffId = practicePaymentAndRemittance.PaymentAndRemittancePerson.EmployeeID;

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();

                // Getting facility by ID
                Facility facility = await facilityRepository.FindAsync(f => f.FacilityID == practiceLocationDetail.FacilityId, "FacilityDetail,FacilityDetail.Employees");
                if (facility.FacilityDetail.Employees == null)
                    facility.FacilityDetail.Employees = new List<FacilityEmployee>();

                FacilityEmployee facilityEmployee = new FacilityEmployee();
                facilityEmployee.EmployeeID = practicePaymentAndRemittance.PaymentAndRemittancePerson.EmployeeID;

                facility.FacilityDetail.Employees.Add(facilityEmployee);

                // Saving facility
                await facilityRepository.SaveAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdatePracticeColleagueAsync(int practiceLocationDetailId, Entities.MasterProfile.PracticeLocation.PracticeColleague practiceColleague)
        {
            try
            {
                //PracticeLocationRepository.UpdatePracticeColleague(practiceLocationDetailId, practiceColleague);
                //await PracticeLocationRepository.SaveAsync();

                var partner = partnerRepository.Find(mp => mp.PracticeColleagueID == practiceColleague.PracticeColleagueID);
                partner.StatusType = partner.StatusType;                
                partner.DeactivationDate = DateTime.Now;               
                partnerRepository.Update(partner);


                // Persisting practice location details
                await partnerRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdatePrimaryCredentialingContactAsync(int practiceLocationDetailId, int primaryCredentialingContactPersonId)
        {
            try
            {
                PracticeLocationRepository.UpdatePrimaryCredentialingContact(practiceLocationDetailId, primaryCredentialingContactPersonId);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateWorkersCompensationInformationAsync(int practiceLocationDetailId, Entities.MasterProfile.PracticeLocation.WorkersCompensationInformation workersCompensationInformation)
        {
            try
            {
                PracticeLocationRepository.UpdateWorkersCompensationInformation(practiceLocationDetailId, workersCompensationInformation);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task RenewWorkersCompensationInformationAsync(int practiceLocationDetailID, WorkersCompensationInformation workersCompensationInfo)
        {
            try
            {
                //Add Workers compensation history information
                PracticeLocationRepository.AddWorkersCompensationHistory(practiceLocationDetailID);

                //Update the state license information
                PracticeLocationRepository.UpdateWorkersCompensationInformation(practiceLocationDetailID, workersCompensationInfo);

                //save the information in the repository
                await PracticeLocationRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.STATE_LICENSE_RENEW_EXCEPTION, ex);
            }
        }

        public async Task UpdatePaymentAndRemittanceAsync(int practiceLocationDetailId, int practicePaymentAndRemittanceId)
        {
            try
            {
                PracticeLocationRepository.UpdatePaymentAndRemittanceAsync(practiceLocationDetailId, practicePaymentAndRemittanceId);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdatePracticeBusinessManagerAsync(int practiceLocationDetailId, Entities.MasterData.Account.Staff.Employee businessOfficeManager)
        {
            try
            {
                // Getting the practice location details by ID
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Find(practiceLocationDetailId);

                // Getting the business department 
                var department = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.BUSINESS).FirstOrDefault();

                // Creating association class with in employee & department 
                EmployeeDepartment employeeDepartment = new EmployeeDepartment();

                // Assigning department to association class
                employeeDepartment.DepartmentID = department.DepartmentID;


                if (businessOfficeManager.Departments == null)
                    businessOfficeManager.Departments = new List<EmployeeDepartment>();

                // Adding the association employee-department class to employee
                businessOfficeManager.Departments.Add(employeeDepartment);

                EmployeeDesignation employeeDesignation = new EmployeeDesignation();
                employeeDesignation.DesignationID = EmployeeDesignationValue.OfficeManagerDesignationId;

                if (businessOfficeManager.Designations == null)
                    businessOfficeManager.Designations = new List<EmployeeDesignation>();

                // Adding the association employee-department class to employee
                businessOfficeManager.Designations.Add(employeeDesignation);


                // Saving the business manager as a master
                //employeeRepository.Create(businessOfficeManager);

                //await employeeRepository.SaveAsync();

                // Getting organization by ID
                Organization organization = organizationRepository.Find(practiceLocationDetail.OrganizationId);

                //Employee organizationEmployee = new Employee();
                //organizationEmployee.EmployeeID = businessOfficeManager.EmployeeID;

                if (organization.Employees == null)
                    organization.Employees = new List<Employee>();

                organization.Employees.Add(businessOfficeManager);

                // Persisting the organization with employees
                await organizationRepository.SaveAsync();

               

                // Assigning business manager to practice location 
                practiceLocationDetail.BusinessOfficeManagerOrStaffId = businessOfficeManager.EmployeeID;

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();

                // Getting facility by ID
                Facility facility = await facilityRepository.FindAsync(f => f.FacilityID == practiceLocationDetail.FacilityId, "FacilityDetail,FacilityDetail.Employees");
                if (facility.FacilityDetail.Employees == null)
                    facility.FacilityDetail.Employees = new List<FacilityEmployee>();

                FacilityEmployee facilityEmployee = new FacilityEmployee();
                facilityEmployee.EmployeeID = businessOfficeManager.EmployeeID;

                facility.FacilityDetail.Employees.Add(facilityEmployee);

                // Saving facility
                await facilityRepository.SaveAsync();

            }
            catch (Exception)
            {

                throw;
            }

            //try
            //{
            //    PracticeLocationRepository.UpdatePracticeBusinessManager(practiceLocationDetailId, businessOfficeManager);
            //    await PracticeLocationRepository.SaveAsync();
            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        public async Task UpdatePracticeBillingContactAsync(int practiceLocationDetailId, Entities.MasterData.Account.Staff.Employee billingContactPerson)
        {
            try
            {
                // Getting the practice location details by ID
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Find(practiceLocationDetailId);

                // Getting the business department 
                var department = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.BILLING).FirstOrDefault();

                // Creating association class with in employee & department 
                EmployeeDepartment employeeDepartment = new EmployeeDepartment();

                // Assigning department to association class
                employeeDepartment.DepartmentID = department.DepartmentID;

                 
                if (billingContactPerson.Departments == null)
                    billingContactPerson.Departments = new List<EmployeeDepartment>();

                // Adding the association employee-department class to employee
                billingContactPerson.Departments.Add(employeeDepartment);

                EmployeeDesignation employeeDesignation = new EmployeeDesignation();
                employeeDesignation.DesignationID = EmployeeDesignationValue.BillingContactDesignationId;

                if (billingContactPerson.Designations == null)
                    billingContactPerson.Designations = new List<EmployeeDesignation>();

                // Adding the association employee-department class to employee
                billingContactPerson.Designations.Add(employeeDesignation);
                

                // Saving the business manager as a master
                //employeeRepository.Create(businessOfficeManager);

                //await employeeRepository.SaveAsync();

                // Getting organization by ID
                Organization organization = organizationRepository.Find(practiceLocationDetail.OrganizationId);

                //Employee organizationEmployee = new Employee();
                //organizationEmployee.EmployeeID = businessOfficeManager.EmployeeID;

                if (organization.Employees == null)
                    organization.Employees = new List<Employee>();

                organization.Employees.Add(billingContactPerson);

                // Persisting the organization with employees
                await organizationRepository.SaveAsync();



                // Assigning business manager to practice location 
                practiceLocationDetail.BillingContactPersonId = billingContactPerson.EmployeeID;

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();

                // Getting facility by ID
                Facility facility = await facilityRepository.FindAsync(f => f.FacilityID == practiceLocationDetail.FacilityId, "FacilityDetail,FacilityDetail.Employees");
                if (facility.FacilityDetail.Employees == null)
                    facility.FacilityDetail.Employees = new List<FacilityEmployee>();
                
                FacilityEmployee facilityEmployee = new FacilityEmployee();
                facilityEmployee.EmployeeID = billingContactPerson.EmployeeID;

                facility.FacilityDetail.Employees.Add(facilityEmployee);

                // Saving facility
                await facilityRepository.SaveAsync();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateProviderOfficeHourAsync(int practiceLocationDetailId, Entities.MasterProfile.PracticeLocation.ProviderPracticeOfficeHour providerPracticeOfficeHour)
        {
            try
            {
                PracticeLocationRepository.UpdateProviderOfficeHourAsync(practiceLocationDetailId, providerPracticeOfficeHour);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task addMidLevelAsync(int practiceLocationDetailID, MidLevelPractitioner midLevel)
        {
            try
            {

                // Getting the practice location details by ID
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Get(pl => pl.PracticeLocationDetailID == practiceLocationDetailID, "MidlevelPractioners").FirstOrDefault();

                if(practiceLocationDetail.MidlevelPractioners.Any(mp => mp.ProfileID == midLevel.ProfileID && mp.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()))
                {
                    throw new PracticeLocationDuplicateMidLevel(ExceptionMessage.PRACTICE_LOCATION_DUPLICATE_MIDLEVEL);
                }

                // Assigning billing contact to practice location 
                if (practiceLocationDetail.MidlevelPractioners == null)
                    practiceLocationDetail.MidlevelPractioners = new List<MidLevelPractitioner>();

                practiceLocationDetail.MidlevelPractioners.Add(midLevel);

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();

               
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateMidLevelAsync(int practiceLocationDetailID, MidLevelPractitioner midLevel)
        {
            try
            {
               var ml = midLevelRepository.Find(mp => mp.MidLevelPractitionerID == midLevel.MidLevelPractitionerID);
               ml.StatusType = midLevel.StatusType;
                //midLevel.ActivationDate = ml.ActivationDate;
               ml.DeactivationDate = DateTime.Now;
                //midLevel.mo
                midLevelRepository.Update(ml);


                // Persisting practice location details
                await midLevelRepository.SaveAsync();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateEmployeeForPaymentAndRemittanceAsync(int practiceLocationDetailId, Employee paymentEmployee)
        {
            try
            {
                PracticeLocationRepository.UpdateEmployeeForPaymentAndRemittance(practiceLocationDetailId, paymentEmployee);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        //AddSupervisingProviderAsync

        public async Task AddSupervisingProviderAsync(int practiceLocationDetailID, SupervisingProvider supervisingProvider)
        {
            try
            {

                // Getting the practice location details by ID
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Get(pl => pl.PracticeLocationDetailID == practiceLocationDetailID, "SupervisingProviders").FirstOrDefault();

                if (practiceLocationDetail.SupervisingProviders.Any(mp => mp.ProfileID == supervisingProvider.ProfileID && mp.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()))
                {
                    throw new PracticeLocationDuplicateMidLevel(ExceptionMessage.PRACTICE_LOCATION_DUPLICATE_SUPERVISING);
                }

                // Assigning billing contact to practice location 
                if (practiceLocationDetail.SupervisingProviders == null)
                    practiceLocationDetail.SupervisingProviders = new List<SupervisingProvider>();

                practiceLocationDetail.SupervisingProviders.Add(supervisingProvider);

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdateSupervisingProviderAsync(int practiceLocationDetailID, SupervisingProvider supervisingProvider)
        {
            try
            {
                var supervising = supervisingProviderRepository.Find(sp => sp.SupervisingProviderID == supervisingProvider.SupervisingProviderID);
                supervising.StatusType = supervisingProvider.StatusType;
                //midLevel.ActivationDate = ml.ActivationDate;
                supervising.DeactivationDate = DateTime.Now;

                supervisingProviderRepository.Update(supervising);

                // Persisting practice location details
                await supervisingProviderRepository.SaveAsync();


            }
            catch (Exception)
            {

                throw;
            }
        }



        public async Task AddPracticeColleagueAsync(int practiceLocationDetailID, PracticeColleague practiceColleague)
        {
            try
            {

                // Getting the practice location details by ID
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Get(pl => pl.PracticeLocationDetailID == practiceLocationDetailID, "PracticeColleagues").FirstOrDefault();

                if (practiceLocationDetail.PracticeColleagues.Any(mp => mp.ProfileID == practiceColleague.ProfileID && mp.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString()))
                {
                    throw new PracticeLocationDuplicateMidLevel(ExceptionMessage.PRACTICE_LOCATION_DUPLICATE_Partner);
                }

                // Adding covering colleague to practice location 
                if (practiceLocationDetail.PracticeColleagues == null)
                    practiceLocationDetail.PracticeColleagues = new List<PracticeColleague>();

                practiceColleague.ActivationDate = DateTime.Now;
                practiceLocationDetail.PracticeColleagues.Add(practiceColleague);

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();


        }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
