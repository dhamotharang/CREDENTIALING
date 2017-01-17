using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Subscriber
    {
        public long Subscriber_PK_Id { get; set; }

        [Display(Name = "Insured's I.D. Number")]
        public string SubscriptionID { get; set; }
        public string SubscriberCode { get; set; }

        [Display(Name = "First")]
        public string FirstName { get; set; }

        [Display(Name = "Last")]
        public string LastName { get; set; }

        [Display(Name = "MI")]
        public string MiddleName { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<long> ContactInfo_FK_Id { get; set; }
        public Nullable<long> PCP_FK_Id { get; set; }
        public Plan Plan { get; set; }

        public Nullable<long> Member_FK_Id { get; set; }
        public string SubscriberPriority { get; set; }
        public virtual Member Member { get; set; }
        public virtual Provider Provider { get; set; }
        public ContactInfo ContactInfo { get; set; }

        [Display(Name = "Other Insured's Policy or Group Number")]
        public string PolicyNumber { get; set; }

         [Display(Name = "Sex")]
        public string Gender { get; set; }
    }
}