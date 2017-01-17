using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.ProfileDemographicInfo
{
    public class AddressInfo
    {
        public AddressInfo()
        {
            LastUpdatedDateTime = DateTime.Now;
        }
        
        public int AddressInfoID { get; set; }

        [Required]
        public virtual string City
        {
            get;
            set;
        }

        [Required]
        public virtual string State
        {
            get;
            set;
        }

        [Required]
        public virtual string County
        {
            get;
            set;
        }

        [Required]
        public virtual string Country
        {
            get;
            set;
        }

        [Required]
        public virtual string Address
        {
            get;
            set;
        }

        public string UnitNumber { get; set; }

        [Required]
        public virtual string ZipCode
        {
            get;
            set;
        }

        public virtual int DurationOfStayInMonths
        {
            get;
            set;
        }

        public virtual bool IsActive
        {
            get;
            set;
        }
        //[DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [Column(TypeName = "datetime2")]
        public virtual DateTime? LastUpdatedDateTime
        {
            get;
            set;
        }

        public virtual AddressType AddressType
        {
            get;
            set;
        }

        

    }
}
