using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.TaskTracker
{
    public class TaskTrackerBusinessModel
    {
        public int TaskTrackerId { get; set; }

        public int? ProfileID { get; set; }

        public string SubSectionName { get; set; }

        public string Subject { get; set; }

        public DateTime NextFollowUpDate { get; set; }

        public string ModeOfFollowUp { get; set; }

        public string InsuranceCompanyName { get; set; }

        public string AssignedToAuthId { get; set; }

        public string AssignedByAuthId { get; set; }

        public int? HospitalID { get; set; }

        public string Notes { get; set; }

        public StatusType? StatusType { get; set; } 
    }
}
