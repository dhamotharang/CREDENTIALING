using PortalTemplate.Areas.UM.Models.ViewModels.Attachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.IServices
{
    public interface IAttachmentServices
    {
        AttachmentViewModel ViewAttachment(AttachmentViewModel model);
        AttachmentViewModel AddAttachment(AttachmentViewModel model);
        AttachmentViewModel DeleteAttachment(AttachmentViewModel model);
        List<AttachmentViewModel> GetAllAttachments(string SubscriberID);
    }
}