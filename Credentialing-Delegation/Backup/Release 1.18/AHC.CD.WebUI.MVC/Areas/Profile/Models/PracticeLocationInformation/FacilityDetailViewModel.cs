using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class FacilityDetailViewModel
    {
        public int FacilityDetailID { get; set; }
        
        public FacilityServiceViewModel Service { get; set; }
        public FacilityAccessibilityViewModel Accessibility { get; set; }
        public FacilityLanguageViewModel Language { get; set; }
        public PracticeOfficeHourViewModel PracticeOfficeHour { get; set; }
        //public ICollection<FacilityEmployeeViewModel> Employees { get; set; }
        //public ICollection<FacilityWorkHourViewModel> FacilityWorkHours { get; set; }
    }
}
