using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Auditing.Models.CreateAuditing
{
    public class ICDCodeDetailsViewModel
    {
        public ICDCodeDetailsViewModel()
        {
            this.Categories = new List<CategoryViewModel>();
            this.HCCCodes = new List<HCCCodeDetailsViewModel>();
        }
        public string Code { get; set; }
        public string Description { get; set; }
        public List<HCCCodeDetailsViewModel> HCCCodes = new List<HCCCodeDetailsViewModel>();
        public Boolean IsAgree { get; set; }
        public List<CreateAuditing.CategoryViewModel> Categories { get; set; }
    }
}