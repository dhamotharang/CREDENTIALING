using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class BusinessOfficeContactPerson
    {
        public BusinessOfficeContactPerson()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int BusinessOfficeContactPersonID { get; set; }

        [Required]
        public string FirstName { get; set; }
        
        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        #region Telephone

        public string Telephone
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
                if (value != null)
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
        
        public string EmailID { get; set; }

        //public virtual ICollection<PracticeLocationDetail> PracticeLocationDetails { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
