using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterData.Account.Staff;
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
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetail.PracticeLocationDetailID);

                if (IsPracticeLocationDetailExists(practiceLocationDetail.PracticeLocationDetailID, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail = AutoMapper.Mapper.Map<PracticeLocationDetail, PracticeLocationDetail>(practiceLocationDetail, updatePracticeLocationDetail);
                    Update(updatePracticeLocationDetail);
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
                    updatePracticeLocationDetail.OfficeHour = providerPracticeOfficeHour;
                    Update(updatePracticeLocationDetail);
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
