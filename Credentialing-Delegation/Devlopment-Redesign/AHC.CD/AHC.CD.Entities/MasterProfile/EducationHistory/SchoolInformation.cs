using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.EducationHistory
{
    public class SchoolInformation
    {
        public SchoolInformation()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int SchoolInformationID { get; set; }

        //[Required]
        public string SchoolName { get; set; }

        public string Email { get; set; }

        #region Phone Number

        //[Required]
        public string PhoneNumber
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

        public string FaxNumber
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

        #region Address

        //[Required]
        public string Building { get; set; }

        //[Required]
        public string Street { get; set; }

        //[Required]
        public string Country { get; set; }

        //[Required]
        public string State { get; set; }

        public string County { get; set; }

        //[Required]
        public string City { get; set; }

       // [Required]
        public string ZipCode { get; set; }

        #endregion        

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
