using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.ProviderProfile
{
    public class ContractInformationViewModel
    {
        [Display(Name = "CONTRACT DOCUMENT")]
        public string DcoumentPath { get; set; }
    }
}