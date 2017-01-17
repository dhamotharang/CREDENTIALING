using AHC.CD.Data.ADO.DTO;
using AHC.CD.Entities.EmailNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.EmailService
{
    public interface IEmailServiceRepository
    {
         Task<IEnumerable<SentMailDTO>> GetAllSentMails();

         Task<IEnumerable<FollowUpMailDTO>> GetAllOutboxMails();

         Task<EmailInfo> GetIndividualEmailDetail(int EmailInfoId);
    }
}
