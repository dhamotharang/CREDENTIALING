using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.EducationHistory
{
    public class FifthPathwayDetailViewModel
    {
        public int FifthPathwayDetailID { get; set; }
        [Display(Name = "Hospital/Institution")]
        public string Institution { get; set; }
        [Display(Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Required]
        [Display(Name = "County")]
        public string County { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        [Required]
        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }
        [Required]
        [Display(Name = "Telephone Number")]
        public string Telephone { get; set; }
        [Display(Name = "Fax")]
        public string Fax { get; set; }
        [Display(Name = "Did you complete your education at this school?")]
        public bool CompletedEducationAtThisSchool { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "datetime2")]
        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        
    }
}
