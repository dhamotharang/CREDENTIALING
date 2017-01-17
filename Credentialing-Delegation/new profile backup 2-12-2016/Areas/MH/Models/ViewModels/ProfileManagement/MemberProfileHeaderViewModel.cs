using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.ProfileManagement
{
    public class MemberProfileHeaderViewModel
    {
        public MemberProfileHeaderViewModel()
        {
            InsuranceCompany = "Ultimate Health Plans LLC";
            PlanName = "Ultimate Elite(HMO)";
            EffectiveDate = "15/5/2013";
        }

        [Display(Name = "Reference ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string ReferenceID { get; set; }

        [Display(Name = "Subscriber ID")]
        [DisplayFormat(NullDisplayText = "-")]
        public string SubscriberID { get; set; }

        [Display(Name = "First Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastName { get; set; }

        [Display(Name = "Gender")]
        [DisplayFormat(NullDisplayText = "-")]
        public string Gender { get; set; }

        [Display(Name = "DOB")]
        [DisplayFormat(NullDisplayText = "-")]
        public string DOB { get; set; }

        [Display(Name = "Age")]
        [DisplayFormat(NullDisplayText = "-")]
        public int Age { get; set; }

        [Display(Name = "Insurance Company")]
        [DisplayFormat(NullDisplayText = "-")]
        public string InsuranceCompany { get; set; }

        [Display(Name = "Plan Name")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PlanName { get; set; }

        [Display(Name = "Effective Date")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EffectiveDate { get; set; }

        [Display(Name = "Eligibility")]
        [DisplayFormat(NullDisplayText = "-")]
        public bool Eligibility { get; set; }

        [Display(Name = "PCP")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PCP { get; set; }

        [Display(Name = "PCPPhone")]
        [DisplayFormat(NullDisplayText = "-")]
        public string PCPPhone { get; set; }
    }
}