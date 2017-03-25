using AHC.CD.Data.ADO.EmailService;
using AHC.CD.Entities.EmailNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.EmailService
{
    public class EmailManager : IEmailManager
    {
        private IEmailServiceRepository emailRepository = null;

        public EmailManager(IEmailServiceRepository emailRepository)
        {
            this.emailRepository = emailRepository;                
        }

        public async Task<IEnumerable<Data.ADO.DTO.SentMailDTO>> GetAllSentMails()
        {
            try
            {
                return await emailRepository.GetAllSentMails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Data.ADO.DTO.FollowUpMailDTO>> GetAllFollowUpMails()
        {
            try
            {
                return await emailRepository.GetAllOutboxMails();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmailInfo> GetIndividualEmailDetail(int emailInfoId)
        {
            try
            {
                return await emailRepository.GetIndividualEmailDetail(emailInfoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
