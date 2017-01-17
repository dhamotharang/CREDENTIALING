using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class HospitalPrivilegeExpiry
    {
        public HospitalPrivilegeExpiry()
        {
            this.LastModifiedDate = DateTime.Now;
        }

        public int HospitalPrivilegeExpiryID { get; set; }
        public int HospitalPrivilegeDetailID { get; set; }
        public string HospitalName { get; set; }
        public DateTime? AffilicationStartDate { get; set; }
        public DateTime? AffiliationEndDate { get; set; }

        public DateTime? NotificationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
