using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models.ScheduleCRUD
{
    public class EditScheduleViewModel
    {
        [DisplayName("Subscriber Id")]
        public string SubscriberID { get; set; }

        [DisplayName("Member last Name")]
        public string MemberLastName { get; set; }

        [DisplayName("Member First Name")]
        public string MemberFirstName { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("DOB")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Plan Name")]
        public string PlanName { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }


        [DisplayName("Referring Provider")]
        public string ReferringProviderName { get; set; }

        [DisplayName("Rendering Provider")]
        public string RenderingProviderName { get; set; }

        [DisplayName("Billing Provider")]
        public string BillingProviderName { get; set; }

        [DisplayName("Service Facility")]
        public string ServiceFacilityName { get; set; }


        [DisplayName("Schedule Date")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? ScheduleDate { get; set; }

        [DisplayName("Schedule Time")]
        public string ScheduleTime { get; set; }

        [DisplayName("Visit Category")]
        public string VisitCategory { get; set; }

        [DisplayName("Visit Length")]
        public string VisitLength { get; set; }

        [DisplayName("Visit Reason")]
        public string VisitReason { get; set; }
    }
}