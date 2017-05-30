using AHC.CD.Business.DocumentWriter;
using AHC.CD.Entities.Credentialing.AppointmentInformation;
using AHC.CD.Entities.Credentialing.CCMPortal;
using AHC.CD.Entities.Credentialing.Loading;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.AppointmentInfo
{
    public interface IAppointmentManager
    {
        Task<CredentialingAppointmentDetail> UpdateAppointmentDetails(int credentialingInfoID, DocumentDTO AttachDocument, CredentialingAppointmentDetail credentialingAppointmentDetail, string authUserId);
        Task<List<int>> ScheduleAppointmentForMany(List<int> credentialingAppointmentDetails, DateTime appointmentDate, string authUserId);
        Task<int> RemoveScheduledAppointmentForIndividual(int credentialingAppointmentDetailID, string authUserId);
        Task<int> SaveResultForScheduledAppointment(int credentialingAppointmentDetailID, DocumentDTO CCMDocument, CredentialingAppointmentResult credentialingAppointmentResult, string authUserId);
        Task<int> SaveResultForScheduledAppointmentwithdigitalsignature(int credentialingAppointmentDetailID, CredentialingAppointmentResult credentialingAppointmentResult, string authUserId);
        string GetCCMSignature(string userid);
        Task<IEnumerable<CredentialingInfo>> GetAllCredentialInfoList();
        Task<Object> GetAllCredentialInfoListHistory();
        Task<IEnumerable<CredentialingInfo>> GetAllFilteredCredentialInfoList();
        Task<CredentialingInfo> GetCredentialinfoByID(int id);
        Task<string> GenerateWelcomeLetter(int profileid, string welcomeletterinitialdate, string servicecommencingdate);
        Task<List<CCMAppiontment>> GetCCMAppointmentsInfo(string ApprovalStatus);

        Task<CCMActionDTO> GetCCMActionData(int CredInfoID);

        string AddDocument(string docRootPath, string docTitle, DateTime? expiryDate, DocumentDTO document);

        Task<dynamic> SaveCCMQuickActionResultsAsync(CCMQuickActionDTO CCMActionResult);
    }
}
