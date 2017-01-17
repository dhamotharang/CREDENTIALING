using AHC.CD.Entities.MasterData.Enums;
using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class PracticeOfficeHourViewModel
    {
        public int PracticeLocationDetailID { get; set; }

        public int PracticeOfficeHourID { get; set; }

        public ICollection<PracticeDayViewModel> PracticeDays { get; set; }
    }
}