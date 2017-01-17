using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.CreateCoding
{
    public class CPTCodeDetailsViewModel
    {
        public CPTCodeDetailsViewModel()
        {
            this.CPTCodes = new List<ICDCPTMapping.ICDCPTCodemappingViewModel>();
        }
        public string CPTType { get; set; }
        public List<ICDCPTMapping.ICDCPTCodemappingViewModel> CPTCodes { get; set; }
    }
}