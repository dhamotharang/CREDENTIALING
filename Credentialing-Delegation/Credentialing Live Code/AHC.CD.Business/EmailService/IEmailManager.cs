using AHC.CD.Data.ADO.DTO;
using AHC.CD.Entities.EmailNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.EmailService
{
    public interface IEmailManager
    {
        Task<IEnumerable<SentMailDTO>> GetAllSentMails();

        Task<IEnumerable<FollowUpMailDTO>> GetAllFollowUpMails();

        Task<EmailInfo> GetIndividualEmailDetail(int emailInfoId);
    }
}
