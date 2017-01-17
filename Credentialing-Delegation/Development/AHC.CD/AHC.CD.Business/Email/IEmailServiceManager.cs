using AHC.CD.Business.BusinessModels.EmailGroup;
using AHC.CD.Entities;
using AHC.CD.Entities.EmailNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Email
{
    public interface IEmailServiceManager
    {
        Task<object> GetAllEmailInfo();
        Task<IEnumerable<EmailTemplate>> GetAllEmailTemplatesAsync();
        EmailInfo SaveComposedEmail(EmailInfo email);
        Task<object> AddNewGroupEmail(EmailGroup groupemail, Dictionary<string, string> CduserIds, string AuthId);
        Task<Dictionary<string, List<string>>> CheckGroupMailId(List<string> Tolist);
        Task<object> UpdateGroupMailasync(EmailGroup groupMail, Dictionary<string, string> Dictionary, string AuthId);
        Task<string> RemoveIndividualMailFromGroupAsync(int Cduser_GroupMailId);
        Task<string> InactivateEmailGroupAsync(int EmailGroupId, string AuthId);
        Task<string> ActivateEmailGroupAsync(int EmailGroupId, string AuthId);
        Task<EmailInfo> StopFollowUpEmailAsync(int emailInfoID);
        Task<EmailInfo> StopFollowUpEmailForSelectReceiversAsync(int emailInfoID, List<int> emailRecipientIDs);
        Task<IEnumerable<EmailInfo>> GetAllActiveFollowUpEmailsAsync();
        Task<IEnumerable<EmailInfo>> GetAllInboxEmailsAsync(string authID);
        Task<List<string>> GetAllEmails();
        //Task<IEnumerable<EmailGroup>> GetAllGroupMailIdsAsync();
        Task<List<GroupMailDTO>> GetAllGroupMailsAsync(string AuthId);
        string SavePDFFile(string PDFFile);
        string SaveDelegatedPlanPDFFile(byte[] pdfbytes,int ccID);
        Task<List<CDUser>> GetAllCDusersasync();
        List<string> GetAllGroupMailNamesasync();
        List<string> GetAllEmailsForaGroup(int EmailGroupId);
        string SaveDocumentChecklistPDFFile(byte[] pdfbytes, int ccID);
        int GetCDUserIdFromAuthId(string UserAuthId);
    }
}
