﻿using AHC.CD.Entities.EmailNotifications;
using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Resources.Messages;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.ValidtionAttribute;
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

        [DateEnd(DateStartProperty = "FromDate", MaxYear = "100", IsRequired = false, ErrorMessage = ValidationErrorMessage.DATE_NOT_GREATER_THAN)]
        public DateTime? ToDate { get; set; }

        public ICollection<EmailAttachmentViewModel> EmailAttachments { get; set; }

        [Required(ErrorMessage = "Please provide the subject.")]
        public string Subject { get; set; }

        //[Required(ErrorMessage = "Please Enter the Body Details.")]
        public string Body { get; set; }

        public YesNoOption? SaveAsTemplateYesNoOption { get; set; }

        [Required(ErrorMessage = "Please select whether to schedule recurrence")]
        public YesNoOption? IsRecurrenceEnabledYesNoOption { get; set; }

        [RequiredIf("RecurrenceIntervalTypeCategory", (int)Entities.MasterData.Enums.RecurrenceIntervalType.Custom, ErrorMessage = "Please provide an interval")]
        //[RequiredIfNotEmpty("RecurrenceIntervalTypeCategory", ErrorMessage = "Please provide an interval")]
        public int? IntervalFactor { get; set; }

        [RequiredIf("RecurrenceIntervalTypeCategory", (int)Entities.MasterData.Enums.RecurrenceIntervalType.Custom, ErrorMessage = "Please provide a date")]
        public DateTime? DateForCustomRecurrence { get; set; }

        [RequiredIf("IsRecurrenceEnabledYesNoOption", (int)Entities.MasterData.Enums.YesNoOption.YES, ErrorMessage = "Please select the type of interval")]
        public RecurrenceIntervalType? RecurrenceIntervalTypeCategory { get; set; }

        public StatusType? StatusType { get; set; }
    }
}