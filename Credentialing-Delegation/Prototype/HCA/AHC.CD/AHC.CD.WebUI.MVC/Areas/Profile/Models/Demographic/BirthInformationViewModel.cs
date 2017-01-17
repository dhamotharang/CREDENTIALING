using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class BirthInformationViewModel
    {
        public int BirthInformationID { get; set; }

        [Required]  
        [Display(Name="City of Birth *")]
        public string CityOfBirth { get; set; }

        [Required]
        [Display(Name = "Country of Birth *")]
        public string CountryOfBirth { get; set; }

        [Required]
        [Display(Name = "County of Birth *")]
        public string CountyOfBirth { get; set; }

        [Required]
        [Display(Name = "Date of Birth *")]
        [Column(TypeName="datetime2")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [Display(Name = "State of Birth *")]
        public string StateOfBirth { get; set; }

              
        [Column(TypeName = "datetime2")]
        public virtual DateTime LastUpdatedDateTime{get;set;}

       
        [Display(Name = "Certificate")]
        public string BirthCertificatePath { get; set; }

        public HttpPostedFileBase BirthCertificateFile { get; set; }
    }
}
