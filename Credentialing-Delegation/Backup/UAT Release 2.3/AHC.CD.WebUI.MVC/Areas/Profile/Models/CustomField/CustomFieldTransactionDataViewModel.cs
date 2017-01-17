using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.CustomField
{
    public class CustomFieldTransactionDataViewModel
    {
        public int CustomFieldTransactionDataID { get; set; }

        public int CustomFieldID { get; set; }

        public string CustomFieldTransactionDataValue { get; set; }

        public StatusType? StatusType { get; set; }
    }
}