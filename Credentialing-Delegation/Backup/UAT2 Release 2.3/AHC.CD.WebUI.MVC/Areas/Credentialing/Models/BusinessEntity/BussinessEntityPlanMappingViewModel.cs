using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.BusinessEntity
{
    public class BussinessEntityPlanMappingViewModel
    {
        public int BEPlanMappingID { get; set; }

        [Required]
        public int GroupID { get; set; }

        [Required]
        public int PlanID { get; set; }

        public int MappedByID { get; set; }

        public int? ChangedByID { get; set; }

        #region Status

        public StatusType? StatusType { get; set; }

        #endregion  
    }
}