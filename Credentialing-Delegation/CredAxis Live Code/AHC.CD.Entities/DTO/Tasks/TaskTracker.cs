using System;
using System.Collections.Generic;

namespace AHC.CD.Entities.DTO.Tasks
{
    public class TaskTracker
    {
        public TaskTracker()
        {
            ModeOfFollowUp = new List<string>();
        }
        public string FollowUps { get; set; }
        public int TaskTrackerID { get; set; }
        public string ProviderName { get; set; }
        public string SubSectionName { get; set; }
        public string Subject { get; set; }
        public string PlanName { get; set; }
        public List<string> ModeOfFollowUp { get; set; }
        public DateTime? NextFollowUpDate { get; set; }
        public int AssignedToId { get; set; }
        public int AssignedById { get; set; }
        public int DaysLeft { get; set; }
    }
}
