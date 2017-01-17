using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.Appointment
{
    public class PlanCCMDetail
    {
        public PlanCCMDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PlanCCMDetailID { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
