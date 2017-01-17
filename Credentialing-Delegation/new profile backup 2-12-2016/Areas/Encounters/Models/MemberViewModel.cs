using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Encounters.Models
{
    public class MemberViewModel
    {
        public int MemberId { get; set; }

        public int ProviderId { get; set; }

        [DisplayName("Subscriber Id")]
        public string SubscriberID { get; set; }

        [DisplayName("Member last Name")]
        public string MemberLastName { get; set; }

        [DisplayName("Member Middle Name")]
        public string MemberMiddleName { get; set; }

        [DisplayName("Member First Name")]
        public string MemberFirstName { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        [DisplayName("DOB")]
        public DateTime? DateOfBirth { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        public string PatientCity { get; set; }

        public string PatientState { get; set; }

        public string PatientZip { get; set; }

        public string PCP { get; set; }

        public string PayerName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? EndDate { get; set; }

        public PlanViewModel Plan { get; set; }
    }
}