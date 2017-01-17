using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class BankDetailViewModel
    {
        public int BankAccountId { get; set; }

        public string Group { get; set; }

        public string Certificate { get; set; }

        public string AccountNumber { get; set; }

        public string Deduction { get; set; }

        public string OrganizationCode { get; set; }

        public string OrganizationName { get; set; }

        public string Transit { get; set; }

        public string Signature { get; set; }

        public string SignedDate { get; set; }

        public string Status { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }

        public string AccountTypeCode { get; set; }

        public string AccountTypeName { get; set; }

    }
}