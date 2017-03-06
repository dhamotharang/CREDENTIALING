using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.TaskTracker
{
    public class TaskTrackerHistory
    {
        public TaskTrackerHistory()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int TaskTrackerHistoryID { get; set; }

        public int? ProviderID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime NextFollowUpDate { get; set; }

        public string SubSectionName { get; set; }

        public string Subject { get; set; }

        public string ModeOfFollowUp { get; set; }

        public int? AssignToCCOID { get; set; }

        public int? AssignedByCCOID { get; set; }

        public int? HospitalId { get; set; } 

        //public string InsuaranceCompanyNames { get; set; }

        public int? InsuaranceCompanyNameID { get; set; }

        public int? PlanID { get; set; }

        public string LastUpdatedBy { get; set; }

        public string Notes { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public TaskTrackerStatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                return (TaskTrackerStatusType)Enum.Parse(typeof(TaskTrackerStatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
