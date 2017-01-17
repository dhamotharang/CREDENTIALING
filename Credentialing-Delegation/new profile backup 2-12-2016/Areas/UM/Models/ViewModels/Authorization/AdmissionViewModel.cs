using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Authorization
{
    public class AdmissionViewModel
    {
        [Display(Name = "Received DT: ", ShortName = "Req Dt: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? AdmissionRequestedDate { get; set; }

        [Display(Name = "From Date: ", ShortName = "From DT: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? AdmissionFromDate { get; set; }

        [Display(Name = "Approved Days: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public int? AdmissionApprovedDays { get; set; }
    }
}