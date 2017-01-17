using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.DTO.Plan
{
    /// <summary>
    /// Author:Santosh Kumar Senapati
    /// Plan Module Redesign With ADO Plugin 
    /// </summary>
    public class PlanDTO
    {
        public int PlanID { get; set; }
        public string PlanLogo { get; set; }
        public string PlanName { get; set; }
        public string PlanContactPersonName { get; set; }
        public string PlanEmailID { get; set; }
        public string PlanAddress { get; set; }
        public string PlanStatus { get; set; }
    }
}
