using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class HospitalOnlyViewModel
    {
        public int HospitalID { get; set; } 
   
        public string HospitalName { get; set; }

        public string Code { get; set; }

        public StatusType? StatusType { get; set; }
    }
}