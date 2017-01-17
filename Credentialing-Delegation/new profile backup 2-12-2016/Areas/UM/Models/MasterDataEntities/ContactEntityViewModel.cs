using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class ContactEntityViewModel
    {
        public int ContactEntityID { get; set; }

        public string EntityName { get; set; }

        public int? ContactEntityTypeID { get; set; } 
    }
}