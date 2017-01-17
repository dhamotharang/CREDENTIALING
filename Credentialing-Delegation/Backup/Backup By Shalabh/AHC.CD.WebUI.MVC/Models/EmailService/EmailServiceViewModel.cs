using AHC.CD.Entities.MasterData.Enums;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.EmailService
{
    public class EmailServiceViewModel
    {
        public int? EmailServiceID { get; set; }

        [Required(ErrorMessage = "Please provide an email address.")]
        public string To { get; set; }

        public string CC { get; set; }

        public string BCC { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        [Required(ErrorMessage = "Please provide the subject.")]
        public string Subject { get; set; }

        public string Body { get; set; }

        public YesNoOption? SaveAsTemplateYesNoOption { get; set; }

        [Required(ErrorMessage = "Please select whether to schedule recurrence")]
        public YesNoOption? IsRecurrenceEnabledYesNoOption { get; set; }

        [RequiredIf("RecurrenceIntervalTypeCategory", (int)Entities.MasterData.Enums.RecurrenceIntervalType.Custom, ErrorMessage = "Please provide an interval")]
        [RequiredIfNotEmpty("RecurrenceIntervalTypeCategory", ErrorMessage = "Please provide an interval")]
        public int? IntervalFactor { get; set; }

        [RequiredIf("RecurrenceIntervalTypeCategory", (int)Entities.MasterData.Enums.RecurrenceIntervalType.Custom, ErrorMessage = "Please provide a date")]
        public DateTime? DateForCustomRecurrence { get; set; }

        [RequiredIf("IsRecurrenceEnabledYesNoOption", (int)Entities.MasterData.Enums.YesNoOption.YES, ErrorMessage = "Please select the type of interval")]
        public RecurrenceIntervalType? RecurrenceIntervalTypeCategory { get; set; }

        public StatusType? StatusType { get; set; }
    }
}