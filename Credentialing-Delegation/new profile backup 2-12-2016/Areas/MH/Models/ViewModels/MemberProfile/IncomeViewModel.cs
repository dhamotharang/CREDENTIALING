using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.MH.Models.ViewModels.MemberProfile
{
    public class IncomeViewModel
    {
        public int IncomeId { get; set; }

        public string IncomeSourceCode { get; set; }

        public string IncomeSourceName { get; set; }

        public string IncomeAmount { get; set; }

        public string IncomeFrequency { get; set; }

        public string Status { get; set; }

        public string SourceCode { get; set; }

        public string SourceName { get; set; }

        public string TimeStamp { get; set; }

        public string CreatedByEmail { get; set; }

        public string CreatedDate { get; set; }

        public string LastModifiedByEmail { get; set; }

        public string LastModifiedDate { get; set; }


    }
}