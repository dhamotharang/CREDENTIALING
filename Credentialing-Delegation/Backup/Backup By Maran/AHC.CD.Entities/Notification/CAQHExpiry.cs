using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class CAQHExpiry
    {
        public CAQHExpiry()
        {
            this.LastModifiedDate = DateTime.Now;
        }
        public int CAQHExpiryID { get; set; }

        public string CAQHNumber { get; set; }

        public DateTime? NextAttestationDate { get; set; }

        public DateTime? NotificationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

    }
}
