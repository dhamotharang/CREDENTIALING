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
        void UpdatePracticeLocation(PracticeLocationDetail practiceLocationDetail);

        void UpdatePracticeBusinessManager(int practiceLocationDetailId, Employee businessOfficeManager);     
        void UpdatePracticeBusinessManager(int practiceLocationDetailId, int businessOfficeManagerId);

        void UpdatePracticeOfficeHour(int practiceLocationDetailId, PracticeOfficeHour practiceOfficeHour);
        void UpdateOpenPracticeStatus(int practiceLocationDetailId, OpenPracticeStatus openPracticeStatus);

        void UpdatePracticeBillingContact(int practiceLocationDetailId, Employee billingContactPerson); 
        void UpdatePracticeBillingContact(int practiceLocationDetailId, int billingContactPersonId);

        int  UpdateEmployeeForPaymentAndRemittance(int practiceLocationDetailId, Employee paymentEmployee); 
        void UpdatePaymentAndRemittance(int practiceLocationDetailId, PracticePaymentAndRemittance practicePaymentAndRemittance);
        void UpdatePaymentAndRemittanceAsync(int practiceLocationDetailId, int practicePaymentAndRemittanceId);

        void UpdatePracticeColleague(int practiceLocationDetailId, PracticeColleague practiceColleague);
        void UpdateMidLevelPractitioner(int practiceLocationDetailId, MidLevelPractitioner midLevelPractitioner);
        void UpdatePrimaryCredentialingContact(int practiceLocationDetailId, int primaryCredentialingContactPersonId);
        void UpdateWorkersCompensationInformation(int practiceLocationDetailId, WorkersCompensationInformation workersCompensationInformation);

        void UpdateProviderOfficeHourAsync(int practiceLocationDetailId, ProviderPracticeOfficeHour providerPracticeOfficeHour);

        void AddWorkersCompensationHistory(int practiceLocationDetailID);

    }
}
