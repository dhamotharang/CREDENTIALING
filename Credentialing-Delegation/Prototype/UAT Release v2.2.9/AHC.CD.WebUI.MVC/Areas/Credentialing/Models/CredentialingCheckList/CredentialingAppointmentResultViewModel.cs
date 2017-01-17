using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList
{
    public class CredentialingAppointmentResultViewModel
    {
        public int CredentialingAppointmentResultID { get; set; }

        public string SignaturePath { get; set; }

        public DateTime? SignedDate { get; set; }

        public int? SignedByID { get; set; }

        [Required(ErrorMessage = "Please select Digital Signature.")]
        [PostedFileExtension(AllowedFileExtensions = "jpeg,jpg,png,bmp,PNG,JPEG,JPG,BMP", ErrorMessage = "Please select the file of type jpeg, .png, .jpg, .bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Digital Signature should be less than 10 MB of size.")]
        public HttpPostedFileBase SignatureFile { get; set; }
        
        [Required(ErrorMessage = "Please select Status.")]
        [Range(1, int.MaxValue, ErrorMessage = "Select a correct license")]
        public CCMApprovalStatusType? ApprovalStatusType { get; set; }
        
        public string RemarkForApprovalStatus { get; set; }

        public CredentialingLevel? CredentialingLevel { get; set; }
       

    }
}
