using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterData.Enums;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class HospitalViewModel
    {
        //public HospitalOnlyViewModel HospitalOnlyViewModel { get; set;}
        public int HospitalID { get; set; }

        public string HospitalName { get; set; }

        public string Code { get; set; }

        public StatusType? StatusType { get; set; }

        public HospitalContactInfoViewModel HospitalContactInfoViewModel { get; set; }

        public HospitalContactPersonViewModel HospitalContactPersonViewModel { get; set; }
    }
}