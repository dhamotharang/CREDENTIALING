using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing
{
    public class PlanContactDetail
    {
        public PlanContactDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int PlanContactDetailID { get; set; }

        public string ContactPersonName { get; set; }

        public ContactDetail ContactDetail { get; set; }        

        //public string EmailAddress { get; set; }

        //public bool IsPrimary { get; set; }

        //#region Phone Number

        //public string Number
        //{
        //    get
        //    {
        //        if (String.IsNullOrEmpty(this.PhoneCountryCode))
        //            return this.PhoneNumber;
        //        else if (!String.IsNullOrEmpty(this.PhoneNumber))
        //            return this.PhoneCountryCode + "-" + this.PhoneNumber;

        //        return null;
        //    }
        //    set
        //    {
        //        //Number = value;
        //        if (value != null)
        //        {
        //            var numbers = value.Split(new char[] { '-' }, 2);
        //            if (numbers.Length == 1)
        //                this.PhoneNumber = numbers[0];
        //            else
        //            {
        //                this.PhoneCountryCode = numbers[0];
        //                this.PhoneNumber = numbers[1];
        //            }
        //        }
        //    }
        //}

        //[NotMapped]
        //public string PhoneNumber { get; set; }

        //[NotMapped]
        //public string PhoneCountryCode { get; set; }

        //#endregion

        //#region Fax Number

        ////[Required]
        //public string ContactPersonFax
        //{
        //    get
        //    {
        //        if (String.IsNullOrEmpty(this.FaxCountryCode))
        //            return this.FaxNumber;
        //        else if (!String.IsNullOrEmpty(this.FaxNumber))
        //            return this.FaxCountryCode + "-" + this.FaxNumber;

        //        return null;
        //    }
        //    set
        //    {
        //        if (value != null)
        //        {
        //            var numbers = value.Split(new char[] { '-' }, 2);
        //            if (numbers.Length == 1)
        //                this.FaxNumber = numbers[0];
        //            else
        //            {
        //                this.FaxCountryCode = numbers[0];
        //                this.FaxNumber = numbers[1];
        //            }
        //        }
        //    }
        //}

        //[NotMapped]
        //public string FaxNumber { get; set; }

        //[NotMapped]
        //public string FaxCountryCode { get; set; }

        //#endregion

        public DateTime LastModifiedDate { get; set; }
    }
}
