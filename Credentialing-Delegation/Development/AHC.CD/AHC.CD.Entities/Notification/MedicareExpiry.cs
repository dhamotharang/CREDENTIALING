using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class MedicareExpiry
    {
        public MedicareExpiry()
        {
            this.LastModifiedDate = DateTime.Now;
        }
        public int MedicareExpiryID { get; set; }
        public int MedicareInformationID { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime? NotificationDate { get; set; }
        
        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

    }
}
