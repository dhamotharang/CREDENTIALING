using AHC.CD.Entities.MasterProfile.BoardSpecialty;
using AHC.CD.Entities.MasterProfile.Demographics;
using AHC.CD.Entities.MasterProfile.PracticeLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.DTO
{
   public class ProfileReport
    {
        public PersonalDetail PersonalDetail { set;get; }
        public ICollection<PracticeLocationDetail> PracticeLocationDetails { get; set; }
        public LanguageInfo LanguageDetails { get; set; }
        public ICollection<SpecialtyDetail> SpecialityDetails { get; set; }
        public int? ProfileID { get; set; }
    }
}
