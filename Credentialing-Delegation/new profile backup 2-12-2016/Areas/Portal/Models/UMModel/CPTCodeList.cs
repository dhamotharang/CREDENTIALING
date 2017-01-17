using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.UMModel
{
    public class CPTCodeList
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public object status { get; set; }
        public string Codewithdescription { get; set; }
        public string codewithshortdesc { get; set; }
        public bool IsEnM { get; set; }
    }
}