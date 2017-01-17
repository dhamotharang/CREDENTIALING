using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.Resources.Rules;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class WorkersCompensationInfoViewModel
    {
        public int PracticeLocationDetailID { get; set; }
        public int WorkersCompensationInformationID { get; set; }

        [Required]
        [RegularExpression(RegularExpression.FOR_NUMBERS, ErrorMessage = ValidationErrorMessage.CHARACTERS_ONLY_NUMBERS)]
        [StringLength(50, MinimumLength = 2, ErrorMessage = ValidationErrorMessage.NUMBER_LENGTH_MAX_MIN)]
        [Display(Name = "Workers Compensation Number*")]
        public string WorkersCompensationNumber { get; set; }      


        [Display(Name = "Certification Status")]
        public StatusType? StatusType { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        [Display(Name="Expiration Date*")]
        [DateEnd(DateStartProperty = "IssueDate", IsGreaterThan=true, MaxYear = "100", ErrorMessage = "Expiration Date should be greater than Issue Date.")]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        [Display(Name = "Issue Date*")]
        [DateStart(MaxPastYear = "0", MinPastYear = "-100", ErrorMessage = ValidationErrorMessage.START_DATE_RANGE)]
        ////[RegularExpression(RegularExpression.FOR_DATE_FORMAT, ErrorMessage = ValidationErrorMessage.FOR_DATE_FORMAT)]
        public DateTime IssueDate { get; set; }

    }
}