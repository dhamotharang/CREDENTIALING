using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class KnownLanguage
    {
        public KnownLanguage()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int KnownLanguageID { get; set; }

        //[Required]
        public string Language { get; set; }

        //[Required]
        public int ProficiencyIndex { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
