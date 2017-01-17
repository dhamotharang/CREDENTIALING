using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class OutcomeViewModel
    {
        public int? OutcomeID { get; set; }

        public string OutcomeType { get; set; }

        public string ContactTypeName { get; set; }

        public string EntityName { get; set; }

        public string OutcomeTypeName { get; set; }

    }
}