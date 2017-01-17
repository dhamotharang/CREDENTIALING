using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.CreateCoding
{
    public class ICDCodeHistoryDetailsViewModel
    {
        public ICDCodeHistoryDetailsViewModel()
        {
            this.ICDCodesHistory = new List<ICDCodes.ICDCodeViewModel>();
        }
        public string ICDHistoryIndicatorType { get; set; }
        public List<ICDCodes.ICDCodeViewModel> ICDCodesHistory { get; set; }
    }
}