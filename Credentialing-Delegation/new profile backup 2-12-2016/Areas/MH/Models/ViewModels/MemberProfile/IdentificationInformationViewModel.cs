using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class IdentificationInformationViewModel
    {
        public string IdentificationNumber { get; set; }

        public string IdentificationTypeCode { get; set; }

        public string IdentificationTypeName { get; set; }

        public string IssueDate { get; set; }

        public string ExpiryDate { get; set; }

        public string IssuingStateCode { get; set; }

        public string IssuingStateName { get; set; }

        public string IssuingCountryCode { get; set; }

        public string IssuingCountryName { get; set; }

        public string Status { get; set; }

        public string SourceCode { get; set; }

        public string SourceName { get; set; }

        public string TimeStamp { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }
    }
}