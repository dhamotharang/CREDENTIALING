using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class CPTCodeViewModel
    {
        public int CPTCodeID { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        public string Version { get; set; }
    }
}