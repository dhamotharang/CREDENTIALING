using AHC.CD.Data.Repository;
using AHC.CD.Entities.Credentialing;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity;

namespace AHC.CD.Data.EFRepository
{
    internal class PlansEFRepository : EFGenericRepository<Plan>, IPlansRepository
    {
        public async Task<IEnumerable<Plan>> GetAllPlansForInsuranceCompanyAsync(int insuranceCompanyID)
        {
            var plansForInsuranceComp = from p in DbSet
                                        where p.InsuranceCompanyID == insuranceCompanyID
                                        select p;
            return await plansForInsuranceComp.ToListAsync<Plan>();

        }
    }
}
