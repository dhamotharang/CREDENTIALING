using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class OrganizationGroupViewModel
    {
        public int GroupID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string TaxId { get; set; }

        public string NPINumber { get; set; }

        public StatusType? StatusType { get; set; }
        
    }
}