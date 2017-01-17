using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.AppointmentInformation
{
    public class CredentialingAppointmentSchedule
    {
        public CredentialingAppointmentSchedule()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CredentialingAppointmentScheduleID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? AppointmentDate { get; set; }

        public int? AppointmentSetByID { get; set; }
        [ForeignKey("AppointmentSetByID")]
        public CDUser AppointmentSetBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
