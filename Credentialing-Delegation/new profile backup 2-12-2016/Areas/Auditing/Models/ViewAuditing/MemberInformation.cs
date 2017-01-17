using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.ViewAuditing
{
    public class MemberInformation
    {
        [DisplayName("Subscriber Id")]
        public string SubscriberId { get; set; }

        [DisplayName("Member First Name Id")]
        public string MemberFirstName { get; set; }

        [DisplayName("Member Last Name")]
        public string MemberLastName { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("DOB")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? DOB { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Plan Name")]
        public string PlanName { get; set; }

    }
}