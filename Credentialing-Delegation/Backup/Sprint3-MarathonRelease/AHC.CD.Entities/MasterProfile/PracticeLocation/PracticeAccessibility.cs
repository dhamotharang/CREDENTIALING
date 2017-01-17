using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeAccessibility
    {
        public PracticeAccessibility()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int PracticeAccessibilityID { get; set; }

        public ICollection<PracticeAccessibilityQuestionAnswer> PracticeAccessibilityQuestionAnswers { get; set; }
        
        public string OtherHandicapedAccess { get; set; }
        public string OtherDisabilityServices { get; set; }
        public string OtherTransportationServices { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
