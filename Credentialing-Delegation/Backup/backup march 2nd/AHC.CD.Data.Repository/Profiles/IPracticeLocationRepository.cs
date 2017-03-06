using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository.Profiles
{
    public interface IPracticeLocationRepository : IGenericRepository<PracticeLocationDetail>
    {
        void UpdateOpenPracticeStatus(int practiceLocationDetailId, OpenPracticeStatus openPracticeStatus);
        void UpdatePracticeLocation(PracticeLocationDetail practiceLocationDetail);
        void AddPracticeLocationHistory(int profileId, int practiceLocationDetailID,int CDUserID);
        void RemovePracticeLocation(int profileId, PracticeLocationDetail practiceLocationDetail);

        void AddPracticeProvider(int practiceLocationDetailID, PracticeProvider practiceProvider);
        PracticeProvider AddPracticeProviderHistory(int practiceLocationDetailID, int practiceProviderID);

        void UpdateExistingPracticeBusinessManager(int practiceLocationDetailId, Employee businessOfficeManager);
        void AddPracticeBusinessManager(int practiceLocationDetailId, Employee businessOfficeManager);

        void UpdateExistingPracticeBillingCongtact(int practiceLocationDetailId, Employee billingContactPerson);
        void AddPracticeBillingCongtact(int practiceLocationDetailId, Employee billingContactPerson);

        void UpdateExistingPracticePaymentAndRemittance(int practiceLocationDetailId, PracticePaymentAndRemittance practicePaymentAndRemittance);
        void AddPracticePaymentAndRemittance(int practiceLocationDetailId, PracticePaymentAndRemittance practicePaymentAndRemittance);

        void AssignCredentialingContact(int practiceLocationDetailId, int employeeId);
        void AddCredentialingContact(int practiceLocationDetailId, Employee credentialingContactPerson);
        void UpdatePrimaryCredentialingContact(int practiceLocationDetailId, Employee primaryCredentialingContactPerson);

        void UpdateWorkersCompensationInformation(int practiceLocationDetailId, WorkersCompensationInformation workersCompensationInformation);
        void AddWorkersCompensationHistory(int practiceLocationDetailID);

        void UpdateProviderOfficeHourAsync(int practiceLocationDetailId, ProviderPracticeOfficeHour providerPracticeOfficeHour);
    }
}
