using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.Attachment
{
    public class AttachmentViewModel
    {
        public int? AuthorizationID { get; set; }

        public int DocumentID { get; set; }

        [Display(Name = "DOCUMENT NAME", ShortName = "TITLE")]
        public string Name { get; set; }

        public string Path { get; set; }

        [Display(Name = "LAST MODIFIED DATE")]
        public DateTime? LastModifiedDate { get; set; }

        [Display(Name = "UPLOADED DATE")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "DOCUMENT TYPE", ShortName = "ATTACHMENT TYPE")]
        public int AttachmentType { get; set; }

        [Display(Name = "ATTACH DOCUMENT")]
        public HttpPostedFileBase DocumentFile { get; set; }

        [Display(Name = "CREATED BY")]
        public string CreatedBy { get; set; }

    }
}