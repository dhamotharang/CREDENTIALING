using AHC.CD.Entities.Credentialing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository
{
    public interface IPlansRepository : IGenericRepository<Plan>
    {
        Task<IEnumerable<Entities.Credentialing.Plan>> GetAllPlansForInsuranceCompanyAsync(int insuranceCompanyID);
    }
}
