using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PortalTemplate.Areas.Billing.Models.File_Management
{
    public class AdjustmentDetails
    {
        public string AdjustmentGroupCode { get; set; }
        public string AdjustmentReasonCode { get; set; }
        public double AdjustmentAmount { get; set; }
        public string AdjustmentQuantity { get; set; }
        public string AdjustmentReasonCodeDesc { get; set; }
        public string AdjustmentGroupCodeDesc { get; set; }
        public string AdjustmentReasonType { get; set; }
    }
}