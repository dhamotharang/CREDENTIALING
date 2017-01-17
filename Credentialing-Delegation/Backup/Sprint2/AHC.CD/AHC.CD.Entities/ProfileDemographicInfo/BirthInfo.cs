using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public class BirthInfo
    {
        public int BirthInfoID { get; set; }
        public string CityOfBirth { get; set; }
        public string CountryOfBirth { get; set; }
        public string CountyOfBirth { get; set; }
        [Column(TypeName="datetime2")]
        public DateTime? DateOfBirth { get; set; }
        public string StateOfBirth { get; set; }
        [Column(TypeName = "datetime2")]
        public virtual DateTime? LastUpdatedDateTime{get;set;}
    }
}
