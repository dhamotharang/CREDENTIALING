using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.ICDCodes
{
    public class ICDCodeViewModel
    {
        public ICDCodeViewModel()
        {
            this.HCCCodes = new List<HCCCodeViewModel>();
        }
        public string ICDCode { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
        public List<HCCCodeViewModel> HCCCodes { get; set; }
        public Boolean IsChronic { get; set; }
        public int ChronicCount { get; set; }
    }
}