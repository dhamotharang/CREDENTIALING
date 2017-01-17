using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class VisaInfoViewModel
    {
        public VisaInfoViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int VisaInfoID { get; set; }

        [Required(ErrorMessage = "Please enter the Visa Number.")]
        [Display(Name = "Visa Number *")]
        [RegularExpression(@"[0-9]{8}$", ErrorMessage = "Please enter a valid Visa Number.Only 8 digit numbers accepted.")]
        public string VisaNumber { get; set; }

        [Required(ErrorMessage = "Please select the Visa Type.")]
        [Display(Name = "Visa Status *")]
        public int VisaStatusID { get; set; }

        [Display(Name = "Visa Status *")]
        public VisaStatus VisaStatus { get; set; }

        [Required(ErrorMessage = "Please select the Visa status.")]
        [Display(Name = "Visa Type *")]
        public int VisaTypeID { get; set; }

        [Display(Name = "Visa Type")]
        public VisaType VisaType { get; set; }

        [Required(ErrorMessage = "Please enter visa Sponsor.")]
        [Display(Name = "Visa Sponsor *")]
        [RegularExpression(@"^[a-zA-Z0-9 ,-.()]*$", ErrorMessage = "Please enter a valid Visa Sponsor.Only alphabets, numbers and hyphen accepted.")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Length of Visa Sponsor must be between {2} and {1} characters.")]
        public string VisaSponsor { get; set; }

        [Required]
        [DateStart(MinPastYear="0", MaxPastYear="10", ErrorMessage="Date should be between 10 years from current date.")]       
        [Display(Name = "Visa Expiration *")]
        public DateTime VisaExpirationDate { get; set; }

        //[Required]
        [Display(Name = "Select VISA Document")]
        public string VisaCertificatePath { get; set; }

        [Required]
        [Display(Name = "Supporting VISA Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = "Please select the file of type .pdf, .jpeg, .png .jpg .bmp.")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Visa document should be less than 10 MB in size")]
        public HttpPostedFileBase VisaCertificateFile { get; set; }

        //[Required]
        [Display(Name = "Green Card Number")]
        [RegularExpression(@"[a-zA-Z]{3}[0-9]{10}", ErrorMessage = "{0} must be 3 alphabets followed by 10 numerical.")]
        public string GreenCardNumber { get; set; }

        [Display(Name = "Select GreenCard Document")]
        public string GreenCardCertificatePath { get; set; }

        [Display(Name = "Supporting Green Card Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = "Please select the file of type .pdf, .jpeg, .png .jpg .bmp.")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Green card document should be less than 10 MB in size.")]
        public HttpPostedFileBase GreenCardCertificateFile { get; set; }

        [Display(Name = "National Id. Number")]
        public string NationalIDNumber { get; set; }

        [Display(Name = "Select National Identification Document")]
        public string NationalIDCertificatePath { get; set; }

        [RequiredIfNotEmpty("NationalIDNumber", ErrorMessage = "Please select the Country which has issued National Identification Number.")]
        [Display(Name = "Country of Issue")]
        public string CountryOfIssue { get; set; }

        [Display(Name = "Supporting National Identification Document")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = "Please select the file of type .pdf, .jpeg, .png .jpg .bmp.")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "NIN document should be less than 10 MB in size.")]
        public HttpPostedFileBase NationalIDCertificateFile { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }
}