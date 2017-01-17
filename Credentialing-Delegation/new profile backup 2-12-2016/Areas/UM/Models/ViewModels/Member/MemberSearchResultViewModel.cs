using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Member
{
    public class MemberSearchResultViewModel
    {
        public string SubscriberID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public string PCPFullName { get; set; }
        public string PCPPhoneNumber { get; set; }
        public string PlanEffectiveDate { get; set; }
        public string AddressLine1 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}