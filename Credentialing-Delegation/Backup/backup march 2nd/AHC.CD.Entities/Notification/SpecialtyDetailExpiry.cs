using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class SpecialtyDetailExpiry
    {
        public SpecialtyDetailExpiry()
        {
            this.LastModifiedDate = DateTime.Now;
        }

        public int SpecialtyDetailExpiryID { get; set; }
        public int SpecialtyDetailID { get; set; }
        public string SpecialtyName { get; set; }
        public string SpecialtyBoardName { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string CertificateNumber { get; set; }

        public DateTime? NotificationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
