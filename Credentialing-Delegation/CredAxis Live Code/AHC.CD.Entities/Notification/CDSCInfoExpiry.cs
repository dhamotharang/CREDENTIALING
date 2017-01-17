using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class CDSCInfoExpiry
    {
        public CDSCInfoExpiry()
        {
            this.LastModifiedDate = DateTime.Now;
        }

        public int CDSCInfoExpiryID { get; set; }
        public int CDSCInformationID { get; set; }
        public string CertNumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string State { get; set; }

        public DateTime? NotificationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
