using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.AuthPreview
{
    public class DischargePreviewViewModel
    {
        [Display(Name = "Expected DC DT: ", ShortName = "EXP DC DT: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? ExpectedDischargeDate { get; set; }

        [Display(Name = "TO DATE: ", ShortName = "TO DT: ")]
        [DisplayFormat(NullDisplayText = "-")]
        public DateTime? DischargeToDate { get; set; }

        [Display(Name = "Custodial Dt: ")]
        public DateTime? CustodialDate { get; set; }
    }
}