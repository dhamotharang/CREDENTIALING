using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.Demographics
{
    public class ContactDetail
    {
        public ContactDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int ContactDetailID { get; set; }

        #region Pager Number

        public string PersonalPager
        {
            get
            {
                if (String.IsNullOrEmpty(this.CountryCode))
                    return this.PagerNumber;
                else if (!String.IsNullOrEmpty(this.PagerNumber))
                    return this.CountryCode + "-" + this.PagerNumber;

                return null;
            }
            set
            {
                if (value != null)
                {
                    var numbers = value.Split(new char[] { '-' }, 2);
                    if (numbers.Length == 1)
                        this.PagerNumber = numbers[0];
                    else
                    {
                        this.CountryCode = numbers[0];
                        this.PagerNumber = numbers[1];
                    }
                }
            }
        }
        
        [NotMapped]
        public string PagerNumber { get; set; }

        [NotMapped]
        public string CountryCode { get; set; }


        #endregion


        public virtual ICollection<PhoneDetail> PhoneDetails { get; set; }
        public virtual ICollection<EmailDetail> EmailIDs { get; set; }
        public virtual ICollection<PreferredWrittenContact> PreferredWrittenContacts { get; set; }
        public virtual ICollection<PreferredContact> PreferredContacts { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
