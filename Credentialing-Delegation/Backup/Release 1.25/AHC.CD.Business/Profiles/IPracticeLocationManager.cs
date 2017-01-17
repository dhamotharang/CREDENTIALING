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
        Task UpdateOpenPracticeStatusAsync(int practiceLocationDetailId, OpenPracticeStatus openPracticeStatus);
        Task AddPracticeLocationAsync(int profileId, PracticeLocationDetail practiceLocationDetail);
        Task UpdatePracticeLocationAsync(PracticeLocationDetail practiceLocationDetail);

        Task AddPracticeProviderAsync(int practiceLocationDetailID, PracticeProvider practiceProvider);
        Task UpdatePracticeProviderAsync(PracticeProvider practiceProvider);
        Task RemovePracticeProviderAsync(PracticeProvider practiceProvider);

        Task UpdatePracticeBusinessManagerAsync(int practiceLocationDetailId, Employee businessOfficeManager);
        Task UpdatePracticeBillingContactAsync(int practiceLocationDetailId, Employee billingContactPerson);
        Task UpdatePaymentAndRemittanceAsync(int practiceLocationDetailId, PracticePaymentAndRemittance practicePaymentAndRemittance);

        Task UpdatePrimaryCredentialingContactAsync(int practiceLocationDetailId, Employee primaryCredentialingContactPerson);
        Task AddCredentialingContactAsync(int practiceLocationDetailId, Employee credentialingContactPerson);

        Task UpdateWorkersCompensationInformationAsync(int practiceLocationDetailId, WorkersCompensationInformation workersCompensationInformation);
        Task RenewWorkersCompensationInformationAsync(int PracticeLocationDetailID, WorkersCompensationInformation dataWorkersCompensationInfo);

        Task UpdateProviderOfficeHourAsync(int practiceLocationDetailId, ProviderPracticeOfficeHour providerPracticeOfficeHour);
    }
}
