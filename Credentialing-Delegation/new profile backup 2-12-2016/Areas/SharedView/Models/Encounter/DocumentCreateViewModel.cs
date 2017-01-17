using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.SharedView.Models.Encounter
{
    public class DocumentCreateViewModel
    {
        public int DocumentID { get; set; }
        public string DocumentName { get; set; }
        public HttpPostedFileBase File { get; set; }
        public string DocumentCategory { get; set; }
        public string UploadedBy { get; set; }
        public string UploadedOn { get; set; }
    }
}