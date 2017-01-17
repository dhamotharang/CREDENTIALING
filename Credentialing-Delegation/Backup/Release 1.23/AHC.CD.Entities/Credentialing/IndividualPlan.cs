using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing
{
    public class IndividualPlan
    {

        public int IndividualPlanID { get; set; }

        [Required]
        [ForeignKey("Plan")]
        public int PlanID { get; set; }
        
        public virtual Plan Plan
        {
            get;
            set;
        }

        public virtual ICollection<CredentialingInfo> CredentialingHistory
        {
            get;
            set;
        }

        [Column(TypeName = "datetime2")]
        public DateTime? DateActivated
        {
            get;
            set;
        }

        [Column(TypeName = "datetime2")]
        public DateTime? ExpiryDate
        {
            get;
            set;
        }
    }
}
