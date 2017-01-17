using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AHC.MailService;
using System.IO;

namespace AHC.CD.Business.Tests
{
    [TestClass]
    public class EmailSenderUnitTest
    {
        [TestMethod]
        public void SendMailSuccessTest()
        {
            IEmailSender emailSender = new EmailSender();
            EMailModel model = new EMailModel();
            model.From = "venkat@pratian.com";
            model.To = "venkat@pratian.com";
            model.Subject = "test subject";
            model.Body = "test body";
            
            bool actual = emailSender.SendMail(model);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        public void SendMailSuccessWithAttachmentTest()
        {
            IEmailSender emailSender = new EmailSender();
            EMailModel model = new EMailModel();
            model.From = "venkat@pratian.com";
            model.To = "venkatshiva.reddy@gmail.com";
            model.Subject = "test subject";
            model.Body = "test body";
            model.Attachments = new System.Collections.Generic.List<Tuple<Stream, string>>();
            model.Attachments.Add(new Tuple<Stream, string>(new FileStream(@"d:\f1.txt", FileMode.Open), "f1.txt"));
            model.Attachments.Add(new Tuple<Stream, string>(new FileStream(@"d:\f2.txt", FileMode.Open), "f2.txt"));
            bool actual = emailSender.SendMail(model);
            Assert.AreEqual(true, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException))]
        public void SendMailSuccessWithAttachmentNotFoundTest()
        {
            IEmailSender emailSender = new EmailSender();
            EMailModel model = new EMailModel();
            model.From = "venkat@pratian.com";
            model.To = "venkatshiva.reddy@gmail.com";
            model.Subject = "test subject";
            model.Body = "test body";
            model.Attachments = new System.Collections.Generic.List<Tuple<Stream, string>>();
            model.Attachments.Add(new Tuple<Stream, string>(new FileStream(@"d:\f.txt", FileMode.Open), "f1.txt"));
            //model.Attachments.Add(@"D:\f2.txt");
            bool actual = emailSender.SendMail(model);
            Assert.AreEqual(true, actual);
        }
    }
}
