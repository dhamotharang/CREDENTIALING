using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.BusinessModels.ProfileUpdates
{
    public class ApprovalSubmission
    {
        public int TrackerId { get; set; }

        public string ApprovalStatus { get; set; }

        public string RejectionReason { get; set; }
    }
}
