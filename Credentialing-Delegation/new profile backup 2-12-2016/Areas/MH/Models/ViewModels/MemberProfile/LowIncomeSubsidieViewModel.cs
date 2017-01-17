using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class LowIncomeSubsidieViewModel
    {
        public int SpecialEligibilityId { get; set; }

        public string Percentage { get; set; }

        public string CopayLevelID { get; set; }

        public string CopayLevelName { get; set; }

        public string BeneficiarySourceOfSubsidyCodes { get; set; }

        public string ContractYear { get; set; }

        public string ActivityStatus { get; set; }

        public string InstitutionalStatus { get; set; }

    }
}