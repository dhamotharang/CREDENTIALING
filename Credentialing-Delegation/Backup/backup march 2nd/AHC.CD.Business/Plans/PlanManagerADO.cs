using AHC.CD.Data.ADO.Plan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AHC.CD.Business.Plans
{
    internal class PlanManagerADO : IPlanManagerADO
    {
        private readonly IPlanRepositoryADO iPlanRepositoryADO = null;
        public PlanManagerADO(IPlanRepositoryADO iPlanRepositoryADO)
        {
            this.iPlanRepositoryADO = iPlanRepositoryADO;
        }
        public async Task<object> getAllPlansAsync()
        {
            try
            {
                return await iPlanRepositoryADO.GetAllPlans();
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
    }
}
