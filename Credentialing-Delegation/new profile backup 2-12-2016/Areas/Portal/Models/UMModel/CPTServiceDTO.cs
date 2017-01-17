using PortalTemplate.Areas.UM.Models.ViewModels.CPT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.UMModel
{
    public class CPTServiceDTO
    {
        public List<CPTViewModel> CPTCodeList { get; set; }
        public int CPTCodeCount { get; set; }
        public object Message { get; set; }
    }
}