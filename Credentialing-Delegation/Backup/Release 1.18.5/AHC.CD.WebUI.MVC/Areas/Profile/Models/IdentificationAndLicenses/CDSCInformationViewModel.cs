using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using AHC.CD.Resources.Rules;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.IdentificationAndLicenses
{
    public class CDSCInformationViewModel
    {
      
        public int CDSCInformationID { get; set; }


        [Display(Name = "CDS Number *")]
        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        //[StringLength(6, MinimumLength = 6, ErrorMessage = ValidationErrorMessage.STRING_LENGTH_FIXED)]
        //[RegularExpression(@"[a-zA-Z]{1}[0-9]{5}$", ErrorMessage = "CDS number should have 1 alphabet and 5 digits.")]
        [RegularExpression(RegularExpression.ALPHA_NUMERIC, ErrorMessage = ValidationErrorMessage.ALPHA_NUMERIC)]
        public string CertNumber { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        [Display(Name = "Issue State *")]
        public string State { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [DateStart(IsRequired = false, MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        [Display(Name = "Issue Date")]
        public DateTime? IssueDate { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
      //  [GreaterThan("IssueDate", ErrorMessage = ValidationErrorMessage.DATE_NOT_LESS_THAN)]
        [DateEnd(DateStartProperty = "IssueDate", IsRequired = false, ErrorMessage = "Expiry Date should not be less than Issue Date.")]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Document Preview")]
        public string CDSCCerificatePath { get; set; }
       // [RequiredIfEmpty("CDSCCerificatePath",ErrorMessage = "Upload a supporting Document")]

        [Display(Name = "Document Preview")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp,PNG,JPEG,PDF,JPG,BMP", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp")]
//        [PostedFileExtension(AllowedFileExtensions = "pdf,doc,jpg,docx,jpeg,png,bmp", ErrorMessage = "Please select the file of type .pdf, jpeg, .png, .jpg, .bmp, .doc, .docx")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "CDS Document should be less than 10mb in size.")]
        public HttpPostedFileBase CDSCCerificateFile { get; set; }
    

    }
}
