using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Plan
{
    public class PlanContractViewModel
    {
        public int? PlanContractID { get; set; }

        public int PlanLOBID { get; set; }

        public int OrganizationGroupID { get; set; }

        //public bool IsDelegated { get; set; }

        #region Status

        public StatusType? StatusType { get; set; }

        #endregion  
    }
}