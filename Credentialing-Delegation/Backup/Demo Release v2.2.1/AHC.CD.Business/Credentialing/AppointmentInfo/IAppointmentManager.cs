using AHC.CD.Entities.Credentialing.AppointmentInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.AppointmentInfo
{
    public interface IAppointmentManager
    {
        Task<int> UpdateAppointmentDetails(int credentialingInfoID, CredentialingAppointmentDetail credentialingAppointmentDetail, string authUserId);
        Task<List<int>> ScheduleAppointmentForMany(List<int> credentialingAppointmentDetails, DateTime appointmentDate, string authUserId);
        Task<int> RemoveScheduledAppointmentForIndividual(int credentialingAppointmentDetailID, string authUserId);
        Task<int> SaveResultForScheduledAppointment(int credentialingAppointmentDetailID, CredentialingAppointmentResult credentialingAppointmentResult, string authUserId);
    }
}
