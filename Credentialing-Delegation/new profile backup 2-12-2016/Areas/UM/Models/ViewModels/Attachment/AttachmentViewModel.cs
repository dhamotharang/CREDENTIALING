using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Attachment
{
    public class AttachmentViewModel
    {
        public int? AuthorizationID { get; set; }

        public int DocumentID { get; set; }
       
        [Display(Name = "DocumentName", ShortName = "Name",ResourceType = typeof(App_LocalResources.Content))]
        //[Required]
        public string Name { get; set; }

        public string Path { get; set; }

        public string URL { get; set; }

        public string MedName { get; set; }

        [Display(Name = "IncFax", ResourceType = typeof(App_LocalResources.Content))]
        public string IncludeFax { get; set; }

       

        [Display(Name = "LastModifiedDate", ResourceType = typeof(App_LocalResources.Content))]
        public DateTime? LastModifiedDate { get; set; }

        [Display(Name = "CreatedDate", ResourceType = typeof(App_LocalResources.Content))]
        public DateTime? CreatedDate { get; set; }

        #region Attachment Type

        [Display(Name = "DocumentType", ResourceType = typeof(App_LocalResources.Content))]
        public int AttachmentType { get; set; }

        [Display(Name = "DocumentType", ResourceType = typeof(App_LocalResources.Content))]
        public string AttachmentTypeName { get; set; }

        #endregion

        [Display(Name = "AttachDocument", ResourceType = typeof(App_LocalResources.Content))]
        public HttpPostedFileBase DocumentFile { get; set; }

        public string MemberID { get; set; }

        [Display(Name = "CreatedBy", ResourceType = typeof(App_LocalResources.Content))]
        public string CreatedBy { get; set; }

        [Display(Name = "ModuleName", ShortName = "Module", ResourceType = typeof(App_LocalResources.Content))]
        public string ModuleName { get; set; }

        public string Status { get; set; }

        public string DocKey { get; set; }

        public string FileName { get; set; }
    }
}