using AHC.CD.Data.Repository;
using AHC.CD.Entities.EmailNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace AHC.MailService
{
    /// <summary>
    /// Author: Venkat
    /// Date: 30/10/2014
    /// Sends email with multiple attachments
    /// Required to config the smtp host and port number in the web.config file
    /// </summary>
    public class EmailSender : AHC.MailService.IEmailSender
    {
        private IGenericRepository<EmailInfo> emailInfoRepository = null;

        public EmailSender()
        {

        }

        public EmailSender(IUnitOfWork uow)
        {
            emailInfoRepository = uow.GetGenericRepository<EmailInfo>();
            //profileRepository = uow.GetProfileRepository();
        }

        public bool SendMail(EMailModel emailModel)
        {
            bool isSent = true;
            MailMessage mailMessage = new MailMessage();
            if (!string.IsNullOrEmpty(emailModel.From))
            {
                mailMessage.From = new MailAddress(emailModel.From);
            }
            string[] toAddressList = emailModel.To.Split(',');
            foreach (string toAddress in toAddressList)
            {
                mailMessage.To.Add(new MailAddress(toAddress));
                //mailMessage.To.Add(toAddress);
            }
            
            if(emailModel.Bcc != null)
            {
                string[] bccAddressList = emailModel.Bcc.Split(',');
                foreach (string bccAddress in bccAddressList)
                {
                    mailMessage.Bcc.Add(new MailAddress(bccAddress));
                    //mailMessage.Bcc.Add(bccAddress);
                    
                }
            }
                
            if(emailModel.Cc != null)
            {
                string[] ccAddressList = emailModel.Cc.Split(',');
                foreach (string ccAddress in ccAddressList)
                {
                    mailMessage.Bcc.Add(new MailAddress(ccAddress));
                    //mailMessage.Bcc.Add(ccAddress);
                }
            }
                
            mailMessage.Subject = emailModel.Subject;
            //mailMessage.SubjectEncoding = System.Text.Encoding.UTF8;
            mailMessage.Body = emailModel.Body;
            //mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.IsBodyHtml = true;


            if (emailModel.Attachments != null && emailModel.Attachments.Count > 0)
            {
                foreach (var file in emailModel.Attachments)
                {
                    mailMessage.Attachments.Add(new Attachment(file.Item1, file.Item2));
                }
            }

            
            
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Send(mailMessage);
            mailMessage.Dispose();
            //smtpClient.SendAsync(mailMessage, isSent);
            //Task.Factory.StartNew(() =>
            //{
            //    smtpClient.Send(mailMessage);
            //    mailMessage.Dispose();
            //});
            
            return isSent;
        }

        /// <summary>
        /// Method to send emails stacked in the DB
        /// </summary>
        /// <returns></returns>
        public bool SendMail()
        {
            List<EmailInfo> emailStack = emailInfoRepository.Get(e => e.Status != AHC.CD.Entities.MasterData.Enums.StatusType.Inactive.ToString(), "EmailRecipients.EmailTracker, EmailRecurrenceDetail").ToList();

            try
            {
                if (emailStack != null || emailStack.Count != 0)
                {
                    foreach (EmailInfo email in emailStack)
                    {
                        string toList = "";
                        string ccList = "";
                        string bccList = "";
                        foreach (EmailRecipientDetail emailRecipient in email.EmailRecipients)
                        {
                            EmailTracker emailTracker = new EmailTracker();
                            if (emailRecipient.EmailTracker == null || emailRecipient.EmailTracker.Count == 0)
                            {
                                if (emailRecipient.RecipientType == AHC.CD.Entities.MasterData.Enums.RecipientType.To.ToString())
                                {
                                    toList = toList + emailRecipient.Recipient + ",";
                                }
                                else if (emailRecipient.RecipientType == AHC.CD.Entities.MasterData.Enums.RecipientType.CC.ToString())
                                {
                                    ccList = ccList + emailRecipient.Recipient + ",";
                                }
                                else if (emailRecipient.RecipientType == AHC.CD.Entities.MasterData.Enums.RecipientType.BCC.ToString())
                                {
                                    bccList = bccList + emailRecipient.Recipient + ",";
                                } if (emailRecipient.EmailTracker == null)
                                {
                                    emailRecipient.EmailTracker = new List<EmailTracker>();
                                }
                                emailTracker.EmailStatusTypeCategory = AHC.CD.Entities.MasterData.Enums.EmailStatusType.Sent;
                                emailTracker.EmailTrackerDate = DateTime.Now;
                                emailTracker.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                                emailRecipient.EmailTracker.Add(emailTracker);
                            }
                        }
                        
                        if (toList != "")
                        {
                            emailInfoRepository.Update(email);
                            emailInfoRepository.Save();

                            MailMessage mailMessage = new MailMessage();
                            if (!string.IsNullOrEmpty(email.From))
                            {
                                mailMessage.From = new MailAddress(email.From);
                            }

                            toList = toList.TrimEnd(',');
                            ccList = ccList.TrimEnd(',');
                            bccList = bccList.TrimEnd(',');

                            string[] toAddressList = toList.Split(',');
                            foreach (string toAddress in toAddressList)
                            {
                                mailMessage.To.Add(new MailAddress(toAddress));
                            }

                            if (bccList != "")
                            {
                                string[] bccAddressList = bccList.Split(',');
                                foreach (string bccAddress in bccAddressList)
                                {
                                    mailMessage.Bcc.Add(new MailAddress(bccAddress));
                                }
                            }

                            if (ccList != "")
                            {
                                string[] ccAddressList = ccList.Split(',');
                                foreach (string ccAddress in ccAddressList)
                                {
                                    mailMessage.CC.Add(new MailAddress(ccAddress));
                                }
                            }

                            mailMessage.Subject = email.Subject;
                            mailMessage.Body = email.Body;
                            mailMessage.IsBodyHtml = true;

                            SmtpClient smtpClient = new SmtpClient();
                            try
                            {
                                smtpClient.Send(mailMessage);
                                //EmailTracker emailTracker = new EmailTracker();
                                //foreach (var emailRecipient in email.EmailRecipients)
                                //{
                                //    if (emailRecipient.EmailTracker == null)
                                //    {
                                //        emailRecipient.EmailTracker = new List<EmailTracker>();
                                //    }                                    
                                //    emailTracker.EmailStatusTypeCategory = AHC.CD.Entities.MasterData.Enums.EmailStatusType.Sent;
                                //    emailTracker.EmailTrackerDate = DateTime.Now;
                                //    emailTracker.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                                //    emailRecipient.EmailTracker.Add(emailTracker);
                                //}
                                //emailInfoRepository.Update(email);
                                //emailInfoRepository.Save();
                            }
                            catch (System.Net.Mail.SmtpFailedRecipientsException ex)
                            {
                                EmailTracker failureEmailTracker = new EmailTracker();
                                foreach (var emailRecipient in email.EmailRecipients)
                                {
                                    if (emailRecipient.Recipient.Equals(ex.FailedRecipient.TrimStart('<').TrimEnd('>')))
                                    {
                                        if (emailRecipient.EmailTracker == null)
                                        {
                                            emailRecipient.EmailTracker = new List<EmailTracker>();
                                        }   
                                        failureEmailTracker.EmailStatusTypeCategory = AHC.CD.Entities.MasterData.Enums.EmailStatusType.SendingFailed;
                                        failureEmailTracker.EmailTrackerDate = DateTime.Now;
                                        failureEmailTracker.StatusType = AHC.CD.Entities.MasterData.Enums.StatusType.Active;
                                        emailRecipient.EmailTracker.Add(failureEmailTracker);
                                    }
                                }
                                emailInfoRepository.Update(email);
                                emailInfoRepository.Save();
                            }
                            catch (System.Net.Mail.SmtpException ex)
                            {
                                throw ex;
                            }
                            finally
                            {
                                mailMessage.Dispose();
                            }                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {                
                throw ex;
            }
            return true;
        }
    }
}
