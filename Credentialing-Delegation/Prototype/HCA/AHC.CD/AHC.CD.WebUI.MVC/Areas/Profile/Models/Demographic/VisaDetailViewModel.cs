using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class VisaDetailViewModel
    {
        public int VisaDetailID { get; set; }
        
        [Required]
        [Display(Name="Are you a citizen of US ? *")]
        public string IsResidentOfUSA { get; set; }

        public virtual VisaInfoViewModel VisaInfo { get; set; }

    }

    public class VisaInfoViewModel
    {
        public int VisaInfoID { get; set; }

        [Required]
        [Display(Name = "Are you authorized to work in US ?")]
        public string IsAuthorizedToWorkInUS { get; set; }
        
        [Display(Name="Visa Number")]
        [RegularExpression(@"[0-9]{8}$", ErrorMessage="{0} must contain 8 digits only")]
        public string VisaNumber { get; set; }

        [Display(Name = "Visa Type")]
        public string VisaType { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Select a Visa Type!!")]
        [Display(Name = "Visa Type *")]
        public virtual VisaTypes VisaTypes { get; set; }

        [Column(TypeName = "datetime2")]
        [Required]
        //[DateStart(MinPastYear="0", MaxPastYear="10", ErrorMessage="Date should be  between 10 years from now!!")]       
        [Display(Name = "Visa Expiration *")]
        public DateTime VisaExpirationDate { get; set; }

        [Required]
        [Display(Name = "Visa Status *")]
        public string VisaStatus { get; set; }

        [Required]
        [Display(Name = "Visa Sponsor *")]
        [RegularExpression(@"^[a-zA-Z0-9 ]*$", ErrorMessage = "{0} must be only alphanumeric")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Length of {0} must be between {2} and {1} characters.")]
        public string VisaSponsor { get; set; }

        //[Required]
        [Display(Name = "Select VISA Certificate")]
        public string VisaCertificatePath { get; set; }

        [Display(Name = "Certificate")]
        public HttpPostedFileBase VisaCertificateFile { get; set; }

        [Display(Name = "Green Card Number")]
        [RegularExpression(@"[a-zA-Z]{3}[0-9]{10}", ErrorMessage = "{0} must be 3 alphabets followed by 10 numerical") ]
        public string GreenCardNumber { get; set; }

        [Display(Name = "Select GreenCard Certificate")]
        public string GreenCardCertificatePath { get; set; }

        [Display(Name = "Certificate")]
        public HttpPostedFileBase GreenCardCertificateFile { get; set; }

        [Display(Name = "National Id. Number")]
        public string NationalIDNumber { get; set; }

        [Display(Name = "Select National Identification Certificate")]
        public string NationalIDCertificatePath { get; set; }

        [Display(Name = "Certificate")]
        public HttpPostedFileBase NationalIDCertificateFile { get; set; }

        [Required]
        [Display(Name = "Country of Issue *")]
        public string CountryOfIssue { get; set; }
    }

    public enum VisaTypes
    {
        B1B2,
        H1B,
        L,
        F,
        M,
        J,
        C1D,
        H3,
        I,
        P1,
        P2,
        P3,
        A,
        G,
        C,
        R1R2
    }
}
