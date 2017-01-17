using PortalTemplate.Areas.UM.Models.ViewModels.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class MemberViewModel
    {
        public string Name { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Display(Name = "MEMBER ID")]
        public string MemberID { get; set; }

        [Display(Name="Date of Birth")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Age: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? Age { get; set; }

        public string Gender { get; set; }

        public string SubscriberID { get; set; }

        //[Display(Name = "PCP")]
        //public DateTime? PCPName { get; set; }

        //[Display(Name = "PCP PH")]
        //public DateTime? PCPPhoneNumber { get; set; }

        //[Display(Name = "PCP FAX")]
        //public DateTime? PCPFaxNumber { get; set; }

        public MemberPCPViewModel PCP { get; set; }

        public MembershipViewModel MemberMembership { get; set; }

        public MemberContactViewModel MemberContact { get; set; }
    }
}