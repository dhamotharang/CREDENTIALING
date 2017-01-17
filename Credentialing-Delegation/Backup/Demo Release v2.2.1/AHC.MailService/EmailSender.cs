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

        
    }
}
