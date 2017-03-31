using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Foolproof;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.DocumentRepository
{
    public class OtherDocumentViewModel
    {

        public int OtherDocumentID { get; set; }

        [Display(Name = "Title *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
   //     [StringLength(20, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_MAX_MIN)]
        public string Title { get; set; }

        [Display(Name = "Is Private")]
        public bool IsPrivate { get; set; }

        public string ModifiedBy { get; set; }

        [Display(Name = "Supporting Document")]
        public string DocumentPath { get; set; }

        [Display(Name = "Attach Document *")]
        //[RequiredIfEmpty("DocumentPath", ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase File { get; set; }

    }
}