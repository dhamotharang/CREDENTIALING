using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxisProduct.Models.ProviderProfileViewModel.WorkHistoryViewModel
{
    public class MilitaryServiceViewModel
    {
        [Key]
        public int? ID { get; set; }

        [Display(Name = "MILITARY BRANCH ")]
        [DisplayFormat(NullDisplayText = "-")]
        public string MilitaryBranch { get; set; }


        [Display(Name = "START DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StartDate { get; set; }

        [Display(Name = "END DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EndDate { get; set; }

        [Display(Name = "RANK AT DISCHARGE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string RankAtDischarge { get; set; }

        [Display(Name = "DISCHARGE TYPE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string DischargeType { get; set; }
       
        [Display(Name = "PRESENT DUTY STATUS/ASSIGNMENT")]
        [DisplayFormat(NullDisplayText = "-")]
        public string DutyStatus { get; set; }
    }
}