using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.CreateEncounter
{
    public class DocumentDetailsViewModel
    {
        public string DocumentName { get; set; }

        public string DocumentCategory { get; set; }

        public HttpPostedFileBase  Document { get; set; }
    }
}