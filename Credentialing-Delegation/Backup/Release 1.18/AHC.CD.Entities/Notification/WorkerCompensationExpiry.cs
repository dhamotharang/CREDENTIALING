using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class WorkerCompensationExpiry
    {
        public WorkerCompensationExpiry()
        {
            this.LastModifiedDate = DateTime.Now;
        }

        public int WorkerCompensationExpiryID { get; set; }
        public int WorkersCompensationInformationID { get; set; }
        public string WorkersCompensationNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime IssueDate { get; set; }

        public DateTime? NotificationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
