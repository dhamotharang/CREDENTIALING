using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class MasterDataOrganizationGroupViewModel
    {
        public int OrganizationGroupID { get; set; }

        public string GroupName { get; set; }

        public string GroupDescription { get; set; }

        public StatusType? StatusType { get; set; }
    }
}