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
using AutoMapper;
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
        private IGenericRepository<PracticeProvider> practiceProviderRepository;

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
            this.practiceProviderRepository = uow.GetGenericRepository<PracticeProvider>();
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

        //public async Task UpdatePracticeBusinessManagerAsync(int practiceLocationDetailId, int businessOfficeManagerId)
        //{
        //    try
        //    {
        //        PracticeLocationRepository.UpdatePracticeBusinessManager(practiceLocationDetailId, businessOfficeManagerId);
        //        await PracticeLocationRepository.SaveAsync();
        //    }
        //    catch (Exception)
        //    {
                
        //        throw;
        //    }
        //}
        //public async Task UpdatePracticeBillingContactAsync(int practiceLocationDetailId, int billingContactPersonId)
        //{
        //    try
        //    {
        //        PracticeLocationRepository.UpdatePracticeBillingContact(practiceLocationDetailId, billingContactPersonId);
        //        await PracticeLocationRepository.SaveAsync();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public async Task UpdateEmployeeForPaymentAndRemittanceAsync(int practiceLocationDetailId, Employee paymentEmployee)
        //{
        //    try
        //    {
        //        PracticeLocationRepository.UpdateEmployeeForPaymentAndRemittance(practiceLocationDetailId, paymentEmployee);
        //        await PracticeLocationRepository.SaveAsync();
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

        //public async Task UpdatePaymentAndRemittanceAsync(int practiceLocationDetailId, int practicePaymentAndRemittanceId)
        //{
        //    try
        //    {
        //        PracticeLocationRepository.UpdatePaymentAndRemittanceAsync(practiceLocationDetailId, practicePaymentAndRemittanceId);
        //        await PracticeLocationRepository.SaveAsync();
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}

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

        public async Task UpdatePrimaryCredentialingContactAsync(int practiceLocationDetailId, Employee primaryCredentialingContactPerson)
        {
            try
            {
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId, "PrimaryCredentialingContactPerson");
                practiceLocationDetail.PrimaryCredentialingContactPerson = Mapper.Map<Employee, Employee>(primaryCredentialingContactPerson, practiceLocationDetail.PrimaryCredentialingContactPerson);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Worker Compensation

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
        #endregion

        #region Payment And Remittance

        public async Task UpdatePaymentAndRemittanceAsync(int practiceLocationDetailId, Entities.MasterProfile.PracticeLocation.PracticePaymentAndRemittance practicePaymentAndRemittance)
        {
            try
            {
                if (practicePaymentAndRemittance.PracticePaymentAndRemittanceID == 0)
                    await AddPracticePaymentAndRemittanceAsync(practiceLocationDetailId, practicePaymentAndRemittance);
                else
                    await UpdateExistingPracticePaymentAndRemittanceAsync(practiceLocationDetailId, practicePaymentAndRemittance);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateExistingPracticePaymentAndRemittanceAsync(int practiceLocationDetailId, PracticePaymentAndRemittance practicePaymentAndRemittance)
        {
            try
            {
                // Getting the practice location details by ID
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId, "PaymentAndRemittance", "PaymentAndRemittance.PaymentAndRemittancePerson");

                practiceLocationDetail.PaymentAndRemittance.PaymentAndRemittancePerson = Mapper.Map<Employee, Employee>(practicePaymentAndRemittance.PaymentAndRemittancePerson, practiceLocationDetail.PaymentAndRemittance.PaymentAndRemittancePerson);

                practiceLocationDetail.PaymentAndRemittance = Mapper.Map<PracticePaymentAndRemittance, PracticePaymentAndRemittance>(practicePaymentAndRemittance, practiceLocationDetail.PaymentAndRemittance);

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task AddPracticePaymentAndRemittanceAsync(int practiceLocationDetailId, PracticePaymentAndRemittance practicePaymentAndRemittance)
        {
            // Getting the practice location details by ID
            PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Find(practiceLocationDetailId);

            if (practicePaymentAndRemittance.PaymentAndRemittancePerson.Departments == null)
                practicePaymentAndRemittance.PaymentAndRemittancePerson.Departments = new List<EmployeeDepartment>();

            // Adding the association employee-department class to employee
            practicePaymentAndRemittance.PaymentAndRemittancePerson.Departments.Add(new EmployeeDepartment()
            {
                DepartmentID = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.PAYMENT).FirstOrDefault().DepartmentID
            });

            if (practicePaymentAndRemittance.PaymentAndRemittancePerson.Designations == null)
                practicePaymentAndRemittance.PaymentAndRemittancePerson.Designations = new List<EmployeeDesignation>();

            // Adding the association employee-department class to employee
            practicePaymentAndRemittance.PaymentAndRemittancePerson.Designations.Add(new EmployeeDesignation() { DesignationID = EmployeeDesignationValue.PaymentAndRemittanceContactDesignationId });

            practiceLocationDetail.PaymentAndRemittance = practicePaymentAndRemittance;

            // Persisting practice location details
            await PracticeLocationRepository.SaveAsync();
        }

        #endregion

        #region Business Office Manager

        public async Task UpdatePracticeBusinessManagerAsync(int practiceLocationDetailId, Entities.MasterData.Account.Staff.Employee businessOfficeManager)
        {
            try
            {
                if (businessOfficeManager.EmployeeID == 0)
                    await AddPracticeBusinessManagerAsync(practiceLocationDetailId, businessOfficeManager);
                else
                    await UpdateExistingPracticeBusinessManagerAsync(practiceLocationDetailId, businessOfficeManager);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateExistingPracticeBusinessManagerAsync(int practiceLocationDetailId, Employee businessOfficeManager)
        {
            try
            {
                // Getting the practice location details by ID
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId, "BusinessOfficeManagerOrStaff");

                practiceLocationDetail.BusinessOfficeManagerOrStaff = Mapper.Map<Employee, Employee>(businessOfficeManager, practiceLocationDetail.BusinessOfficeManagerOrStaff);

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task AddPracticeBusinessManagerAsync(int practiceLocationDetailId, Employee businessOfficeManager)
        {
            // Getting the practice location details by ID
            PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Find(practiceLocationDetailId);

            if (businessOfficeManager.Departments == null)
                businessOfficeManager.Departments = new List<EmployeeDepartment>();

            // Adding the association employee-department class to employee
            businessOfficeManager.Departments.Add(new EmployeeDepartment()
            {
                DepartmentID = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.BUSINESS).FirstOrDefault().DepartmentID
            });

            if (businessOfficeManager.Designations == null)
                businessOfficeManager.Designations = new List<EmployeeDesignation>();

            // Adding the association employee-department class to employee
            businessOfficeManager.Designations.Add(new EmployeeDesignation() { DesignationID = EmployeeDesignationValue.OfficeManagerDesignationId });

            practiceLocationDetail.BusinessOfficeManagerOrStaff = businessOfficeManager;

            // Persisting practice location details
            await PracticeLocationRepository.SaveAsync();
        } 

        #endregion

        #region Billing Contact

        public async Task UpdatePracticeBillingContactAsync(int practiceLocationDetailId, Entities.MasterData.Account.Staff.Employee billingContactPerson)
        {

            try
            {
                if (billingContactPerson.EmployeeID == 0)
                    await AddPracticeBillingCongtactAsync(practiceLocationDetailId, billingContactPerson);
                else
                    await UpdateExistingPracticeBillingCongtactAsync(practiceLocationDetailId, billingContactPerson);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateExistingPracticeBillingCongtactAsync(int practiceLocationDetailId, Employee billingContactPerson)
        {
            try
            {
                // Getting the practice location details by ID
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId, "BillingContactPerson");

                practiceLocationDetail.BillingContactPerson = Mapper.Map<Employee, Employee>(billingContactPerson, practiceLocationDetail.BillingContactPerson);

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task AddPracticeBillingCongtactAsync(int practiceLocationDetailId, Employee billingContactPerson)
        {
            // Getting the practice location details by ID
            PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Find(practiceLocationDetailId);

            if (billingContactPerson.Departments == null)
                billingContactPerson.Departments = new List<EmployeeDepartment>();

            // Adding the association employee-department class to employee
            billingContactPerson.Departments.Add(new EmployeeDepartment()
            {
                DepartmentID = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.BILLING).FirstOrDefault().DepartmentID
            });

            if (billingContactPerson.Designations == null)
                billingContactPerson.Designations = new List<EmployeeDesignation>();

            // Adding the association employee-department class to employee
            billingContactPerson.Designations.Add(new EmployeeDesignation() { DesignationID = EmployeeDesignationValue.BillingContactDesignationId });

            practiceLocationDetail.BillingContactPerson = billingContactPerson;

            // Persisting practice location details
            await PracticeLocationRepository.SaveAsync();
        } 

        #endregion

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


        public void AddCredentialingContactExisting(PracticeLocationDetail practiceLocationDetail,int employeeId)
        {

            practiceLocationDetail.PrimaryCredentialingContactPersonId = employeeId;
            PracticeLocationRepository.SaveAsync();
        }

        public async Task AddCredentialingContactAsync(int practiceLocationDetailId, Employee credentialingContactPerson)
        {
            try
            {
                // Getting the practice location details by ID
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Find(practiceLocationDetailId);

                if (credentialingContactPerson.EmployeeID != 0)
                {

                    AddCredentialingContactExisting(practiceLocationDetail, credentialingContactPerson.EmployeeID);

                }

                else {

                    // Getting the business department 
                    var department = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.CredentialingContact).FirstOrDefault();

                    // Creating association class with in employee & department 
                    EmployeeDepartment employeeDepartment = new EmployeeDepartment();

                    // Assigning department to association class
                    employeeDepartment.DepartmentID = department.DepartmentID;


                    if (credentialingContactPerson.Departments == null)
                        credentialingContactPerson.Departments = new List<EmployeeDepartment>();

                    // Adding the association employee-department class to employee
                    credentialingContactPerson.Departments.Add(employeeDepartment);

                    EmployeeDesignation employeeDesignation = new EmployeeDesignation();
                    employeeDesignation.DesignationID = EmployeeDesignationValue.PrimaryCredentialingContactDesignationId;

                    if (credentialingContactPerson.Designations == null)
                        credentialingContactPerson.Designations = new List<EmployeeDesignation>();

                    // Adding the association employee-department class to employee
                    credentialingContactPerson.Designations.Add(employeeDesignation);

                    // Getting organization by ID
                    Organization organization = organizationRepository.Find(practiceLocationDetail.OrganizationId);

                    if (organization.Employees == null)
                        organization.Employees = new List<Employee>();

                    organization.Employees.Add(credentialingContactPerson);

                    // Persisting the organization with employees
                    await organizationRepository.SaveAsync();

                    // Assigning business manager to practice location 
                    practiceLocationDetail.PrimaryCredentialingContactPersonId = credentialingContactPerson.EmployeeID;

                    // Persisting practice location details
                    await PracticeLocationRepository.SaveAsync();

                    // Getting facility by ID
                    Facility facility = await facilityRepository.FindAsync(f => f.FacilityID == practiceLocationDetail.FacilityId, "FacilityDetail,FacilityDetail.Employees");
                    if (facility.FacilityDetail.Employees == null)
                        facility.FacilityDetail.Employees = new List<FacilityEmployee>();

                    FacilityEmployee facilityEmployee = new FacilityEmployee();
                    facilityEmployee.EmployeeID = credentialingContactPerson.EmployeeID;

                    facility.FacilityDetail.Employees.Add(facilityEmployee);

                    // Saving facility
                    await facilityRepository.SaveAsync();
                
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task AddPracticeProviderAsync(int practiceLocationDetailID, PracticeProvider practiceProvider)
        {
            try
            {

                // Getting the practice location details by ID
                PracticeLocationDetail practiceLocationDetail = PracticeLocationRepository.Get(pl => pl.PracticeLocationDetailID == practiceLocationDetailID, "PracticeProviders").FirstOrDefault();

                if (practiceLocationDetail.PracticeProviders.Any(mp => mp.NPINumber == practiceProvider.NPINumber && mp.Status == AHC.CD.Entities.MasterData.Enums.StatusType.Active.ToString() && mp.PracticeType == practiceProvider.PracticeType))
                {
                    throw new PracticeLocationDuplicateMidLevel(ExceptionMessage.PRACTICE_LOCATION_DUPLICATE_PracticeProvider);
                }

                // Adding covering colleague to practice location 
                if (practiceLocationDetail.PracticeProviders == null)
                    practiceLocationDetail.PracticeProviders = new List<PracticeProvider>();

                practiceProvider.ActivationDate = DateTime.Now;
                practiceProvider.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;                
                practiceLocationDetail.PracticeProviders.Add(practiceProvider);

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();


            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task UpdatePracticeProviderAsync(PracticeProvider practiceProvider)
        {
            try
            {
                var practice = practiceProviderRepository.Find(mp => mp.PracticeProviderID == practiceProvider.PracticeProviderID);
                practiceProvider.StatusType = practice.StatusType;
                practiceProvider.ActivationDate = practice.ActivationDate;
                practice = AutoMapper.Mapper.Map<PracticeProvider, PracticeProvider>(practiceProvider, practice);
                
                //practiceProviderRepository.Update(practice);

                // Persisting practice location details
                await practiceProviderRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task RemovePracticeProviderAsync(PracticeProvider practiceProvider)
        {
            try
            {

                var practice = practiceProviderRepository.Find(mp => mp.PracticeProviderID == practiceProvider.PracticeProviderID);

                practice.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                practice.DeactivationDate = DateTime.Now;
                //practice = AutoMapper.Mapper.Map<PracticeProvider, PracticeProvider>(practiceProvider, practice);
                
                //practiceProviderRepository.Update(practice);

                // Persisting practice location details
                await practiceProviderRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
