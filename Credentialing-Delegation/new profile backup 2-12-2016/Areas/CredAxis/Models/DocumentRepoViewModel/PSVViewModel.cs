using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.DocumentRepoViewModel
{
    public class PSVViewModel
    {
        public int ProfileId { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string FilePath { get; set; }
       
        public bool VerficationStatus { get; set; }
        public string VerifiedOn { get; set; }

    }
}