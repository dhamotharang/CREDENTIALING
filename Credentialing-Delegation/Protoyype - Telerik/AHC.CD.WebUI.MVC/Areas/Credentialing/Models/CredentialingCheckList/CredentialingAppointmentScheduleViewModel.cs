using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Credentialing.Models.CredentialingCheckList
{
    public class CredentialingAppointmentScheduleViewModel
    {
        public int CredentialingAppointmentScheduleID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? AppointmentDate { get; set; }

        public int? AppointmentSetByID { get; set; }
    }
}