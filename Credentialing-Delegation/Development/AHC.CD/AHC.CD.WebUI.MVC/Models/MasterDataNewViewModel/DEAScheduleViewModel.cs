using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class DEAScheduleViewModel
    {
        public int DEAScheduleID { get; set; }
        
        public string ScheduleTitle { get; set; }
        
        public string ScheduleTypeTitle { get; set; }

        public StatusType? StatusType { get; set; }
        
    }
}