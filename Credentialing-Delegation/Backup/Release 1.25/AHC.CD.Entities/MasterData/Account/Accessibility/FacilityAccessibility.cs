using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account.Accessibility
{
    public class FacilityAccessibility
    {
        public FacilityAccessibility()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int FacilityAccessibilityID { get; set; }

        public string OtherHandicappedAccess { get; set; }

        public string OtherDisabilityServices { get; set; }

        public string OtherTransportationAccess { get; set; }

        public virtual ICollection<FacilityAccessibilityQuestionAnswer> FacilityAccessibilityQuestionAnswers { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
