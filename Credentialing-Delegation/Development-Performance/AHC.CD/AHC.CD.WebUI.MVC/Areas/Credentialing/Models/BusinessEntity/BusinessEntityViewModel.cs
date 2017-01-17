using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.BusinessEntity
{
    public class BusinessEntityViewModel
    {
        //public int BusinessEntityID { get; set; }
        public int GroupID { get; set; }

        public string Name { get; set; }

        #region Status

        public StatusType? StatusType { get; set; }

        #endregion  
    }
}