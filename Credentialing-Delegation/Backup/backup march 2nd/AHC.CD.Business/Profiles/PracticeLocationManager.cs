using AHC.CD.Data.Repository;
using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities;
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
        private IGenericRepository<Employee> employeeRepository;
        private IGenericRepository<PracticeProvider> practiceProviderRepository;
        private IGenericRepository<PracticeOpenStatusQuestionAnswer> PracticeOpenStatusQuestionAnswerRepository;
        private IGenericRepository<CDUser> CDUserRepo;
        //private IUnitOfWork uow;

        public PracticeLocationManager(IUnitOfWork uow, IRepositoryManager repositoryManager)
        {
            this.ProfileRepository = uow.GetProfileRepository();
            this.PracticeLocationRepository = uow.GetPracticeLocationRepository();
            this.departmentRepository = uow.GetGenericRepository<Department>();
            this.organizationRepository = uow.GetGenericRepository<Organization>();
            this.facilityRepository = uow.GetGenericRepository<Facility>();
            this.employeeRepository = uow.GetGenericRepository<Employee>();
            this.practiceProviderRepository = uow.GetGenericRepository<PracticeProvider>();
            this.CDUserRepo = uow.GetGenericRepository<CDUser>();
            this.PracticeOpenStatusQuestionAnswerRepository = uow.GetGenericRepository<PracticeOpenStatusQuestionAnswer>();

        }

        #region Practice Location General Information

        public async Task AddPracticeLocationAsync(int profileId, Entities.MasterProfile.PracticeLocation.PracticeLocationDetail practiceLocationDetail)
        {
            try
            {
              //  var profile=ProfileRepository.FindAsync(x=>x.ProfileID==profileId && x.PracticeLocationDetails)
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

        public async Task RemovePracticeLocationAsync(int profileId, PracticeLocationDetail practiceLocationDetail,string UserAuthID)
        {
            try
            {
                //Save record into History
               
                PracticeLocationRepository.AddPracticeLocationHistory(profileId, practiceLocationDetail.PracticeLocationDetailID,GetUserId(UserAuthID));

                //Remove practice location detail
                PracticeLocationRepository.RemovePracticeLocation(profileId, practiceLocationDetail);

                //save the information in the repository
                await PracticeLocationRepository.SaveAsync();
            }
            catch (ApplicationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new ProfileManagerException(ExceptionMessage.PRACTICE_LOCATION_REMOVE_EXCEPTION, ex);
            }
        }

        #endregion

        #region Open Practice Status

        public async Task UpdateOpenPracticeStatusAsync(int practiceLocationDetailId, Entities.MasterProfile.PracticeLocation.OpenPracticeStatus openPracticeStatus)
        {
            try
            {
                PracticeLocationDetail PracticeLocationDetail = PracticeLocationRepository.Find(x => x.PracticeLocationDetailID == practiceLocationDetailId, "OpenPracticeStatus,OpenPracticeStatus.PracticeQuestionAnswers");
                if (PracticeLocationDetail!=null)
                {
                    if (PracticeLocationDetail.OpenPracticeStatus == null)
                        PracticeLocationDetail.OpenPracticeStatus = openPracticeStatus;
                    else
                    {
                        PracticeLocationDetail.OpenPracticeStatus = AutoMapper.Mapper.Map<OpenPracticeStatus, OpenPracticeStatus>(openPracticeStatus);
                        
                        foreach (var data in openPracticeStatus.PracticeQuestionAnswers)
                        {
                            if (data.PracticeOpenStatusQuestionAnswerID == 0)
                            {
                                PracticeOpenStatusQuestionAnswerRepository.Create(data);
                            }
                        }
                        PracticeOpenStatusQuestionAnswerRepository.Save();
                    }
                }
                
                PracticeLocationRepository.UpdateOpenPracticeStatus(practiceLocationDetailId, openPracticeStatus);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        #region Practice Provider

        public async Task AddPracticeProviderAsync(int practiceLocationDetailID, PracticeProvider practiceProvider)
        {
            try
            {
                //if (await PracticeLocationRepository.AnyAsync(p => p.PracticeProviders.Any(pr => pr.NPINumber == practiceProvider.NPINumber && pr.Status.Equals(StatusType.Active.ToString()) && pr.Practice.Equals(practiceProvider.Practice)) && p.PracticeLocationDetailID == practiceLocationDetailID))
                //    throw new PracticeLocationDuplicateMidLevel(ExceptionMessage.PRACTICE_LOCATION_DUPLICATE_PracticeProvider);

                practiceProvider.ActivationDate = DateTime.Now;
                practiceProvider.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                PracticeLocationRepository.AddPracticeProvider(practiceLocationDetailID, practiceProvider);
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

                if (practiceProvider.PracticeType == AHC.CD.Entities.MasterData.Enums.PracticeType.CoveringColleague)
                {
                    foreach (var providerType in practiceProvider.PracticeProviderTypes)
                    {
                        if (providerType.PracticeProviderTypeId != 0)
                        {
                            var type = practice.PracticeProviderTypes.FirstOrDefault(t => t.PracticeProviderTypeId == providerType.PracticeProviderTypeId);
                            if (type != null)
                            {
                                type = AutoMapper.Mapper.Map<PracticeProviderType, PracticeProviderType>(providerType, type);
                            }
                            else
                                practice.PracticeProviderTypes.Add(providerType);
                        }
                        else
                        {
                            practice.PracticeProviderTypes.Add(providerType);
                        }
                    }

                    foreach (var providerSpecialty in practiceProvider.PracticeProviderSpecialties)
                    {
                        if (providerSpecialty.PracticeProviderSpecialtyId != 0)
                        {
                            var specialty = practice.PracticeProviderSpecialties.FirstOrDefault(t => t.PracticeProviderSpecialtyId == providerSpecialty.PracticeProviderSpecialtyId);
                            if (specialty != null)
                            {
                                specialty = AutoMapper.Mapper.Map<PracticeProviderSpecialty, PracticeProviderSpecialty>(providerSpecialty, specialty);
                            }
                            else
                                practice.PracticeProviderSpecialties.Add(providerSpecialty);
                        }
                        else
                        {
                            practice.PracticeProviderSpecialties.Add(providerSpecialty);
                        }
                    }

                }

                //practiceProviderRepository.Update(practice);

                // Persisting practice location details
                await practiceProviderRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task RemovePracticeProviderAsync(int practiceLocationDetailID, PracticeProvider practiceProvider)
        {
            try
            {
                //Save record into History
                var practice = PracticeLocationRepository.AddPracticeProviderHistory(practiceLocationDetailID, practiceProvider.PracticeProviderID);

                //var practice = practiceProviderRepository.Find(mp => mp.PracticeProviderID == practiceProvider.PracticeProviderID);

                practice.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Inactive;
                practice.DeactivationDate = DateTime.Now;
                //practice = AutoMapper.Mapper.Map<PracticeProvider, PracticeProvider>(practiceProvider, practice);

                //practiceProviderRepository.Update(practice);

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
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
                PracticeLocationRepository.UpdateExistingPracticeBusinessManager(practiceLocationDetailId, businessOfficeManager);
                
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
            try
            {
                if (businessOfficeManager.Departments == null)
                    businessOfficeManager.Departments = new List<EmployeeDepartment>();

                // Adding the association employee-department class to employee
                EmployeeDepartment ed = new EmployeeDepartment();
                var dr = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.BUSINESS).FirstOrDefault();
                ed.DepartmentID = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.BUSINESS).FirstOrDefault().DepartmentID;
                businessOfficeManager.Departments.Add(ed);

                if (businessOfficeManager.Designations == null)
                    businessOfficeManager.Designations = new List<EmployeeDesignation>();

                // Adding the association employee-department class to employee
                businessOfficeManager.Designations.Add(new EmployeeDesignation() { DesignationID = EmployeeDesignationValue.OfficeManagerDesignationId });

                PracticeLocationRepository.AddPracticeBusinessManager(practiceLocationDetailId, businessOfficeManager);

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                PracticeLocationRepository.UpdateExistingPracticeBillingCongtact(practiceLocationDetailId, billingContactPerson);

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

            PracticeLocationRepository.AddPracticeBillingCongtact(practiceLocationDetailId, billingContactPerson);

            // Persisting practice location details
            await PracticeLocationRepository.SaveAsync();
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
                PracticeLocationRepository.UpdateExistingPracticePaymentAndRemittance(practiceLocationDetailId, practicePaymentAndRemittance);

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task AddPracticePaymentAndRemittanceAsync(int practiceLocationDetailId, PracticePaymentAndRemittance practicePaymentAndRemittance)
        {
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

            PracticeLocationRepository.AddPracticePaymentAndRemittance(practiceLocationDetailId, practicePaymentAndRemittance);

            // Persisting practice location details
            await PracticeLocationRepository.SaveAsync();
        }

        #endregion

        #region Credentialing Contact

        public async Task AddCredentialingContactAsync(int practiceLocationDetailId, Employee credentialingContactPerson)
        {
            try
            {
                if (credentialingContactPerson.EmployeeID != 0)
                {
                    PracticeLocationRepository.AssignCredentialingContact(practiceLocationDetailId, credentialingContactPerson.EmployeeID);
                }

                else
                {
                    if (credentialingContactPerson.Departments == null)
                        credentialingContactPerson.Departments = new List<EmployeeDepartment>();

                    // Adding the association employee-department class to employee
                    credentialingContactPerson.Departments.Add(new EmployeeDepartment()
                    {
                        DepartmentID = departmentRepository.Get(d => d.Code == OrganizationDepartmentCode.CredentialingContact).FirstOrDefault().DepartmentID
                    });

                    if (credentialingContactPerson.Designations == null)
                        credentialingContactPerson.Designations = new List<EmployeeDesignation>();

                    // Adding the association employee-department class to employee
                    credentialingContactPerson.Designations.Add(new EmployeeDesignation() { DesignationID = EmployeeDesignationValue.PrimaryCredentialingContactDesignationId });

                    PracticeLocationRepository.AddCredentialingContact(practiceLocationDetailId, credentialingContactPerson);
                }

                // Persisting practice location details
                await PracticeLocationRepository.SaveAsync();

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
                PracticeLocationRepository.UpdatePrimaryCredentialingContact(practiceLocationDetailId, primaryCredentialingContactPerson);
                await PracticeLocationRepository.SaveAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

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

                //Update the worker compensation information
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

        #region Office Hours

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

        #endregion

        public int GetUserId(string userAuthId)
        {
            try
            {
               // var userRepo = uow.GetGenericRepository<CDUser>();

                var user = CDUserRepo.Find(u => u.AuthenicateUserId == userAuthId);

                return user.CDUserID;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
