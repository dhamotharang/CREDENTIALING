using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.SearchProvider
{
    public class SearchProviderViewModel
    {
        public string NPINumber { set; get; }        

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProviderRelationship { set; get; }

        public string IPAGroupName { get; set; }
    }
}