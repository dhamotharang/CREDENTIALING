using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.DelegationProfileReport
{
    public class ProviderPracitceInfoBusinessModel
    {
        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string FaxNumber { get; set; }

        public string OfficeHourMonday { get; set; }

        public string OfficeHourTuesday { get; set; }

        public string OfficeHourWednesday { get; set; }

        public string OfficeHourThursday { get; set; }

        public string OfficeHourFridayday { get; set; }

        public string BillingAddress { get; set; }

        public string BillingPhoneNumber { get; set; }

        public string BillingFaxNumber { get; set; }

        public List<string> CoveringPhysicians { get; set; }

    }
}
