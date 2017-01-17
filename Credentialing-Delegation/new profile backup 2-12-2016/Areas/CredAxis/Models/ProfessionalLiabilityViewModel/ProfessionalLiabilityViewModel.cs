using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxis.Models.ProfessionalLiabilityViewModel
{
    public class ProfessionalLiabilityViewModel
    {
        public string ProfessionalLiabilityID { get; set; }

        [Display(Name = "INSURANCE CARRIER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string InsuranceCarrier { get; set; }


        [Display(Name = "INSURANCE CARRIER ADDRESS")]
        [DisplayFormat(NullDisplayText = "-")]
        public string InsuranceAddress { get; set; }

        [Display(Name = "SUITE/BUILDING")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Suite { get; set; }

        [Display(Name = "STREET/P. O. BOX NO")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StreetNo { get; set; }

        [Display(Name = "CITY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string City { get; set; }

        [Display(Name = "STATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string State { get; set; }

        [Display(Name = "COUNTY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string County { get; set; }

        [Display(Name = "COUNTRY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Country { get; set; }

        [Display(Name = "ZIP CODE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ZipCode { get; set; }

        [Display(Name = "SELF INSURED?")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SelfInsured { get; set; }

        [Display(Name = "UNLIMITED COVERAGE WITH INSURANCE CARRIER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string InsuranceCarrierCoverage { get; set; }

        [Display(Name = "TYPE OF COVERAGE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string TypeofCoverage { get; set; }

        [Display(Name = "ORIGINAL EFFECTIVE DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string OrginialDate { get; set; }

        [Display(Name = "EFFECTIVE DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EffectiveDate { get; set; }

        [Display(Name = "EXPIRATION DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ExpirationDate { get; set; }

        [Display(Name = "AMOUNT OF COVERAGE PER OCCURRENCE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AmountCoverage { get; set; }

        [Display(Name = "POLICY INCLUDES TAIL COVERAGE ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PolicyTailCoverage { get; set; }

        [Display(Name = "POLICY NUMBER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PolicyNumber { get; set; }

        [Display(Name = "PHONE NUMBER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhoneNumber { get; set; }

        [Display(Name = "AMOUNT OF COVERAGE AGGREGATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string AmountcoverageAggregate { get; set; }

        [Display(Name = "DOCUMENT")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Document { get; set; }

    }
}