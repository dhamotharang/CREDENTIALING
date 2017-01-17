using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.CustomField
{
    public class CustomFieldTransactionViewModel
    {
        public int CustomFieldTransactionID { get; set; }

        public ICollection<CustomFieldTransactionDataViewModel> CustomFieldTransactionDatas { get; set; }

        public StatusType? StatusType { get; set; }
    }
}