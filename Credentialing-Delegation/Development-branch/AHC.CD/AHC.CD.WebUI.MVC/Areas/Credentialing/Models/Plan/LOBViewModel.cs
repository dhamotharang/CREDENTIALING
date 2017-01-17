using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.WebUI.MVC.Areas.Credentialing.Models.Plan;
using AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models
{
    public class LOBViewModel
    {
        public int LOBID { get; set; }

        #region Lob Contact Detail

        public ICollection<LOBContactDetailViewModel> LOBContactDetails { get; set; }   

        #endregion

        #region Lob Address Detail

        public ICollection<LOBAddressDetailViewModel> LOBAddressDetails { get; set; }   

        #endregion

        #region Sub Plans

        public ICollection<SubPlanViewModel> SubPlans { get; set; } 

        #endregion

        #region Status

        public StatusType? StatusType { get; set; }

        #endregion 
    }
}
