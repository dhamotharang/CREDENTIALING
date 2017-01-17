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

        #region Phone Number

        //[Required]
        //[MaxLength(100)]
        //[Index(IsUnique = true)]
        public string PhoneNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.CountryCode))
                    return this.Number;
                else if (!String.IsNullOrEmpty(this.Number))
                    return this.CountryCode + "-" + this.Number;

                return null;
            }
            set
            {
                //Number = value;
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Number = numbers[0];
                    else
                    {
                        this.CountryCode = numbers[0];
                        this.Number = numbers[1];
                    }
                }
            }
        }

        [NotMapped]
        public string Number { get; set; }

        [NotMapped]
        public string CountryCode { get; set; }


        #endregion

        #region PhoneType

        //[Required]
        public string PhoneType { get;  set; }

        [NotMapped]
        public virtual PhoneTypeEnum? PhoneTypeEnum
        {
            get
            {
                if (String.IsNullOrEmpty(this.PhoneType))
                    return null;

                if (this.PhoneType.Equals("Not Available"))
                    return null;

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
        public string Preference { get;  set; }

        [NotMapped]
        public PreferenceType? PreferenceType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Preference))
                    return null;

                if (this.Preference.Equals("Not Available"))
                    return null;

                return (PreferenceType)Enum.Parse(typeof(PreferenceType), this.Preference);
            }
            set
            {
                this.Preference = value.ToString();
            }
        }
        
        #endregion        

        #region Status

        //[Required]
        public string Status { get; set; }

        [NotMapped]
        public virtual StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
                    return null;

                if (this.Status.Equals("Not Available"))
                    return null;

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
    }
}
