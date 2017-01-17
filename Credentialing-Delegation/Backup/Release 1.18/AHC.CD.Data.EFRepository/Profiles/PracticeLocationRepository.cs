using AHC.CD.Data.Repository.Profiles;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
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

        public void UpdatePracticeBusinessManager(int practiceLocationDetailId, int businessOfficeManagerId)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail.BusinessOfficeManagerOrStaffId = businessOfficeManagerId;
                    Update(updatePracticeLocationDetail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdatePracticeOfficeHour(int practiceLocationDetailId, PracticeOfficeHour practiceOfficeHour)
        {
            try
            {
                //PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                //if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                //{
                //    if (updatePracticeLocationDetail.OfficeHour == null)
                //        updatePracticeLocationDetail.OfficeHour = practiceOfficeHour;
                //    else
                //    {
                //        updatePracticeLocationDetail.OfficeHour = AutoMapper.Mapper.Map<ProviderPracticeOfficeHour, ProviderPracticeOfficeHour>(practiceOfficeHour, updatePracticeLocationDetail.OfficeHour);

                //        foreach (var practiceDay in practiceOfficeHour.PracticeDays)
                //        {
                //            if (practiceDay.PracticeDayID != 0)
                //            {
                //                var hours = updatePracticeLocationDetail.OfficeHour.PracticeDays.FirstOrDefault(t => t.PracticeDayID == practiceDay.PracticeDayID);
                //                if (hours != null)
                //                {
                //                    hours = AutoMapper.Mapper.Map<PracticeDay, PracticeDay>(practiceDay, hours);
                //                }
                //                else
                //                    updatePracticeLocationDetail.OfficeHour.PracticeDays.Add(practiceDay);
                //            }
                //            else
                //                updatePracticeLocationDetail.OfficeHour.PracticeDays.Add(practiceDay);
                //        }

                //        Update(updatePracticeLocationDetail);
                //    }
                    
                //}
            }
            catch (Exception ex)
            {
                throw;
            }
        }

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

        public void UpdatePracticeBillingContact(int practiceLocationDetailId, int billingContactPersonId)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                  
                    //  ..Where(d => d.Code).FirstOrDefault();
                   // Department ddDepartment =FindAsync();
                    

                    updatePracticeLocationDetail.BillingContactPersonId = billingContactPersonId;
                    Update(updatePracticeLocationDetail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdatePaymentAndRemittance(int practiceLocationDetailId, PracticePaymentAndRemittance practicePaymentAndRemittance)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    if (updatePracticeLocationDetail.PaymentAndRemittance == null)
                        updatePracticeLocationDetail.PaymentAndRemittance = practicePaymentAndRemittance;
                    else
                    {
                        updatePracticeLocationDetail.PaymentAndRemittance = AutoMapper.Mapper.Map<PracticePaymentAndRemittance, PracticePaymentAndRemittance>(practicePaymentAndRemittance, updatePracticeLocationDetail.PaymentAndRemittance);
                        Update(updatePracticeLocationDetail);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdatePracticeColleague(int practiceLocationDetailId, PracticeColleague practiceColleague)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                //if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                //{
                //    if (updatePracticeLocationDetail.PracticeColleague == null)
                //        updatePracticeLocationDetail.PracticeColleague = practiceColleague;
                //    else
                //    {
                //        updatePracticeLocationDetail.PracticeColleague = AutoMapper.Mapper.Map<PracticeColleague, PracticeColleague>(practiceColleague, updatePracticeLocationDetail.PracticeColleague);
                //        Update(updatePracticeLocationDetail);
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdateMidLevelPractitioner(int practiceLocationDetailId, MidLevelPractitioner midLevelPractitioner)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                //if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                //{
                //    if (updatePracticeLocationDetail.MidLevelPractitioner == null)
                //        updatePracticeLocationDetail.MidLevelPractitioner = midLevelPractitioner;
                //    else
                //    {
                //        updatePracticeLocationDetail.MidLevelPractitioner = AutoMapper.Mapper.Map<MidLevelPractitioner, MidLevelPractitioner>(midLevelPractitioner, updatePracticeLocationDetail.MidLevelPractitioner);
                //        Update(updatePracticeLocationDetail);
                //    }
                //}
            }
            catch (Exception ex)
            {
                throw;
            }
        }


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
        //no need to pass id, one to one reln
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

        public void UpdatePaymentAndRemittanceAsync(int practiceLocationDetailId, int practicePaymentAndRemittanceId)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    updatePracticeLocationDetail.PaymentAndRemittanceId = practicePaymentAndRemittanceId;
                    Update(updatePracticeLocationDetail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdatePracticeBusinessManager(int practiceLocationDetailId, Entities.MasterData.Account.Staff.Employee businessOfficeManager)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {

                    updatePracticeLocationDetail.BusinessOfficeManagerOrStaff = AutoMapper.Mapper.Map<Employee, Employee>(businessOfficeManager, updatePracticeLocationDetail.BusinessOfficeManagerOrStaff);
                    Update(updatePracticeLocationDetail);                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void UpdatePracticeBillingContact(int practiceLocationDetailId, Entities.MasterData.Account.Staff.Employee billingContactPerson)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {

                    updatePracticeLocationDetail.BillingContactPerson = AutoMapper.Mapper.Map<Employee, Employee>(billingContactPerson, updatePracticeLocationDetail.BillingContactPerson);
                    Update(updatePracticeLocationDetail);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

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

        public int UpdateEmployeeForPaymentAndRemittance(int practiceLocationDetailId, Employee paymentEmployee)
        {
            try
            {
                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);

                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {
                    //updatePracticeLocationDetail.BillingContactPerson = AutoMapper.Mapper.Map<Employee, Employee>(paymentEmployee, updatePracticeLocationDetail.Facility.FacilityDetail.Employees);
                   // Update(updatePracticeLocationDetail);
                    return 0;
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        public void UpdateCredentialingContact(int practiceLocationDetailId, int credentialingContactPersonId)
        {
            try {

                PracticeLocationDetail updatePracticeLocationDetail = Find(practiceLocationDetailId);


                if (IsPracticeLocationDetailExists(practiceLocationDetailId, updatePracticeLocationDetail))
                {

                    updatePracticeLocationDetail.BillingContactPersonId = credentialingContactPersonId;
                    Update(updatePracticeLocationDetail);
                }
            
            }


            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
