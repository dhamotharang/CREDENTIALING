using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing.DTO
{
  public  class PracticeLocationDTO
    {
        public string PracticeLocationName { get; set; }

        // public Facility Facility { get; set; }

        public int FacilityID { get; set; }

        public string FacilityName { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        #region Address

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        #endregion

        #region Mobile Number

        public string MobileNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.CountryCodeTelephone))
                    return this.Telephone;
                else if (!String.IsNullOrEmpty(this.Telephone))
                    return this.CountryCodeTelephone + "-" + this.Telephone;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.Telephone = numbers[0];
                    else
                    {
                        this.CountryCodeTelephone = numbers[0];
                        this.Telephone = numbers[1];
                    }

                }
            }
        }

        public string Telephone { get; set; }


        public string CountryCodeTelephone { get; set; }

        #endregion

        #region Fax Number

        public string FaxNumber
        {
            get
            {
                if (String.IsNullOrEmpty(this.CountryCodeFax))
                    return this.Fax;
                else if (!String.IsNullOrEmpty(this.Fax))
                    return this.CountryCodeFax + "-" + this.Fax;

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
                        this.CountryCodeFax = numbers[0];
                        this.Fax = numbers[1];
                    }
                }
            }
        }


        public string Fax { get; set; }


        public string CountryCodeFax { get; set; }

        #endregion

        public string EmailAddress { get; set; }





        public DateTime LastModifiedDate { get; set; }
    }
}
