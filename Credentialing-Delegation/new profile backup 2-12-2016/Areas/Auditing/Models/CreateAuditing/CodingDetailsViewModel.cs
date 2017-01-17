using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.CreateAuditing
{
    public class CodingDetailsViewModel
    {
        public CodingDetailsViewModel()
        {
            this.ICDCodeDetails = new ICDCodesInfoViewModel();
            this.CPTCodeDetails = new CPTCodesInfoViewModel();
        }

        public ICDCodesInfoViewModel ICDCodeDetails { get; set; }
        public CPTCodesInfoViewModel CPTCodeDetails { get; set; }
    }
}