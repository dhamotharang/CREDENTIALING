using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ProfessionalAffiliation
{
    public class ProfessionalAffiliationDetailViewModel
    {
        public int ProfessionalAffiliationInfoID { get; set; }

        [Required]
        [RegularExpression(@"[a-zA-Z0-9 .'-]*$", ErrorMessage = "Only alpha-numeric and (-'.) characters can be used.")]
        [Display(Name = "Organization Name *")]
        public string OrganizationName { get; set; }

        [Required]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = "Start Date should fall between 100 years from now!!")]
        [Display(Name = "Start Date *")]
        public DateTime StartDate { get; set; }

        [DateEnd(DateStartProperty = "StartDate", ErrorMessage = "Should be greater than Start Date!!")]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        [Required]
        [Display(Name = "Position Office Held *")]
        [StringLength(50, MinimumLength=2, ErrorMessage="{0} must be between {2} and {1} characters.")]
        public string PositionOfficeHeld { get; set; }

        [Required]
        [Display(Name = "Member/Applicant *")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string Member { get; set; }
    }
}
