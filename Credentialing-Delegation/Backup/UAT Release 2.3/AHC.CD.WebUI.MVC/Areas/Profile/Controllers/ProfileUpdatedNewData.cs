using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.Business.BusinessModels.ProfileUpdates
{
    public class ProfileUpdatedNewData
    {
        public string FieldName { get; set; }

        public string OldValue { get; set; }

        public string NewValue { get; set; }

        public string ApprovalStatus { get; set; }  
    }
}
