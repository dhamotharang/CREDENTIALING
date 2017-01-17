using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Entities.MasterProfile.PracticeLocation
{
    public class PracticeLocationDetail
    {
        public PracticeLocationDetail()
        {
            LastModifiedDate = DateTime.Now;
        }
        
        public int PracticeLocationDetailID { get; set; }

        public PracticeGeneralInformation PracticeGeneralInformation { get; set; }

        //public int BusinessOfficeContactPersonID { get; set; }
        //[ForeignKey("BusinessOfficeContactPersonID")]
        public BusinessOfficeContactPerson BusinessOfficePersonContact { get; set; }

        //public int OfficeHourID { get; set; }
        //[ForeignKey("OfficeHourID")]
        public OfficeHour OfficeHour { get; set; }

        //public int OpenPracticeStatusID { get; set; }
        //[ForeignKey("OpenPracticeStatusID")]
        public OpenPracticeStatus OpenPracticeStatus { get; set; }

        public BillingContact BillingContact { get; set; }

        public PaymentRemittance PaymentRemittance { get; set; }

        public PracticeNonEnglishLanguage PracticeNonEnglishLanguage { get; set; }

        public PracticeAccessibility PracticeAccessibility { get; set; }

        public PracticeService PracticeService { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime LastModifiedDate { get; set; }
    }
}
