using AHC.CD.WebUI.MVC.Models.ProviderViewModel.Shared;
using AHC.MailService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.Utility
{
    public class EmailTransformer
    {



        public static EMailModel TransformToEmailModel(EmailViewModel EmailViewModel)
        {
            EMailModel emailModel = new EMailModel();

            emailModel.To = EmailViewModel.To;

            emailModel.Subject = EmailViewModel.Subject;

            emailModel.Body = EmailViewModel.Body;

            // FROM ADDRESS HAS TO BE READ FROM CONFIGURATION FILE

            emailModel.From = EmailViewModel.From;

            emailModel.Attachments = new System.Collections.Generic.List<Tuple<Stream, string>>();

            //emailModel.Attachments.Add(new Tuple<Stream, string>(new FileStream("C:/Users/pratian/Desktop/jquery.mask.min.js", FileMode.Open), "f1.txt"));
            

            return emailModel;

        }

    }
}