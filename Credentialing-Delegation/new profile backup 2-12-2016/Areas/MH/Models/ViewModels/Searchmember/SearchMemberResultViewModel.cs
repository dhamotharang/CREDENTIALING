using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.SearchMember
{
    public class SearchMemberResultViewModel
    {
        [DisplayFormat(NullDisplayText = "-")]
        public string SubscriberID { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string MiddleInitail { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string DOB { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string Gender { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string PCP { get; set; }

        [DisplayFormat(NullDisplayText = "-")]
        public string Status { get; set; }
    }
}