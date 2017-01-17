using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.CreateEncounter
{
    public class DocumentHistoryViewModel
    {
        public string DocumentName { get; set; }

        public string DocumentCategory { get; set; }

        public string UploadedBy { get; set; }

        public DateTime? UploadedOn { get; set; }

        public string DocumentPath { get; set; }
    }
}