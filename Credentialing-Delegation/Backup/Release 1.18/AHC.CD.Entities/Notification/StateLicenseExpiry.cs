using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class StateLicenseExpiry
    {
        public StateLicenseExpiry()
        {
            this.LastModifiedDate = DateTime.Now;
        }

        public int StateLicenseExpiryID { get; set; }
        public int StateLicenseInformationID { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime IssueDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string IssueState { get; set; }

        public DateTime? NotificationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
