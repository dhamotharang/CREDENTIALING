using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterData.Account;
using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository.Profiles
{
    class PracticeLocationRepository : EFGenericRepository<PracticeLocationDetail>, IPracticeLocationRepository
    {        

        #region Private Methods

        private PracticeLocationDetail Find(int practiceLocationDetail)
        {
            return DbSet.Find(practiceLocationDetail);
        }


        private bool IsPracticeLocationDetailExists(int practiceLocationDetailId, PracticeLocationDetail practiceLocationDetail)
        {
            if (practiceLocationDetail == null)
            {
                throw new Exception("Location detail not found");
            }
            return true;
        }

        #endregion

        #region Practice General Information

        public void UpdatePracticeLocation(PracticeLocationDetail practiceLocationDetail)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(x => x.PracticeLocationDetailID == practiceLocationDetail.PracticeLocationDetailID, "BusinessOfficeManagerOrStaff, PaymentAndRemittance, OfficeHour, OpenPracticeStatus, BillingContactPerson, PracticeProviders, PrimaryCredentialingContactPerson, WorkersCompensationInformation, Facility, Organization, Group, PracticeLocationDetailHistory");
                Employee businessOfficeManagerOrStaff = updatePracticeLocationDetail.BusinessOfficeManagerOrStaff;
                Employee billingContactPerson = updatePracticeLocationDetail.BillingContactPerson;
                PracticePaymentAndRemittance paymentAndRemittance = updatePracticeLocationDetail.PaymentAndRemittance;
                ProviderPracticeOfficeHour officeHour = updatePracticeLocationDetail.OfficeHour;
                OpenPracticeStatus openPracticeStatus = updatePracticeLocationDetail.OpenPracticeStatus;
                ICollection<PracticeProvider> practiceProviders = updatePracticeLocationDetail.PracticeProviders;
                Employee primaryCredentialingContactPerson = updatePracticeLocationDetail.PrimaryCredentialingContactPerson;
                WorkersCompensationInformation workersCompensationInformation = updatePracticeLocationDetail.WorkersCompensationInformation;
                Facility facility = updatePracticeLocationDetail.Facility;
                Organization organization = updatePracticeLocationDetail.Organization;
                PracticingGroup group = updatePracticeLocationDetail.Group;
                ICollection<PracticeLocationDetailHistory> practiceLocationDetailHistory = updatePracticeLocationDetail.PracticeLocationDetailHistory;

                if (IsPracticeLocationDetailExists(practiceLocationDetail.PracticeLocationDetailID, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail = AutoMapper.Mapper.Map<PracticeLocationDetail, PracticeLocationDetail>(practiceLocationDetail, updatePracticeLocationDetail);
                    Update(updatePracticeLocationDetail);
                    updatePracticeLocationDetail.BusinessOfficeManagerOrStaff = businessOfficeManagerOrStaff;
                    updatePracticeLocationDetail.BillingContactPerson = billingContactPerson;
                    updatePracticeLocationDetail.PaymentAndRemittance = paymentAndRemittance;
                    updatePracticeLocationDetail.OfficeHour = officeHour;
                    updatePracticeLocationDetail.OpenPracticeStatus = openPracticeStatus;
                    updatePracticeLocationDetail.PracticeProviders = practiceProviders;
                    updatePracticeLocationDetail.PrimaryCredentialingContactPerson = primaryCredentialingContactPerson;
                    updatePracticeLocationDetail.WorkersCompensationInformation = workersCompensationInformation;
                    updatePracticeLocationDetail.Facility = facility;
                    updatePracticeLocationDetail.Organization = organization;
                    updatePracticeLocationDetail.Group = group;
                    updatePracticeLocationDetail.PracticeLocationDetailHistory = practiceLocationDetailHistory;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddPracticeLocationHistory(int profileId, int practiceLocationDetailID)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Get(p => p.PracticeLocationDetailID == practiceLocationDetailID, "BusinessOfficeManagerOrStaff, BusinessOfficeManagerOrStaff.Designations, BusinessOfficeManagerOrStaff.Designations.Designation, BusinessOfficeManagerOrStaff.Departments, BusinessOfficeManagerOrStaff.Departments.Department, PaymentAndRemittance, PaymentAndRemittance.PaymentAndRemittancePerson, PaymentAndRemittance.PaymentAndRemittancePerson.Departments, PaymentAndRemittance.PaymentAndRemittancePerson.Departments.Department, PaymentAndRemittance.PaymentAndRemittancePerson.Designations, PaymentAndRemittance.PaymentAndRemittancePerson.Designations.Designation, OfficeHour, OfficeHour.PracticeDays, OfficeHour.PracticeDays.DailyHours, OpenPracticeStatus, OpenPracticeStatus.PracticeQuestionAnswers, OpenPracticeStatus.PracticeQuestionAnswers.Question, BillingContactPerson, BillingContactPerson.Departments, BillingContactPerson.Departments.Department, BillingContactPerson.Designations, BillingContactPerson.Designations.Designation, PracticeProviders, PracticeProviders.PracticeProviderSpecialties, PracticeProviders.PracticeProviderSpecialties.Specialty, PracticeProviders.PracticeProviderTypes, PracticeProviders.PracticeProviderTypes.ProviderType, PrimaryCredentialingContactPerson, PrimaryCredentialingContactPerson.Departments, PrimaryCredentialingContactPerson.Departments.Department, PrimaryCredentialingContactPerson.Designations, PrimaryCredentialingContactPerson.Designations.Designation, WorkersCompensationInformation, Facility, Facility.FacilityDetail, Facility.FacilityDetail.Service, Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers, Facility.FacilityDetail.Service.FacilityServiceQuestionAnswers.Question, Facility.FacilityDetail.Accessibility, Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers, Facility.FacilityDetail.Accessibility.FacilityAccessibilityQuestionAnswers.Question, Facility.FacilityDetail.Language, Facility.FacilityDetail.Language.NonEnglishLanguages, Facility.FacilityDetail.Employees, Facility.FacilityDetail.Employees.Employee, Facility.FacilityDetail.Employees.Employee.Departments, Facility.FacilityDetail.Employees.Employee.Departments.Department, Facility.FacilityDetail.Employees.Employee.Designations, Facility.FacilityDetail.Employees.Employee.Designations.Designation, Facility.FacilityDetail.FacilityWorkHours, Facility.FacilityDetail.PracticeOfficeHour, Facility.FacilityDetail.PracticeOfficeHour.PracticeDays, Facility.FacilityDetail.PracticeOfficeHour.PracticeDays.DailyHours, Facility.FacilityDetail.FacilityPracticeProviders, Facility.FacilityDetail.FacilityPracticeProviders.FacilityPracticeProviderSpecialties, Facility.FacilityDetail.FacilityPracticeProviders.FacilityPracticeProviderTypes, Organization, Organization.OrganizationType, Group, Group.Group").FirstOrDefault();
                if (IsPracticeLocationDetailExists(practiceLocationDetailID, updatePracticeLocationDetail))
                {
                    PracticeLocationDetailHistory addPracticeLocationDetailHistory = new PracticeLocationDetailHistory();
                    addPracticeLocationDetailHistory = AutoMapper.Mapper.Map<PracticeLocationDetail, PracticeLocationDetailHistory>(updatePracticeLocationDetail, addPracticeLocationDetailHistory);
                    addPracticeLocationDetailHistory.Facility = updatePracticeLocationDetail.Facility;
                    addPracticeLocationDetailHistory.BillingContactPerson = updatePracticeLocationDetail.BillingContactPerson;
                    addPracticeLocationDetailHistory.BusinessOfficeManagerOrStaff = updatePracticeLocationDetail.BusinessOfficeManagerOrStaff;
                    addPracticeLocationDetailHistory.OpenPracticeStatus = updatePracticeLocationDetail.OpenPracticeStatus;
                    addPracticeLocationDetailHistory.OfficeHour = updatePracticeLocationDetail.OfficeHour;
                    addPracticeLocationDetailHistory.PaymentAndRemittance = updatePracticeLocationDetail.PaymentAndRemittance;
                    addPracticeLocationDetailHistory.Organization = updatePracticeLocationDetail.Organization;
                    addPracticeLocationDetailHistory.PrimaryCredentialingContactPerson = updatePracticeLocationDetail.PrimaryCredentialingContactPerson;
                    addPracticeLocationDetailHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updatePracticeLocationDetail.PracticeLocationDetailHistory == null)
                    {
                        updatePracticeLocationDetail.PracticeLocationDetailHistory = new List<PracticeLocationDetailHistory>();
                    }
                    updatePracticeLocationDetail.PracticeLocationDetailHistory.Add(addPracticeLocationDetailHistory);
                    Update(updatePracticeLocationDetail);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void RemovePracticeLocation(int profileId, PracticeLocationDetail practiceLocationDetail)
        {
            try
            {
                PracticeLocationDetail removePracticeLocationDetail = Find(practiceLocationDetail.PracticeLocationDetailID);
                if (IsPracticeLocationDetailExists(practiceLocationDetail.PracticeLocationDetailID, removePracticeLocationDetail))
                {
                    removePracticeLocationDetail.StatusType = practiceLocationDetail.StatusType;
                    Update(removePracticeLocationDetail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Open Practice Status

        public void UpdateOpenPracticeStatus(int practiceLocationDetailId, OpenPracticeStatus openPracticeStatus)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    if (updatePracticeLocationDetail.OpenPracticeStatus == null)
                        updatePracticeLocationDetail.OpenPracticeStatus = openPracticeStatus;
                    else
                    {
                        updatePracticeLocationDetail.OpenPracticeStatus = AutoMapper.Mapper.Map<OpenPracticeStatus, OpenPracticeStatus>(openPracticeStatus, updatePracticeLocationDetail.OpenPracticeStatus);
                        Update(updatePracticeLocationDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Practice Providers

        public void AddPracticeProvider(int practiceLocationDetailID, PracticeProvider practiceProvider)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = this.Find(p => p.PracticeLocationDetailID == practiceLocationDetailID, "PracticeProviders");

                if (IsPracticeLocationDetailExists(practiceLocationDetailID, updatePracticeLocationDetail))
                {
                    if (updatePracticeLocationDetail.PracticeProviders == null)
                        updatePracticeLocationDetail.PracticeProviders = new List<PracticeProvider>();

                    updatePracticeLocationDetail.PracticeProviders.Add(practiceProvider);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public PracticeProvider AddPracticeProviderHistory(int practiceLocationDetailID, int practiceProviderID)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = this.Find(p => p.PracticeLocationDetailID == practiceLocationDetailID, "PracticeProviders, PracticeProviders.PracticeProviderHistory, PracticeProviders.PracticeProviderTypes, PracticeProviders.PracticeProviderSpecialties ");
                //PracticeProvider updatePracticeProvider = Get(practiceProviderID);
                PracticeProvider updatePracticeProvider = updatePracticeLocationDetail.PracticeProviders.FirstOrDefault(pp => pp.PracticeProviderID == practiceProviderID);
                if (IsPracticeLocationDetailExists(practiceLocationDetailID, updatePracticeLocationDetail))
                {
                    PracticeProviderHistory addPracticeProviderHistory = new PracticeProviderHistory();
                    addPracticeProviderHistory = AutoMapper.Mapper.Map<PracticeProvider, PracticeProviderHistory>(updatePracticeProvider, addPracticeProviderHistory);
                    addPracticeProviderHistory.HistoryStatusType = HistoryStatusType.Deleted;
                    if (updatePracticeProvider.PracticeProviderHistory == null)
                    {
                        updatePracticeProvider.PracticeProviderHistory = new List<PracticeProviderHistory>();
                    }
                    updatePracticeProvider.PracticeProviderHistory.Add(addPracticeProviderHistory);
                    Update(updatePracticeLocationDetail);
                    return updatePracticeProvider;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        #endregion

        #region Business Office Manager

        public void UpdateExistingPracticeBusinessManager(int practiceLocationDetailId, Employee businessOfficeManager)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = this.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId, "BusinessOfficeManagerOrStaff");

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail.BusinessOfficeManagerOrStaff = Mapper.Map<Employee, Employee>(businessOfficeManager, updatePracticeLocationDetail.BusinessOfficeManagerOrStaff);
                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddPracticeBusinessManager(int practiceLocationDetailId, Employee businessOfficeManager)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = this.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail.BusinessOfficeManagerOrStaff = businessOfficeManager;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Billing Contact

        public void UpdateExistingPracticeBillingCongtact(int practiceLocationDetailId, Employee billingContactPerson)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = this.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId, "BillingContactPerson");

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail.BillingContactPerson = Mapper.Map<Employee, Employee>(billingContactPerson, updatePracticeLocationDetail.BillingContactPerson);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddPracticeBillingCongtact(int practiceLocationDetailId, Employee billingContactPerson)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = this.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail.BillingContactPerson = billingContactPerson;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Payment and Remittance

        public void UpdateExistingPracticePaymentAndRemittance(int practiceLocationDetailId, PracticePaymentAndRemittance practicePaymentAndRemittance)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = this.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId, "PaymentAndRemittance");

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail.PaymentAndRemittance.PaymentAndRemittancePerson = Mapper.Map<Employee, Employee>(practicePaymentAndRemittance.PaymentAndRemittancePerson, updatePracticeLocationDetail.PaymentAndRemittance.PaymentAndRemittancePerson);
                    updatePracticeLocationDetail.PaymentAndRemittance = Mapper.Map<PracticePaymentAndRemittance, PracticePaymentAndRemittance>(practicePaymentAndRemittance, updatePracticeLocationDetail.PaymentAndRemittance);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddPracticePaymentAndRemittance(int practiceLocationDetailId, PracticePaymentAndRemittance practicePaymentAndRemittance)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = this.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail.PaymentAndRemittance = practicePaymentAndRemittance;

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Credentialing Contact

        public void AssignCredentialingContact(int practiceLocationDetailId, int employeeId)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = this.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail.PrimaryCredentialingContactPersonId = employeeId;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void AddCredentialingContact(int practiceLocationDetailId, Employee credentialingContactPerson)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = this.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail.PrimaryCredentialingContactPerson = credentialingContactPerson;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdatePrimaryCredentialingContact(int practiceLocationDetailId, Employee primaryCredentialingContactPerson)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = this.Find(p => p.PracticeLocationDetailID == practiceLocationDetailId, "PrimaryCredentialingContactPerson");

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail.PrimaryCredentialingContactPerson = Mapper.Map<Employee, Employee>(primaryCredentialingContactPerson, updatePracticeLocationDetail.PrimaryCredentialingContactPerson);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Worker Compensation

        public void UpdateWorkersCompensationInformation(int practiceLocationDetailId, WorkersCompensationInformation workersCompensationInformation)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    if (updatePracticeLocationDetail.WorkersCompensationInformation == null)
                        updatePracticeLocationDetail.WorkersCompensationInformation = workersCompensationInformation;
                    else
                    {
                        updatePracticeLocationDetail.WorkersCompensationInformation = AutoMapper.Mapper.Map<WorkersCompensationInformation, WorkersCompensationInformation>(workersCompensationInformation, updatePracticeLocationDetail.WorkersCompensationInformation);
                        Update(updatePracticeLocationDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        
        public void AddWorkersCompensationHistory(int practiceLocationDetailID)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Get(pld => pld.PracticeLocationDetailID == practiceLocationDetailID, "WorkersCompensationInformation").FirstOrDefault<PracticeLocationDetail>();

                if (IsPracticeLocationDetailExists(practiceLocationDetailID, updatePracticeLocationDetail))
                {

                    updatePracticeLocationDetail.WorkersCompensationInformation.WorkersCompensationInfoHistory.Add(new WorkersCompensationInfoHistory()
                    {
                        IssueDate = updatePracticeLocationDetail.WorkersCompensationInformation.IssueDate.Value,
                        ExpirationDate = updatePracticeLocationDetail.WorkersCompensationInformation.ExpirationDate.Value,
                    });
                    Update(updatePracticeLocationDetail);
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Office Hours

        public void UpdateProviderOfficeHourAsync(int practiceLocationDetailId, ProviderPracticeOfficeHour providerPracticeOfficeHour)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    //updatePracticeLocationDetail.OfficeHour = providerPracticeOfficeHour;
                    //Update(updatePracticeLocationDetail);
                    if (updatePracticeLocationDetail.OfficeHour == null)
                        updatePracticeLocationDetail.OfficeHour = providerPracticeOfficeHour;
                    else
                    {
                        updatePracticeLocationDetail.OfficeHour = providerPracticeOfficeHour;
                        //updatePracticeLocationDetail.OfficeHour = AutoMapper.Mapper.Map<ProviderPracticeOfficeHour, ProviderPracticeOfficeHour>(providerPracticeOfficeHour, updatePracticeLocationDetail.OfficeHour);
                        Update(updatePracticeLocationDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
