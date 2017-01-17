using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.CreateCoding
{
    public class ICDCodeDetailsViewModel
    {
        public ICDCodeDetailsViewModel()
        {
            this.ICDCodes = new List<ICDCodes.ICDCodeViewModel>();
        }
        public bool IsICD10 { get; set; }
        public List<ICDCodes.ICDCodeViewModel> ICDCodes { get; set; }
    }
}