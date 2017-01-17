using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Coding.Models.CPTCodes
{
    public class CPTCodeViewModel
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public double Fee { get; set; }
        public Boolean isEnM { get; set; }
    }
}