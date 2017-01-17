using AHC.CD.Entities.ProviderInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository
{
    public interface ICredentialingRepository : IGenericRepository<Individual>
    {
        Task<IEnumerable<Entities.Credentialing.IndividualPlan>> GetAllCredentialedIndividualPlansAsync(int individualProviderID);

    }
}
