using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.CreateAuditing
{
    public class ICDCodesInfoViewModel
    {
        public ICDCodesInfoViewModel()
        {
            this.ICDCodes = new List<ICDCodeDetailsViewModel>();          
        }
        [DisplayName("ICD Indicator : ")]
        public string ICDIndicatorType { get; set; }
        public List<ICDCodeDetailsViewModel> ICDCodes { get; set; }
    }
}