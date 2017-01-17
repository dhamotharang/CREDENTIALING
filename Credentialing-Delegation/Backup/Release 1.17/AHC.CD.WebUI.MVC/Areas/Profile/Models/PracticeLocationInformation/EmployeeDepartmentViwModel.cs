using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocationInformation
{
    public class EmployeeDepartmentViwModel
    {
        public int EmployeeDepartmentID { get; set; }

        public int DepartmentID { get; set; }

        public YesNoOption? PrimaryYesNoOption { get; set; }

    }
}
