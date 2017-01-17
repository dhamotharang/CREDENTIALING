using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.BoardSpecialty
{
    public class SpecialtyBoardCertifiedDetailViewModel
    {
        public int SpecialtyBoardCertifiedDetailID { get; set; }

        [Required(ErrorMessage = ValidationErrorMessage.REQUIRED_SELECT)]
        [Display(Name = "Board Name *")]
        public int SpecialtyBoardID { get; set; }

        [Display(Name = "Certificate Number")]
        public string CertificateNumber { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Initial Certification Date")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = false, ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        public DateTime? InitialCertificationDate { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Last Re-Certification Date")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        [DateEnd(DateStartProperty = "SpecialtyBoardCertifiedDetail_InitialCertificationDate", MaxYear="0", ErrorMessage = "Last Re-Certification Date should not be less than Initial Certification Date.")]
        //[GreaterThanOrEqualTo("InitialCertificationDate", ErrorMessage = "Last Re-Certification Date * should not be less than Initial Certification Date.")]
        public DateTime? LastReCerificationDate { get; set; }

        //[Required(ErrorMessage = ValidationErrorMessage.REQUIRED_ENTER)]
        [Display(Name = "Expiration Date")]
        [DateEnd(DateStartProperty = "SpecialtyBoardCertifiedDetail_LastReCerificationDate", ErrorMessage = "Expiration Date should not be less than Last Re-Certification Date.")]
        //[GreaterThanOrEqualTo("LastReCerificationDate", ErrorMessage = "Expiration Date should not be less than Last Re-Certification Date.")]
        public DateTime? ExpirationDate { get; set; }

        [Display(Name = "Supporting Document")]
        public string BoardCertificatePath { get; set; }

        [Display(Name = "Board Certificate")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = "Please select the file of type pdf, jpeg, png, jpg, bmp")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = ValidationErrorMessage.UPLOAD_FILE_SIZE_ELIGIBLE)]
        public HttpPostedFileBase BoardCertificateDocumentFile { get; set; }
    }
}
