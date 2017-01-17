using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.ViewModels.Letter
{
    public class NonCoverageDetailViewModel
    {
        public int NonCoverageDetailID { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Service { get; set; }

        public string DetailedExplanation { get; set; }

        public string FactsUsed { get; set; }

        public string PlanPolicy { get; set; }

        public int? LengthOfStayID { get; set; }
    }
}