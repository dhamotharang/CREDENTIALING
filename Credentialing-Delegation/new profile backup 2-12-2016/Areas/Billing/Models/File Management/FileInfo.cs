using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.File_Management
{
    public class FileInfo
    {
        public string AccountName { get; set; }
        public string UserName { get; set; }
        public string Source { get; set; }
        public string ClaimType { get; set; }
        public string ApplicationOrGroupName { get; set; }
        public string RegisterUserName { get; set; }
        public string FileKey { get; set; }
        public string FileName { get; set; }
        public DateTime UploadedDate { get; set; }

    }
}