using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.Demographic
{
    public class ProviderTitleViewModel
    {
        public int ProviderTitleID { get; set; }

        [Required]
        public int ProviderTypeId { get; set; }

        #region Status

        [Required]
        public StatusType StatusType { get; set; }

        #endregion 
    }
}