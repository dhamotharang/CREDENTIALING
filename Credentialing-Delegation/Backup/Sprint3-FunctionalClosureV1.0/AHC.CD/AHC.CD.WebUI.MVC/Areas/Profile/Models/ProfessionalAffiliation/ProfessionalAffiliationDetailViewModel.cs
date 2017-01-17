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

        [Required(ErrorMessage = "Please enter Organization Name")]
        [RegularExpression(@"[a-zA-Z 0-9.'-,]*$", ErrorMessage = "Please enter valid Organization Name. Only alphabets, numbers, spaces, comma and hyphen accepted")]
        [Display(Name = "Organization Name *")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        public string OrganizationName { get; set; }


        [DateStart(ErrorMessage = "Start Date should be less than current date.")]
        [Display(Name = "Start Date ")]
        public DateTime? StartDate { get; set; }

        [DateEnd(DateStartProperty = "StartDate", ErrorMessage = "End Date should be greater than Start Date!!")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Position Office Held ")]
        [StringLength(50, MinimumLength=2, ErrorMessage="{0} must be between {2} and {1} characters.")]
        [RegularExpression(@"[a-zA-Z 0-9.'-,]*$", ErrorMessage = "Please enter valid Position Held. Only alphabets, numbers, spaces, comma and hyphen accepted")]
        public string PositionOfficeHeld { get; set; }

        [Display(Name = "Member/Applicant ")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "{0} must be between {2} and {1} characters.")]
        [RegularExpression(@"[a-zA-Z 0-9.'-,]*$", ErrorMessage = "Please enter valid Member/Applicant. Only alphabets, numbers, spaces, comma and hyphen accepted")]
        public string Member { get; set; }
    }
}
