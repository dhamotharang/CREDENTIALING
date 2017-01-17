using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.CustomField
{
    public class CustomFieldViewModel
    {
        public int CustomFieldID { get; set; }

        public string CustomFieldTitle { get; set; }

        public CustomFieldCategoryType? customFieldCategoryType { get; set; }

        public StatusType? StatusType { get; set; }
    }
}