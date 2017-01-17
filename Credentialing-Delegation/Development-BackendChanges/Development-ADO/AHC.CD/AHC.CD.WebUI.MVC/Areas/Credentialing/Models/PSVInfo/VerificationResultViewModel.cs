using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.PSVInfo
{
    public class VerificationResultViewModel
    {
        public int VerificationResultId { get; set; }

        public string Remark { get; set; }

        public string Source { get; set; }

        public VerificationResultStatusType? VerificationResultStatusType { get; set; }

        public string VerificationDocumentPath { get; set; }

        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
        public HttpPostedFileBase VerificationResultDocument { get; set; }
          
    }
}