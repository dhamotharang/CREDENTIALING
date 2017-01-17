using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class ReasonViewModel
    {
        public int? ReasonID { get; set; }

        public string ReasonDescription { get; set; }

        public string ContactTypeName { get; set; }

        public string EntityName { get; set; }

        public string Direction { get; set; }
       
    }
}