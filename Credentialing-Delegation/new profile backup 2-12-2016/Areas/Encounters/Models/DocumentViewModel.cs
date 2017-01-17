using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models
{
    public class DocumentViewModel
    {
        public int DocumentID { get; set; }
        public string DocumentName { get; set; }
        public string Category { get; set; }
        public string UploadedBy { get; set; }
        public string UploadedOn { get; set; }
    }
}