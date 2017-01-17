using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.Claims_Tracking
{
    public class EobReport
    {
        public int Id { get; set; }

        public string PrimaryPayerName { get; set; }

        public string SecondaryPayerName { get; set; }

        public string RenderingProviderName { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime? Dos { get; set; }

        public string PatientName { get; set; }

        public string Cpt { get; set; }

        public string Billed { get; set; }

        public string Allowed { get; set; }

        public string Adjusted { get; set; }

        public string Denied { get; set; }

        public string CoInsurence { get; set; }

        public string Amount { get; set; }

        public string Penalty { get; set; }

        public string Check { get; set; }

        public string Paid { get; set; }

        public string Reason { get; set; }

        public string ReasonDescription { get; set; }
    }
}