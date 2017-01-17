using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Models.MasterDataNewViewModel
{
    public class MilitaryRankViewModel
    {

        public int MilitaryRankID { get; set; }

        public string Title { get; set; }

        public StatusType? StatusType { get; set; }

        public MilitaryBranchViewModel MilitaryBranch { get; set; }
        
        
    }
}