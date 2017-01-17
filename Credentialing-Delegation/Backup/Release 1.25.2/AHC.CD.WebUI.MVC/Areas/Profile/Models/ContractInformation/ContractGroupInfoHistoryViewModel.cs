using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.ContractInformation
{
    public class ContractGroupInfoHistoryViewModel
    {
        
        public int ContractGroupInfoHistoryID { get; set; }

        public ProviderRelationshipOption? ProviderRelationshipOption { get; set; }

        public DateTime? IssueDate { get; set; }

        public DateTime? ExpiryDate { get; set; }



        public string ContractGroupCerificatePath { get; set; }
    }
}