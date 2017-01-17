using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.CredAxisProduct.Models.ProviderProfileViewModel.WorkHistoryViewModel
{
    public class PublicHealthServicesViewModel
    {
        [Key]
        public int? ID { get; set; }

       
        [Display(Name = "START DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string StartDate { get; set; }


        [Display(Name = "END DATE")]
        [DisplayFormat(NullDisplayText = "-")]
        public string EndDate { get; set; }

        [Display(Name = "LAST LOCATION")]
        [DisplayFormat(NullDisplayText = "-")]
        public string LastLocation { get; set; }
    }
}