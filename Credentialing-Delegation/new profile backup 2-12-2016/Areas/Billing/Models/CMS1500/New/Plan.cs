using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.New
{
    public class Plan
    {
        [Display(Name = "Payer Name")]
        public string PlanName { get; set; }

        [Display(Name = "Payer ID")]
        public string PlanUniqueId { get; set; }

        public Address Address { get; set; }
    }
}