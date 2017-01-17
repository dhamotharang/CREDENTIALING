using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class MasterDataPlaceOfServiceViewModel
    {
        public int PlaceOfServiceID { get; set; }

        [JsonProperty("ShortDescription")]
        public string Name { get; set; }

        [JsonProperty("CodeValue")]
        public string Code { get; set; }

        public string Description { get; set; }
    }
}