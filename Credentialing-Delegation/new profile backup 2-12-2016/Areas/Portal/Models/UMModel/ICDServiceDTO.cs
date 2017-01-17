using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.UMModel
{
    public class ICDServiceDTO
    {
        public List<ICDCodeList> IcdCodeList { get; set; }
        public int IcdCodeCount { get; set; }
        public object Message { get; set; }
    }
}