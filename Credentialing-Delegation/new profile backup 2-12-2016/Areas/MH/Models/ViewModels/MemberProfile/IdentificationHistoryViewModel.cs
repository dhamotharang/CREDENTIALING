using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class IdentificationHistoryViewModel
    {
        public int IdentificationId { get; set; }

        public string IdentificationNumber { get; set; }

        public string IdentificationType { get; set; }

        public string IssueDate { get; set; }

        public string ExpiryDate { get; set; }

        public string IssuingStateCode { get; set; }

        public string IssuingStateName { get; set; }

        public string IssuingCountryCode { get; set; }

        public string IssuingCountryName { get; set; }

        public string Status { get; set; }

        public string ActiveFromDate { get; set; }

        public string ActiveTillDate { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }
    }
}