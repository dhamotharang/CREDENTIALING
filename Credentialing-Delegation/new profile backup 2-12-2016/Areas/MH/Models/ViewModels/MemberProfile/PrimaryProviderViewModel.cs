using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class PrimaryProviderViewModel
    {
        public int PrimaryProviderId { get; set; }

        [Display(Name = "PCP")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PCPName { get; set; }

        public string NPI { get; set; }

        public string ProviderID { get; set; }

        public string FacilityCode { get; set; }

        public string FacilityName { get; set; }

        public string IPACode { get; set; }

        public string IPAName { get; set; }

        public string BillingProviderUuid { get; set; }

        public string EffectiveDate { get; set; }

        public string TerminationDate { get; set; }

        public string MembershipId { get; set; }

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