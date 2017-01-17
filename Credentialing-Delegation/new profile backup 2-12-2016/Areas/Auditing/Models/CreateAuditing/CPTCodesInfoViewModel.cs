using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.CreateAuditing
{
    public class CPTCodesInfoViewModel
    {
        public CPTCodesInfoViewModel()
        {
            this.CPTCodes = new List<CPTCodeDetailsViewModel>();
        }
       
        public List<CPTCodeDetailsViewModel> CPTCodes { get; set; }
    }
}