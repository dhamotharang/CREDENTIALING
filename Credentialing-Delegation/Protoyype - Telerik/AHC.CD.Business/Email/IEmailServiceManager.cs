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
        Task<EmailInfo> SaveComposedEmail(EmailInfo email);
        Task<EmailInfo> StopFollowUpEmailAsync(int emailInfoID);
        Task<EmailInfo> StopFollowUpEmailForSelectReceiversAsync(int emailInfoID, List<int> emailRecipientIDs);
        Task<IEnumerable<EmailInfo>> GetAllActiveFollowUpEmailsAsync();
        Task<IEnumerable<EmailInfo>> GetAllInboxEmailsAsync(string authID);
        Task<List<string>> GetAllEmails();
    }
}
