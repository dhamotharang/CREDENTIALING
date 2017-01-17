using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public class CitizenshipInfo
    {
        public int CitizenshipInfoID { get; set; }
        public bool IsResidentOfCountry { get; set; }
        public virtual NonResidentInfo NonResidentInfo { get; set; }
    }
}
