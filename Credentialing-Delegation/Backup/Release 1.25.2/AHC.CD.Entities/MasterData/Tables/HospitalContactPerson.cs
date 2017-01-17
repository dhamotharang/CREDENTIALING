using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.MasterData.Tables
{
    public class HospitalContactPerson
    {
        public HospitalContactPerson()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int HospitalContactPersonID { get; set; }
        
        //[Required]
        public string ContactPersonName { get; set; }

        #region Phone Number

        //[Required]
        public string ContactPersonPhone
        {
            get
            {
                if (String.IsNullOrEmpty(this.PhoneCountryCode))
                    return this.Phone;
                else if (!String.IsNullOrEmpty(this.Phone))
                    return this.PhoneCountryCode + "-" + this.Phone;

                return null;
            }
            set
            {
                if(value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Phone = numbers[0];
                    else
                    {
                        this.PhoneCountryCode = numbers[0];
                        this.Phone = numbers[1];
                    }
                }
            }
        }

        [NotMapped]
        public string Phone { get; set; }

        [NotMapped]
        public string PhoneCountryCode { get; set; }


        #endregion

        #region Fax Number

        //[Required]
        public string ContactPersonFax
        {
            get
            {
                if (String.IsNullOrEmpty(this.FaxCountryCode))
                    return this.Fax;
                else if (!String.IsNullOrEmpty(this.Fax))
                    return this.FaxCountryCode + "-" + this.Fax;

                return null;
            }
            set
            {
                if(value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Fax = numbers[0];
                    else
                    {
                        this.FaxCountryCode = numbers[0];
                        this.Fax = numbers[1];
                    }
                }
            }
        }

        [NotMapped]
        public string Fax { get; set; }

        [NotMapped]
        public string FaxCountryCode { get; set; }

        #endregion

        #region Status

        public string Status { get; private set; }

        [NotMapped]
        public StatusType? StatusType
        {
            get
            {
                if (String.IsNullOrEmpty(this.Status))
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
