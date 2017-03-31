using AHC.CD.Data.ADO.DTO.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Data.ADO.Plan
{
    public interface IPlanRepositoryADO
    {
        Task<object> GetAllPlans();
        Task<PlanDTO> GetPlanDataByIDAsync(int PlanID);
    }
}
