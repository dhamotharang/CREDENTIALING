using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AHC.CD.WebUI.MVC.Areas.Profile.Models.PracticeLocation
{
    public class PracticeLocationDetailViewModel
    {
        public int PracticeLocationDetailID { get; set; }

        public PracticeGeneralInformationViewModel PracticeGeneralInformation { get; set; }

        
        public BusinessOfficeContactPersonViewModel BusinessOfficePersonContact { get; set; }

        
        public OfficeHourViewModel OfficeHour { get; set; }

       
        public OpenPracticeStatusViewModel OpenPracticeStatus { get; set; }

        public BillingContactViewModel BillingContact { get; set; }

        public PaymentRemittanceViewModel PaymentRemittance { get; set; }

        public PracticeNonEnglishLanguageViewModel PracticeNonEnglishLanguage { get; set; }

        public PracticeAccessibilityViewModel PracticeAccessibility { get; set; }

        public PracticeServiceViewModel PracticeService { get; set; }
    }
}