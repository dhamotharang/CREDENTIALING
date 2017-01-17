using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PortalTemplate.Areas.Billing.Models.CMS1500.Old
{
    public class PER_AdministrativeCommunicationContact_Professional_837ViewModel
    {
        public int PERkey { get; set; }
        public Nullable<int> Headerkey { get; set; }
        public Nullable<int> Infosourcekey { get; set; }
        public string PER02_ContactName { get; set; }
        public string PER0X_PhoneNo { get; set; }
        public string PER0X_PhoneExtNo { get; set; }
        public string PER0X_FaxNo { get; set; }
        public string PER0X_Email { get; set; }
    
    }
}
