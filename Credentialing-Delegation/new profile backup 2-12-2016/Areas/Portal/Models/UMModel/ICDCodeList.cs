using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.UMModel
{
    public class ICDCodeList
    {
        public int IcdCodeId { get; set; }
        public string IcdCodeNumber { get; set; }
        public string IcdCodeVersion { get; set; }
        public string Description { get; set; }
        public object last_updated { get; set; }
        public string IcdCode_Type { get; set; }
        public string ICDwithdescription { get; set; }
    }
}