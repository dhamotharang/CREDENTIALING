using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public class ContactInfo
    {
        public ContactInfo()
        {
            LastUpdatedDateTime = DateTime.Now;
        }
        public int ContactInfoID { get; set; }

        public virtual int CountryCode
        {
            get;
            set;
        }

        public virtual string PhoneNo
        {
            get;
            set;
        }

        public virtual ContactType ContactType
        {
            get;
            set;
        }

        [Column(TypeName = "datetime2")]
        public virtual DateTime? LastUpdatedDateTime
        {
            get;
            set;
        }

    }
}
