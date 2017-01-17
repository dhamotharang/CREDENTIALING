using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class PracticeColleagueViewModel
    {

        public int PracticeLocationDetailID { get; set; }

        public int PracticeColleagueID { get; set; }
        
        public int ProfileID { get; set; }

        public StatusType StatusType { get; set; }

    }
}