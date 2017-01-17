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

        public ICollection<PhoneDetail> PhoneDetails { get; set; }
        public ICollection<EmailDetail> EmailIDs { get; set; }
        public ICollection<PreferredWrittenContact> PreferredWrittenContacts { get; set; }
        public ICollection<PreferredContact> PreferredContacts { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
