using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using Foolproof;
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

        [Required(ErrorMessage = "Please enter Organization Name.")]
     //   [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_NUMBER_HYPHEN, ErrorMessage = "Please enter valid Organization Name. Only alphabets, numbers, spaces, comma and hyphen accepted.")]
        [Display(Name = "Organization Name *")]
       // [StringLength(50, MinimumLength = 2, ErrorMessage = "Organization Name must be between 2 and 50 characters.")]
        public string OrganizationName { get; set; }


        [Display(Name = "Start Date ")]
        //[DateStart(MaxPastYear = "0", MinPastYear = "-100", IsRequired=false, ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        //[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End Date")]
        [DateEnd(DateStartProperty = "StartDate", IsRequired=false, IsGreaterThan=true, ErrorMessage = ValidationErrorMessage.DATE_GREATER_THAN_START_DATE)]
        //[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Position Office Held ")]
    //    [StringLength(50, MinimumLength = 2, ErrorMessage = "Position Office Held should be between 2 and 50 characters.")]
    //    [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_NUMBER_HYPHEN, ErrorMessage = "Please enter valid Position Office Held. Only alphabets, numbers, commas, hyphens and spaces accepted.")]
        public string PositionOfficeHeld { get; set; }

        [Display(Name = "Member/Applicant ")]
    //    [StringLength(30, MinimumLength = 2, ErrorMessage = "Member/Applicant should be between 2 and 30 characters.")]
    //    [RegularExpression(RegularExpression.FOR_ALPHABETS_SPACE_NUMBER_HYPHEN, ErrorMessage = "Please enter valid Member/Applicant. Only alphabets, numbers, comma, hyphen and space accepted.")]
        public string Member { get; set; }

        public StatusType? StatusType { get; set; }
    }
}
