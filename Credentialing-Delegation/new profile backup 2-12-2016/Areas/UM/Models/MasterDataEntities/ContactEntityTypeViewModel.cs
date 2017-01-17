using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class ContactEntityTypeViewModel
    {
        public int ContactEntityID { get; set; }
        [JsonProperty("EntityType")]
        public string EntityName { get; set; }
        public int ContactEntityTypeID { get; set; }
        public string ContactEntityType { get; set; }
    }
}