using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.CreateCoding
{
    public class CPTCodeHistoryDetailsViewModel
    {
        public CPTCodeHistoryDetailsViewModel()
        {
            this.CPTCodesHistory = new List<ICDCPTMapping.ICDCPTCodemappingViewModel>();
        }
        public string CPTHistoryType { get; set; }
        public List<ICDCPTMapping.ICDCPTCodemappingViewModel> CPTCodesHistory { get; set; }
    }
}