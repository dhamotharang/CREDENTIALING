using AHC.CD.Data.Repository;
using AHC.CD.Entities.MasterProfile.CredentialingRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.EFRepository
{
    internal class CredentialingRequestEFRepository : EFGenericRepository<CredentialingRequest>, ICredentialingRequestRepository
    {
    }
}
