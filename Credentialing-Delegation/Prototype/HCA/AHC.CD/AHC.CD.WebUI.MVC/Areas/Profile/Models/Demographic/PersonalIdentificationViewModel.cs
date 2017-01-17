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
        public int PersonalIdentificationID { get; set; }

        [Display(Name = "Social Security Number")]
        [Required]
        [StringLength(9, MinimumLength = 9, ErrorMessage = "{0} must be {1} digit.")]
        [RegularExpression(@"^[\d.]+$", ErrorMessage = "Only Integer Accepted!!")]
        [Index(IsUnique = true)]
        public string SSN { get; set; }

        [Display(Name = "Drivers License")]
        [Required]
        [MaxLength(100)]
        [Index(IsUnique = true)]
        public string DL { get; set; }

        public string SSNCertificatePath { get; set; }

        public HttpPostedFileBase SSNCertificateFile { get; set; }

        public string DLCertificatePath { get; set; }

        public HttpPostedFileBase DLCertificateFile { get; set; }
    }

    public class PersonalIdentificationHistoryViewModel
    {
        public int PersonalIdentificationHistoryID { get; set; }
        public string SSN { get; set; }
        public string DL { get; set; }
        public string SSNCertificatePath { get; set; }
    }
}
