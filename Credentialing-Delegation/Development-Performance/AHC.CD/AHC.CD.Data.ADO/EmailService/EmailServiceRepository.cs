using AHC.CD.Data.ADO.CoreRepository;
using AHC.CD.Data.ADO.DTO;
using AHC.CD.Entities.EmailNotifications;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using AHC.CD.Resources;

namespace AHC.CD.Data.ADO.EmailService
{
    internal class EmailServiceRepository : IEmailServiceRepository
    {
        AHC.CD.Data.ADO.CoreRepository.ADORepository ADORepository;

        public EmailServiceRepository()
        {
            ADORepository = new AHC.CD.Data.ADO.CoreRepository.ADORepository();
        }


        public async Task<IEnumerable<SentMailDTO>> GetAllSentMails()
        {
            DataTable table = new DataTable();
            List<SentMailDTO> sentMails = new List<SentMailDTO>();
            using (SqlConnection conn = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = AHC.CD.Resources.DatabaseQueries.AdoQueries.SENTMAILS_QUERY;
                    table = ADORepository.GetData(cmd);
                }

                foreach (DataRow row in table.Rows)
                {
                    SentMailDTO sentmail = new SentMailDTO();
                    sentmail.EmailInfoID = int.Parse(row["EmailInfoID"].ToString());
                    sentmail.SendingDate = row["SendingDate"].ToString();
                    sentmail.Subject = row["Subject"].ToString();
                    sentmail.Recepients = row["Recipient"].ToString();
                    sentMails.Add(sentmail);
                }
            }
            return sentMails;
        }

        public async Task<IEnumerable<DTO.FollowUpMailDTO>> GetAllOutboxMails()
        {
            DataTable table = new DataTable();
            List<FollowUpMailDTO> followupMails = new List<FollowUpMailDTO>();
            using (SqlConnection conn = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = AHC.CD.Resources.DatabaseQueries.AdoQueries.FOLLOWUPMAILS_QUERY;
                    table = ADORepository.GetData(cmd);
                }

                foreach (DataRow row in table.Rows)
                {
                    FollowUpMailDTO followupmail = new FollowUpMailDTO();
                    followupmail.EmailInfoID = int.Parse(row["EmailInfoID"].ToString());
                    followupmail.SendingDate = row["SendingDate"].ToString();
                    followupmail.Subject = row["Subject"].ToString();
                    followupmail.NextFollowUpDate = row["NextMailingDate"].ToString();
                    followupmail.Recepients = row["Recipient"].ToString();
                    followupMails.Add(followupmail);
                }
            }
            return followupMails;
        }

        public async Task<EmailInfo> GetIndividualEmailDetail(int EmailInfoId)
        {
            DataTable table = new DataTable();
            EmailInfo email = new EmailInfo();
            List<EmailRecipientDetail> EmailRecipients = new List<EmailRecipientDetail>();
            List<EmailAttachment> EmailAttachments = new List<EmailAttachment>();
            using (SqlConnection conn = new SqlConnection(ADORepository.GetConnectionString(DataBaseSchemaEnum.CredentialingConnectionString)))
            {
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = AHC.CD.Resources.DatabaseQueries.AdoQueries.INDIVIDUALSENTMAIL_QUERY;
                    SqlParameter p1 = cmd.CreateParameter();
                    p1.ParameterName = "@EmailInfoId";
                    p1.Value = EmailInfoId;
                    cmd.Parameters.Add(p1);
                    table = ADORepository.GetData(cmd);
                }

                foreach (DataRow row in table.Rows)
                {
                    string[] bccrecipientslist = null;
                    string[] ccrecipientslist = null;
                    email.Body = row["Body"].ToString();
                    email.EmailInfoID = int.Parse(row["EmailInfoID"].ToString());
                    string res = row["AttachmentRelativePath"].ToString();
                    string res1 = row["AttachmentServerPath"].ToString();
                    EmailAttachments.Add(new EmailAttachment { AttachmentRelativePath = res, AttachmentServerPath = res1 });
                    var ToRecipients = row["RecipientTo"].ToString().Split(';');
                    EmailRecipients = (ToRecipients.Select(x => new EmailRecipientDetail { Recipient = x, RecipientType = "To", RecipientTypeCategory = AHC.CD.Entities.MasterData.Enums.RecipientType.To })).ToList();
                    string CcRecipients = row["RecipientCC"].ToString();
                    ccrecipientslist = CcRecipients == "" ? bccrecipientslist : CcRecipients.Split(';');
                    if (ccrecipientslist != null)
                    {
                        EmailRecipients.AddRange(ccrecipientslist.Select(x => new EmailRecipientDetail { Recipient = x, RecipientType = "CC", RecipientTypeCategory = AHC.CD.Entities.MasterData.Enums.RecipientType.CC }));
                    }
                    string BccRecipients = row["RecipientBCC"].ToString();
                    bccrecipientslist = BccRecipients == "" ? bccrecipientslist : BccRecipients.Split(';');
                    if (bccrecipientslist != null)
                    {
                        EmailRecipients.AddRange(bccrecipientslist.Select(x => new EmailRecipientDetail { Recipient = x, RecipientType = "BCC", RecipientTypeCategory = AHC.CD.Entities.MasterData.Enums.RecipientType.BCC }));
                    }
                    email.EmailRecipients = EmailRecipients;
                    email.From = row["From"].ToString();
                    email.Subject = row["Subject"].ToString();
                    email.SendingDate = Convert.ToDateTime(row["SendingDate"]);
                    email.IsRecurrenceEnabled = row["IsRecurrenceEnabled"].ToString();
                    //email.StatusType = (AHC.CD.Entities.MasterData.Enums.StatusType)row["Status"];
                }
                email.EmailAttachments = EmailAttachments;
            }
            return email;
        }
    }
}
