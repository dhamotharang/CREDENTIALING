using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public class FamilyInfo
    {
        public virtual int FamilyInfoID
        {
            get;
            set;
        }

        public virtual string SpouseName
        {
            get;
            set;
        }

        [Column(TypeName="datetime2")]
        public DateTime? LastUpdatedDateTime { get; set; }
      
    }
}
