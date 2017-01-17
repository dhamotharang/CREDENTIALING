using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.ProviderViewModel.Shared
{
    public class EmailViewModel
    {
       public EmailViewModel()
        {

            Attachments = new System.Collections.Generic.List<string>();
        }

        public string To { get; set; }
        public string From { get; set; }
        public string Cc { get; set; }
        public string Bcc { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<string> Attachments { get; set; }

    }
}