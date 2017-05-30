using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CCMPortal
{
    public class CCMQuickActionViewModal
    {
        public CCMQuickActionViewModal()
        {
            QuickActionSet = new List<QuickActionIdSet>();
        }
        public List<QuickActionIdSet> QuickActionSet { get; set; }
     
        public string SignaturePath { get; set; }

        [Required(ErrorMessage = "Please select Digital Signature.")]
        [PostedFileExtension(AllowedFileExtensions = "jpeg,jpg,png,bmp,PNG,JPEG,JPG,BMP,gif,GIF,PDF,pdf,DOC,doc,DOCX,docx,XLSX,xlsx", ErrorMessage = "Please select the file of type jpeg, .png, .jpg, .bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Digital Signature should be less than 10 MB of size.")]
        public HttpPostedFileBase SignatureFile { get; set; }

        public DateTime? SignedDate { get; set; }

        public string SignedByID { get; set; }

        public string RemarksForAppointments { get; set; }

        public CCMApprovalStatusType AppointmentsStatus { get; set; }

    }
}