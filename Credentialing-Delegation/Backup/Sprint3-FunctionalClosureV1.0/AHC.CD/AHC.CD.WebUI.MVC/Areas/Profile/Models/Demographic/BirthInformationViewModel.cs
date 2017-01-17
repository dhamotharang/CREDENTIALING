using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
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
        public BirthInformationViewModel()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int BirthInformationID { get; set; }

        [Display(Name = "City of Birth *")]
        [Required(ErrorMessage = "Please enter the City.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "City must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-.]*$", ErrorMessage = "Please enter valid City. Only alphabets, special characters and numbers accepted.")]
        public string CityOfBirth { get; set; }

        [Display(Name = "Country of Birth *")]
        [Required(ErrorMessage = "Please enter the Country.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Country must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-.]*$", ErrorMessage = "Please enter valid Country. Only alphabets, special characters and numbers accepted.")]
        public string CountryOfBirth { get; set; }

        [Display(Name = "County of Birth")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "County must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-.]*$", ErrorMessage = "Please enter valid County. Only alphabets, special characters and numbers accepted.")]
        public string CountyOfBirth { get; set; }

        [Display(Name = "Date of Birth *")]
        [Required(ErrorMessage = "Please enter the Date of Birth.")]
        [DateStart(MaxPastYear = "-18", MinPastYear = "-100", ErrorMessage = "Provider should be above 18 years of age.")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "State of Birth *")]
        [Required(ErrorMessage = "Please enter the State.")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "State must be between {2} and {1} characters.")]
        [RegularExpression(@"^[a-zA-Z ,-.]*$", ErrorMessage = "Please enter valid State. Only alphabets, special characters and numbers accepted.")]
        public string StateOfBirth { get; set; }
      
        public virtual DateTime LastUpdatedDateTime{get;set;}

        [Display(Name = "Birth Certificate")]
        public string BirthCertificatePath { get; set; }

        [Display(Name = "Birth Certificate")]
        [PostedFileExtension(AllowedFileExtensions = "pdf,jpeg,jpg,png,bmp", ErrorMessage = "Please select the file of type .pdf, .jpeg, .png .jpg .bmp.")]
        [PostedFileSize(AllowedSize = 10485760, ErrorMessage = "Birth Certificate document should be less than 10mb in size")]
        public HttpPostedFileBase BirthCertificateFile { get; set; }

        public DateTime LastModifiedDate { get; set; }  
    }
}
