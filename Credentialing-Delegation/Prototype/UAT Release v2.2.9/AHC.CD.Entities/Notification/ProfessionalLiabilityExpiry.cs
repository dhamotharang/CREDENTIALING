using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Notification
{
    public class ProfessionalLiabilityExpiry
    {
        public ProfessionalLiabilityExpiry()
        {
            this.LastModifiedDate = DateTime.Now;
        }

        public int ProfessionalLiabilityExpiryID { get; set; }
        public int ProfessionalLiabilityInfoID { get; set; }
        public string InsuranceCarrier { get; set; }
        public DateTime? EffectiveDate { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string PolicyNumber { get; set; }

        public DateTime? NotificationDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
