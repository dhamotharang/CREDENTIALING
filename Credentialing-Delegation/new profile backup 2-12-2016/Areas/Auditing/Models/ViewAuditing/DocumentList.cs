using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.ViewAuditing
{
    public class DocumentList
    {
        [DisplayName("Document Name")]
        public string DocumentName { get; set; }

        [DisplayName("Category")]
        public string Category { get; set; }

        [DisplayName("Uploaded By")]
        public string UploadedBy { get; set; }

        [DisplayName("Uploaded On")]
        //[DisplayFormat( = "{0:MM/dd/yyyy hh:mm:ss}")]
        public DateTime? UploadedOn { get; set; }

        public string FilePath { get; set; }

    }
}