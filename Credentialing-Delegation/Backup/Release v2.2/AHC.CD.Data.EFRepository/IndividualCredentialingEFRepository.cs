using AHC.CD.Data.Repository;
using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using AHC.CD.Entities.Credentialing;

namespace AHC.CD.Data.EFRepository
{
    internal class IndividualCredentialingEFRepository : EFGenericRepository<IndividualProvider>, ICredentialingRepository
    {
        //public async Task<IEnumerable<IndividualPlan>> GetAllCredentialedIndividualPlansAsync(int individualProviderID)
        //{
        //    //var provider = await DbSet.FindAsync(individualProviderID);

        //    //var activePlans = from individualPlan in provider.IndividualPlans
        //    //                  where individualPlan.CredentialingHistory.Any(ci => ci.CredentialingStatus == CredentialingStatus.Completed)
        //    //                  select individualPlan;
        //    //return activePlans.ToList();
        //    return null;
        //}
    }
}
