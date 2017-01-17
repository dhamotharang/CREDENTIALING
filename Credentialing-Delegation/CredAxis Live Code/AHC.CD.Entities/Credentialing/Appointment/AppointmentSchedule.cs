using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AHC.CD.Entities.Credentialing.PSV;

namespace AHC.CD.Entities.Credentialing.Appointment
{
    public class AppointmentSchedule
    {
        public AppointmentSchedule()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int AppointmentScheduleID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime AppointmentDate { get; set; }

        public int? ScheduledByID { get; set; }
        [ForeignKey("ScheduledByID")]
        public CDUser ScheduledBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ScheduledDate { get; set; }

        public bool IsNotified { get; set; }

        public ICollection<PlanPSVDetail> PlanPSVDetails { get; set; }

        public PlanCCMDetail PlanCCMDetail { get; set; }

        public CCMReport CCMReport { get; set; }

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
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
