using AHC.CD.Entities.MasterProfile.Demographics;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.Credentialing
{
    public class LOBContactDetail
    {
        public LOBContactDetail()
        {
            LastModifiedDate = DateTime.Now;
        }

        public int LOBContactDetailID { get; set; } 

        public string ContactPersonName { get; set; }

        public ContactDetail ContactDetail { get; set; }    

        public DateTime LastModifiedDate { get; set; }
    }
}
