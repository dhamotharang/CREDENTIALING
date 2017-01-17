using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class MidLevelPractitioner
    {
        public int MidLevelPractitionerID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string LicenceNumber { get; set; }
        public string State { get; set; }
        public string PractitionerType { get; set; }
        public bool MidLevelPractitionersCareForPatients { get; set; }
        

    }
}
