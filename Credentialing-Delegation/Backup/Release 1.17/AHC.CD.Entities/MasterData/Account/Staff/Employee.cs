using AHC.CD.Entities.MasterData.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterData.Account.Staff
{
    public class Employee
    {
        public Employee()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int EmployeeID { get; set; }
        
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string FirstName { get; set; }

        #region Address

        public string Building { get; set; }

        public string Street { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string County { get; set; }

        public string City { get; set; }

        public string ZipCode { get; set; }

        public string POBoxAddress { get; set; }   

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

        [NotMapped]
        public string Telephone { get; set; }

        [NotMapped]
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

        [NotMapped]
        public string Fax { get; set; }

        [NotMapped]
        public string CountryCodeFax { get; set; }

        #endregion

        public string EmailAddress { get; set; }

        public ICollection<EmployeeDepartment> Departments { get; set; }
        
        public ICollection<EmployeeDesignation> Designations { get; set; }

        #region Status

        public string Status { get; set; }

        [NotMapped]
        public StatusType? StatusType
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
