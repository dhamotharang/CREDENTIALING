using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.UM.Models.MasterDataEntities
{
    public class AuthorizationTypeViewModel
    {
        public int AuthorizationTypeID { get; set; }
        [JsonProperty("Title")]
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
    }
}