using AHC.CD.Entities.Credentialing.Loading;
using AHC.CD.Entities.Credentialing.LoadingInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Credentialing.CnD
{
    public interface IApplicationManager
    {
        Task<CredentialingInfo> GetCredentialingInfoByIdAsync(int credInfo);
        Task<object> GetCredentialingInfoByProfileId(int profileID);
        Task<object> GetCredentialingInfoById(int credInfoID);
        Task<CredentialingInfo> GetCredentialingFilterInfoByIdAsync(int credInfo);

        Task<List<AHC.CD.Entities.EmailNotifications.EmailAttachment>> GetGeneratedPackagesAsync(int credInfo);

        Task<string> SetCredentialingInfoStatusById(int credInfoID,string authId);

        Task<TimelineActivity> AddTimelineActivityAsync(int credInfoId, Entities.Credentialing.LoadingInformation.TimelineActivity dataModelTimelineActivity);
    }
}
