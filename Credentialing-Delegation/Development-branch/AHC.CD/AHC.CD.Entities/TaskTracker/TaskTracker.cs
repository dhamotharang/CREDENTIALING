using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using AHC.CD.Entities.MasterProfile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.TaskTracker
{
    public class TaskTracker
    {

        public TaskTracker()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int TaskTrackerId { get; set; }

        public int? ProfileID { get; set; }
        [ForeignKey("ProfileID")]
        public Profile Provider { get; set; }

        public string SubSectionName { get; set; }

        public string Subject { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime NextFollowUpDate { get; set; }

        #region Mode Of Follow-Up

        public string ModeOfFollowUp { get; set; }

        #endregion

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

        public string Notes { get; set; }

        //public string InsuaranceCompanyNames { get; set; }

        public string LastUpdatedBy { get; set; }

        public ICollection<TaskTrackerHistory> TaskTrackerHistories { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
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
