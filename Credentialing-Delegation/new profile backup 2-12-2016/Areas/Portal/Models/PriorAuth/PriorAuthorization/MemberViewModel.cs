using PortalTemplate.Areas.UM.Models.ViewModels.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PortalTemplate.Areas.Portal.Models.PriorAuth.PriorAuthorization
{
    public class MemberViewModel
    {
        public string Name { get; set; }

        [Display(Name = "FIRST NAME")]
        public string FirstName { get; set; }

        [Display(Name = "LAST NAME")]
        public string LastName { get; set; }

        [Display(Name = "MIDDLE NAME")]
        public string MiddleName { get; set; }

        [Display(Name = "FULL NAME")]
        public string FullName { get; set; }

        [Display(Name = "MEMBER ID")]
        public int MemberID { get; set; }

        [Display(Name="DATE OF BIRTH", ShortName="DOB")]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "GENDER")]
        public string Gender { get; set; }

        public string SubscriberID { get; set; }

        public MembershipViewModel MemberMembership { get; set; }

        public MemberContactViewModel MemberContact { get; set; }
    }
}