using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class TaskTrackerExpiry
    {
        public TaskTrackerExpiry()
        {
            this.LastModifiedDate = DateTime.Now;
        }

        public int TaskTrackerExpiryID { get; set; }

        public int TaskTrackerId { get; set; }
        [ForeignKey("TaskTrackerId")]
        public AHC.CD.Entities.TaskTracker.TaskTracker Task { get; set; }
        public string SubSectionName { get; set; }

        public string Subject { get; set; }

        public int DaysLeft { get; set; }

        public DateTime? NextFollowUpDate { get; set; }

        #region Mode Of Follow-Up

        public string ModeOfFollowUp { get; set; }

        #endregion
        public int? ProfileID { get; set; }
        [ForeignKey("ProfileID")]
        public Profile Provider { get; set; }

        public int? InsuaranceCompanyNameID { get; set; }
        [ForeignKey("InsuaranceCompanyNameID")]
        public InsuaranceCompanyName InsuranceCompanyName { get; set; }

        public int? AssignedToId { get; set; }
        [ForeignKey("AssignedToId")]
        public CDUser AssignedTo { get; set; }

        public int? AssignedById { get; set; }
        [ForeignKey("AssignedById")]
        public CDUser AssignedBy { get; set; }

        public int? HospitalID { get; set; }
        [ForeignKey("HospitalID")]
        public Hospital Hospital { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastUpdatedDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
