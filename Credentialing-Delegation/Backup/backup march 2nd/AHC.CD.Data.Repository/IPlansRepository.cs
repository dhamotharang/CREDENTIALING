using AHC.CD.Entities.Credentialing;
using AHC.CD.Entities.MasterData.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.Repository
{
    public interface IPlansRepository : IGenericRepository<Plan>
    {
        Task<Plan> GetPlanByIdAsync(int PlanID);
        Task<int> RemovePlanByIdAsync(int PlanID);
        Task<int> ReactivePlanByIdAsync(int PlanID);
    }
}
