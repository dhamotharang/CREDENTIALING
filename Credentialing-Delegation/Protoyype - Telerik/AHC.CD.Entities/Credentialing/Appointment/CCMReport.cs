using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.Appointment
{
    public class CCMReport
    {
        public CCMReport()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int CCMReportID { get; set; }

        public int? ReportByID { get; set; }
        [ForeignKey("ReportByID")]
        public CDUser ReportBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
