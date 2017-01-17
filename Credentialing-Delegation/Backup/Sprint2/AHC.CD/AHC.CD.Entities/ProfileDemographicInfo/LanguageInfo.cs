using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public class LanguageInfo
    {
        public int LanguageInfoID { get; set; }
        public bool CanSpeekSignLanguage { get; set; }
        public virtual ICollection<Language> Languages { get; set; }
    }
}
