using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.SearchMember
{
    public class SearchMemberFactorsViewModel
    {
        [Display(Name = "SUBSCRIBER ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SubscriberID { get; set; }

        [Display(Name = "MBR LAST NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        [Display(Name = "MBR FIRST NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "DOB")]
        [DisplayFormat(NullDisplayText = "-")]
        public string DOB { get; set; }

        [Display(Name = "GENDER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Gender { get; set; }

        [Display(Name = "PHONE NUMBER")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PhoneNumber { get; set; }

        [Display(Name = "MBR PCP")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PCPName { get; set; }

        [Display(Name = "PCP NPI")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PCPNPI { get; set; }

        [Display(Name = "MEDICARE ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MedicareID { get; set; }

        [Display(Name = "INSURANCE COMPANY")]
        [DisplayFormat(NullDisplayText = "-")]
        public string InsuranceCompany { get; set; }

        [Display(Name = "PLAN NAME")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlanName { get; set; }

        [Display(Name = "EFFECTIVE DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EffectiveDate { get; set; }
    }
}