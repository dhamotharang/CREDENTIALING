using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.EmailService
{
    public class EmailAttachmentViewModel
    {
        public string AttachmentRelativePath { get; set; }

        public string AttachmentServerPath { get; set; }

        public string StatusType { get;  set; }
    }
}