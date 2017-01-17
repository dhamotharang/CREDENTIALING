using AHC.CD.Entities.MasterData.Tables;
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

        [Display(Name = "Certificate Number")]
        public string CertificateNumber { get; set; }

        [Required(ErrorMessage = "Please enter the Initial Certification Date.")]
        [Display(Name = "Initial Certification Date *")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired = true, ErrorMessage = "Initial Certification Date should not be greater than current date.")]
        public DateTime InitialCertificationDate { get; set; }

        [Required(ErrorMessage = "Please enter the Last Re-Certification Date.")]
        [Display(Name = "Last Re-Certification Date *")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "Last Re-Certification Date should not be greater than current date.")]
        [GreaterThanOrEqualTo("InitialCertificationDate", ErrorMessage = "Last Re-Certification Date should not be less than Initial Certification Date.")]
        //[LessThan("ExpirationDate", ErrorMessage = "Last Re-Certification Date should not be greater than Expiration Date.")]
        public DateTime LastReCerificationDate { get; set; }

        [Required(ErrorMessage = "Please enter the Expiration Date.")]
        [Display(Name = "Expiration Date *")]
        //[DateStart(MaxPastYear = "100", MinPastYear = "0", IsRequired = true, ErrorMessage = "Expiration Date should be from now till 100 years!!")]
        [GreaterThanOrEqualTo("LastReCerificationDate", ErrorMessage = "Expiration Date should not be less than Last Re-Certification Date.")]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Supporting Document")]
        public string BoardCertificatePath { get; set; }

        //[RequiredIfEmpty("BoardCertificatePath", ErrorMessage = "Board Certificate Document is required!!")]
        [Display(Name = "Board Certificate")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,png,jpg,bmp", ErrorMessage = "Please select the file of type .pdf, .jpeg, .jpg, .png, .bmp.")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Board Certificate should be less than 10MB in size.")]
        public HttpPostedFileBase BoardCertificateDocumentFile { get; set; }
    }
}
