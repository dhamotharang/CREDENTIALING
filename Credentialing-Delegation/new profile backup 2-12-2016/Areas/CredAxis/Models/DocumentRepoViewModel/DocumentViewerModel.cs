using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DocumentRepoViewModel
{
    public class DocumentViewerModel
    {
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string UploadedBy { get; set; }
        public DateTime UploadedOn { get; set; }
        public DocumentAvailabilityStatus AvailabilityStatus { get; set; }
        public List<DocumentCommentViewModel> Comments { get; set; }
    }
}