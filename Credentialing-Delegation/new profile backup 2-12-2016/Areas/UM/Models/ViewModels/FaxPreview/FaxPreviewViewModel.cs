using PortalTemplate.Areas.UM.Models.ViewModels.Attachment;
using PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview;
using PortalTemplate.Areas.UM.Models.ViewModels.Contact;
using PortalTemplate.Areas.UM.Models.ViewModels.Note;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.FaxPreview
{
    public class FaxPreviewViewModel
    {
        public AuthPreviewViewModal AuthPreview { get; set; }

        public string FinalPath { get; set; }
         
        [Display(Name = "Patient Notes")]
        public List<NoteViewModel> Notes { get; set; }

        [Display(Name = "Patient Contacts")]
        public List<AuthorizationContactViewModel> Contacts { get; set; }

        [Display(Name = "Patient Documents")]
        public List<AttachmentViewModel> Attachments { get; set; }
    }
}