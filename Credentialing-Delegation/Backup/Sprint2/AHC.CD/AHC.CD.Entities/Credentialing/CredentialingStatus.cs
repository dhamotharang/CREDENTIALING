using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHC.CD.Entities.Credentialing
{
    public enum CredentialingStatus
    {
        Initiated=1,
        Submited,
        Verified,
        Approved,
        Completed,
        Rejected,
    }
}
