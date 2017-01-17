using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.CodingList
{
    public class DeactivateEncounter
    {
        public string EncounterID { get; set; }
        [DisplayName("Subscriber Id")]
        public string MemberID { get; set; }
        [DisplayName("Member First Name")]
        public string MemberFirstName { get; set; }
        [DisplayName("Member Last Name")]
        public string MemberLastName { get; set; }
        [DisplayName("Gender")]
        public string Gender { get; set; }
        [DisplayName("DOB")]
        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime DOB { get; set; }
        [DisplayName("Address")]
        public string Address { get; set; }
        [DisplayName("Plan Name")]
        public string PlanName { get; set; }
        [DisplayName("Status")]
        public string PreviousStatus { get; set; }
        [DisplayName("Action")]
        public string CurrentStatus { get; set; }
        [DisplayName("Reason")]
        public string DeactiveReason { get; set; }
    }
}