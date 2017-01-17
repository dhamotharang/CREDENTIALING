using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.EmailTemplate
{
    public class EmailTemplateViewModel
    {
        public int EmailTemplateID { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Action { get; set; }

        public string To { get; set; }

        public string CC { get; set; }

        public string BCC { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public EmailNotificationType? EmailNotificationTypeCategory { get; set; }

        public YesNoOption? IsRecurrenceEnabledYesNoOption { get; set; }

        public int? IntervalFactor { get; set; }

        public RecurrenceIntervalType? RecurrenceIntervalTypeCategory { get; set; }

        public StatusType? StatusType { get; set; }
    }
}