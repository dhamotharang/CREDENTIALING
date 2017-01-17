using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class ContactInformationViewModel
    {
        public int ContactId { get; set; }

        public string PrefixCode { get; set; }

        [Display(Name = "Phone Number")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Number { get; set; }

        public string CommunicationMedium { get; set; }

        public string ContactType { get; set; }

        public string Preference { get; set; }

        public string Status { get; set; }

        public string SourceCode { get; set; }

        public string SourceName { get; set; }

        public string TimeStamp { get; set; }

        public string CreatedDate { get; set; }

        public string CreatedByEmail { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }
    }
}