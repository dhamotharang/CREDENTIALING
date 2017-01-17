using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.File_Management
{
    public class FileUpload
    {
        [DisplayName("INPUT SOURCE")]
        public string InputSource { get; set; }

        [DisplayName("FILE TYPE")]
        public string FileType { get; set; }

        [DisplayName("EDI FILE")]
        public List<HttpPostedFileBase> EdiFile { get; set; }
    }
}