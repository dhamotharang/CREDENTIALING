using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class PersonalIdentificationViewModel
    {
        public PersonalIdentificationViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PersonalIdentificationID { get; set; }

        [Display(Name = "Social Security Number *")]
        [Required(ErrorMessage = "Please enter the SSN Number.")]
        [RegularExpression(@"^\d{9}$", ErrorMessage = "Please enter valid SSN Number. Only 9 digit numbers accepted.")]
        [Index(IsUnique = true)]
        public string SSN { get; set; }

        [Display(Name = "SSN Document")]
        public string SSNCertificatePath { get; set; }

        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = "Please select the file of type .pdf, .jpeg, .png .jpg .bmp.")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "SSN document should be less than 10 MB in size")]
        public HttpPostedFileBase SSNCertificateFile { get; set; }

        [Display(Name = "Driver's License")]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        //[RequiredIfNotEmpty("DLCertificateFile", ErrorMessage = "Please enter the Drivers License number.")]
        [RegularExpression(@"^[a-zA-Z0-9-/]*$", ErrorMessage = "Please enter valid Drivers License Number. Only Characters, numbers, hyphens and forward slash accepted.")]
        public string DL { get; set; }

        [Display(Name = "Driver's License Document")]
        public string DLCertificatePath { get; set; }

        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = "Please select the file of type .pdf, .jpeg, .png .jpg .bmp.")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Driver's license document should be less than 10 MB in size")]
        public HttpPostedFileBase DLCertificateFile { get; set; }

        public DateTime LastModifiedDate { get; set; }
    }

    public class PersonalIdentificationHistoryViewModel
    {
        public PersonalIdentificationHistoryViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PersonalIdentificationHistoryID { get; set; }
        public string SSN { get; set; }
        public string DL { get; set; }
        public string SSNCertificatePath { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
