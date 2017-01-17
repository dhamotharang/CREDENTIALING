using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class DEALicenseExpiry
    {
        public DEALicenseExpiry()
        {
            this.LastModifiedDate = DateTime.Now;
        }
        
        public int DEALicenseExpiryID { get; set; }
        public int FederalDEAInformationID { get; set; }
        public string DEANumber { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string StateOfReg { get; set; }

        public DateTime? NotificationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
