using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public class PersonalIdentificationInfo
    {
        public int PersonalIdentificationInfoID { get; set; }

        public string IdentificationNo { get; set; }

        public PersonalIdentificationType PersonalIdentificationType { get; set; }

        [Column(TypeName = "datetime2")]
        public virtual DateTime? LastUpdatedDateTime
        {
            get;
            set;
        }

    }
}
