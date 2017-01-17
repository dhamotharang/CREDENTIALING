using AHC.CD.Entities.MasterData.Enums;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class PhoneDetail
    {
        public PhoneDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int PhoneDetailID { get; set; }

        [Required]
        public string Number { get; set; }
        
        [Required]
        public string CountryCode { get; set; }

        #region PhoneType

        [Required]
        public string PhoneType { get; private set; }

        [NotMapped]
        public virtual PhoneTypeEnum PhoneTypeEnum
        {
            get
            {
                return (PhoneTypeEnum)Enum.Parse(typeof(PhoneTypeEnum), this.PhoneType);
            }
            set
            {
                this.PhoneType = value.ToString();
            }
        }

        #endregion

        #region Preference

        [Required]
        public string Preference { get; private set; }

        [NotMapped]
        public PreferenceType PreferenceType
        {
            get
            {
                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.Preference);
            }
            set
            {
                this.Preference = value.ToString();
            }
        }
        
        #endregion        

        #region Status

        [Required]
        public string Status { get; private set; }

        [NotMapped]
        public virtual StatusType StatusType
        {
            get
            {
                return (StatusType)Enum.Parse(typeof(StatusType), this.Status);
            }
            set
            {
                this.Status = value.ToString();
            }
        }

        #endregion        

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }

        public virtual ICollection<ContactDetail> ContactDetails { get; set; }
    }
}
