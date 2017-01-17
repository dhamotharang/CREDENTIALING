using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Portal.Models.PriorAuthMasterDataEntities
{
    public class DisciplineViewModel
    {
        public int DisciplineID { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }
    }
}