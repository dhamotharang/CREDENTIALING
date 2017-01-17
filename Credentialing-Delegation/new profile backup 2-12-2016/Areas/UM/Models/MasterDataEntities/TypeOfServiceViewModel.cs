using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class TypeOfServiceViewModel
    {
        public int TypeOfServiceID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}