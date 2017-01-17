using AHC.CD.Entities.MasterData.Account.Branch;
using AHC.CD.Entities.MasterData.Account.Staff;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Profiles
{
    public interface IPracticeLocationManager
    {
        Task AddPracticeLocationAsync(int profileId, PracticeLocationDetail practiceLocationDetail);
        Task UpdatePracticeLocationAsync(PracticeLocationDetail practiceLocationDetail);

       
        Task UpdatePracticeOfficeHourAsync(int practiceLocationDetailId, PracticeOfficeHour practiceOfficeHour);
        Task UpdateProviderOfficeHourAsync(int practiceLocationDetailId, ProviderPracticeOfficeHour providerPracticeOfficeHour);
        Task UpdateOpenPracticeStatusAsync(int practiceLocationDetailId, OpenPracticeStatus openPracticeStatus);

        Task UpdatePracticeBillingContactAsync(int practiceLocationDetailId, Employee billingContactPerson);
        Task UpdatePracticeBusinessManagerAsync(int practiceLocationDetailId, Employee businessOfficeManager);
        Task UpdatePaymentAndRemittanceAsync(int practiceLocationDetailId, PracticePaymentAndRemittance practicePaymentAndRemittance);


        //Task UpdatePracticeBillingContactAsync(int practiceLocationDetailId, int billingContactPersonId);
        //Task UpdateEmployeeForPaymentAndRemittanceAsync(int practiceLocationDetailId, Employee paymentEmployee);
        //Task UpdatePracticeBusinessManagerAsync(int practiceLocationDetailId, Employee businessOfficeManager);
        //Task UpdatePracticeBusinessManagerAsync(int practiceLocationDetailId, int businessOfficeManagerId);
        //Task UpdateEmployeeForPaymentAndRemittanceAsync(int practiceLocationDetailId, Employee paymentEmployee);        
        //Task UpdatePaymentAndRemittanceAsync(int practiceLocationDetailId, int practicePaymentAndRemittanceId);


        Task UpdatePracticeColleagueAsync(int practiceLocationDetailId, PracticeColleague practiceColleague);


        Task UpdatePrimaryCredentialingContactAsync(int practiceLocationDetailId, Employee primaryCredentialingContactPerson);
        Task AddCredentialingContactAsync(int practiceLocationDetailId, Employee credentialingContactPerson);

        Task UpdateWorkersCompensationInformationAsync(int practiceLocationDetailId, WorkersCompensationInformation workersCompensationInformation);

        Task addMidLevelAsync(int practiceLocationDetailID, MidLevelPractitioner midLevel);
        Task UpdateMidLevelAsync(int practiceLocationDetailID, MidLevelPractitioner midLevel);

        Task AddSupervisingProviderAsync(int practiceLocationDetailID, SupervisingProvider supervisingProvider);
        Task UpdateSupervisingProviderAsync(int practiceLocationDetailID, SupervisingProvider supervisingProvider);

        Task RenewWorkersCompensationInformationAsync(int PracticeLocationDetailID, WorkersCompensationInformation dataWorkersCompensationInfo);

        Task AddPracticeColleagueAsync(int practiceLocationDetailID, PracticeColleague practiceColleague);

        Task AddPracticeProviderAsync(int practiceLocationDetailID, PracticeProvider practiceProvider);
        Task UpdatePracticeProviderAsync(PracticeProvider practiceProvider);
        Task RemovePracticeProviderAsync(PracticeProvider practiceProvider);
    }
}
