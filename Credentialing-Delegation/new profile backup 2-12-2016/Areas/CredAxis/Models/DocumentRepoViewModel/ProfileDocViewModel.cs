using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DocumentRepoViewModel
{
    public class ProfileDocViewModel
    {
        public int ProfileId { get; set; }
        public string RecordID { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public string Category { get; set; }
        public DocumentAvailabilityStatus AvailabilityStatus { get; set; }
    }
}