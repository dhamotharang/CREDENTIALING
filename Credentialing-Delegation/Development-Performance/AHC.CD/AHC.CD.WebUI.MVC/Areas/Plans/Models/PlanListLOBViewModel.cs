using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Plans.Models
{
    public class PlanListLOBViewModel
    {
        public int LOBID { get; set; }

        public int? PlanLOBID { get; set; }

        public int? PlanID { get; set; }

        #region Lob Contact Detail

        public ICollection<PlanLOBContactDetailsViewModel> LOBContactDetails { get; set; }

        #endregion

        #region Lob Address Detail

        public ICollection<PlanLOBAddressDetailsViewModel> LOBAddressDetails { get; set; }

        #endregion

        #region Sub Plans

        public ICollection<SubPlansViewModel> SubPlans { get; set; }

        #endregion

        #region Status

        public StatusType? StatusType { get; set; }

        #endregion 
    }
}